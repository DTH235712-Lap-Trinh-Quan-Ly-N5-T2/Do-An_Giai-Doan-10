using Microsoft.Extensions.DependencyInjection;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;
using Microsoft.Extensions.DependencyInjection;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmMain : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INotificationService _notification;
        private System.Windows.Forms.Timer? _clockTimer;

        private System.Windows.Forms.Timer? _notificationPollingTimer;
        private DateTime _lastNotificationCheckUtc = DateTime.UtcNow;
        private const int PollingIntervalMs = 5000;

        private const int MaxBellItems = 20;
        private readonly LinkedList<NotificationEventArgs> _bellItems = new();
        private int _unreadCount = 0;
        private ToolStripLabel? _lblBell;
        private ContextMenuStrip? _bellDropdown;

        public frmMain(IServiceProvider serviceProvider, INotificationService notification)
        {
            _serviceProvider = serviceProvider;
            _notification = notification;
            InitializeComponent();
            ApplyRolePermissions();
            StartClock();
            UpdateUserInfo();
            BuildBellIcon();
            StartNotificationPolling();

            _notification.NotificationReceived += OnNotificationReceived;
            this.FormClosed += (_, _) => _notification.NotificationReceived -= OnNotificationReceived;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            OpenHome();
        }

        private void OpenHome()
        {
            foreach (Form child in this.MdiChildren) child.Close();
            var home = _serviceProvider.GetRequiredService<frmHome>();
            home.MdiParent   = this;
            home.WindowState = FormWindowState.Maximized;
            home.Show();
        }

        private void UpdateUserInfo()
        {
            lblStatusUser.Text = $"  👤 {AppSession.FullName}";
            lblStatusRole.Text = $"  [{string.Join(", ", AppSession.Roles)}]";
            this.Text          = $"TaskFlow  —  {AppSession.FullName}";
        }

        // Phân quyền menu theo Role
        private void ApplyRolePermissions()
        {
            // Reset — mặc định hiện tất cả
            menuUsers.Visible        = true;
            menuCustomers.Visible    = true;
            menuProjects.Visible     = true;
            menuReports.Visible      = true;
            menuTaskList.Visible     = true;
            menuExpenses.Visible     = true;
            menuKanban.Visible       = true;
            menuUserAccounts.Visible = true;

            // FIX GD4: menuMyTasks hiện cho MỌI role (cả Developer lẫn Manager)
            // Developer cần xem task được giao cho mình → không ẩn menu này
            menuMyTasks.Visible = true;

            if (!AppSession.IsManager)
            {
                menuUsers.Visible     = false;
                menuCustomers.Visible = false;
                menuReports.Visible   = false;
                // FIX GD4: Developer vẫn thấy menu "Danh sách công việc" (xem read-only)
                // Trong frmTaskList, ApplyRolePermissions() đã ẩn nút Thêm/Sửa/Xóa rồi
                // menuTaskList.Visible = false; ← ĐÃ BỎ dòng này
                menuKanban.Visible    = false;
                menuProjectNew.Visible = false; // Developer không tạo dự án mới
            }
            if (!AppSession.IsAdmin)
                menuUserAccounts.Visible = false;
        }

        // Đồng hồ realtime
        private void StartClock()
        {
            StopClock();
            _clockTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _clockTimer.Tick += (s, e) =>
                lblStatusTime.Text = DateTime.Now.ToString("HH:mm:ss   dd/MM/yyyy  ");
            _clockTimer.Start();
        }

        private void StopClock()
        {
            if (_clockTimer != null)
            { _clockTimer.Stop(); _clockTimer.Dispose(); _clockTimer = null; }
        }

        // Mở MDI child, kiểm tra tránh mở trùng form cùng type
        public void OpenMdiChild(Form child)
        {
            foreach (Form existing in this.MdiChildren)
            {
                if (existing.GetType() == child.GetType())
                { 
                    existing.Activate(); 
                    child.Dispose(); 
                    return; 
                }
            }
            child.MdiParent   = this;
            child.WindowState = FormWindowState.Maximized;
            child.Show();
        }

        /// <summary>
        /// Điều hướng từ Dashboard xuống Danh sách Task (Drill-down)
        /// </summary>
        public void OpenTaskListWithFilter(string filterType, int? projectId)
        {
            // Kiểm tra xem Form đã mở chưa
            var existing = this.MdiChildren.OfType<frmTaskList>().FirstOrDefault();
            if (existing != null)
            {
                existing.Activate();
                _ = existing.ApplyExternalFilter(filterType, projectId);
            }
            else
            {
                var frm = _serviceProvider.GetRequiredService<frmTaskList>();
                OpenMdiChild(frm);
                _ = frm.ApplyExternalFilter(filterType, projectId);
            }
        }

        /// <summary>
        /// Mở form Kanban của một dự án cụ thể.
        /// Sử dụng ActivatorUtilities để resolve form kèm tham số projectId.
        /// </summary>
        public async Task OpenKanbanForProjectAsync(int projectId)
        {
            // Resolve form frmKanban từ DI container và truyền tham số projectId thủ công
            var frm = ActivatorUtilities.CreateInstance<frmKanban>(_serviceProvider, projectId);
            OpenMdiChild(frm);
            await Task.CompletedTask;
        }

        // ── Menu: Hệ thống ────────────────────────────────────
        private void menuHome_Click(object sender, EventArgs e) => OpenHome();

        private void menuChangePassword_Click(object sender, EventArgs e)
        {
            // FIX BUG #5: Bọc using để Dispose form ngay sau khi đóng, tránh memory leak
            using var frm = _serviceProvider.GetRequiredService<frmChangePassword>();
            frm.ShowDialog(this);
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận đăng xuất",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            StopClock();
            AppSession.Logout();
            this.Hide();

            // FIX BUG #6: Bọc using để Dispose frmLogin sau mỗi lần logout
            using var loginForm = _serviceProvider.GetRequiredService<frmLogin>();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                UpdateUserInfo();
                ApplyRolePermissions();
                StartClock();
                
                // Reset bell + polling cho user mới
                _bellItems.Clear();
                _unreadCount = 0;
                RefreshBellLabel();
                _lastNotificationCheckUtc = DateTime.UtcNow;
                
                this.Show();
                OpenHome();
            }
            else Application.Exit();
        }

        private void menuExit_Click(object sender, EventArgs e) => Application.Exit();

        // ── Menu: Người dùng ──────────────────────────────────
        private void menuUserAccounts_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmUsers>());

        private void menuEmployees_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmUsers>());

        // ── Menu: Khách hàng ──────────────────────────────────
        private void menuCustomerList_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmCustomers>());

        // ── Menu: Dự án (GD3) ─────────────────────────────────
        private void menuProjectList_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmProjects>());

        private void menuProjectNew_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmProjects>());

        // ── Menu: Công việc (GD4) ─────────────────────────────

        /// <summary>
        /// Mở form Danh sách công việc — GD4.
        /// Manager: thấy tất cả task, có nút Thêm/Sửa/Xóa.
        /// Developer: chỉ thấy task được giao, không có nút CRUD.
        /// </summary>
        private void menuTaskList_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmTaskList>());

        private void menuKanban_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bảng Kanban cần được gắn với một dự án cụ thể.\n\nVui lòng chọn một Dự án trong danh sách và bấm nút 'Kanban' trên thanh công cụ nhé!",
                            "Hướng dẫn",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            // Tự động chuyển hướng mở form Danh sách Dự án luôn cho tiện
            OpenMdiChild(_serviceProvider.GetRequiredService<frmProjects>());
        }

        /// <summary>
        /// Mở form Công việc của tôi — GD4.
        /// Hiển thị task được giao / cần review / cần test của user đang đăng nhập.
        /// </summary>
        private void menuMyTasks_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmMyTasks>());

        // ── Menu: Chi phí (Giai đoạn 8) ───────────────────────
        private void menuExpenseList_Click(object sender, EventArgs e)
            => OpenMdiChild(_serviceProvider.GetRequiredService<frmExpenses>());

        // ── Menu: Báo cáo (Giai đoạn 6) ───────────────────────
        private void OpenDashboardTab(int index)
        {
            var existing = this.MdiChildren.OfType<frmDashboard>().FirstOrDefault();
            if (existing != null)
            {
                existing.Activate();
                existing.SelectTab(index);
            }
            else
            {
                var frm = _serviceProvider.GetRequiredService<frmDashboard>();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                // Phải show rồi mới SelectTab được vì TabControl cần tạo handle trước
                frm.SelectTab(index); 
            }
        }

        // NHẬN THÔNG BÁO TỪ TaskService
        private void OnNotificationReceived(object? sender, NotificationEventArgs e)
        {
            if (!AppSession.IsLoggedIn || e.TargetUserId != AppSession.UserId)
                return;

            // Marshal về UI thread
            this.InvokeIfRequired(() =>
            {
                _bellItems.AddFirst(e);
                while (_bellItems.Count > MaxBellItems)
                    _bellItems.RemoveLast();

                _unreadCount++;
                RefreshBellLabel();
            });
        }

        private void BuildBellIcon()
        {
            _lblBell = new ToolStripLabel
            {
                Text         = "🔔 (0)",
                Font         = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor    = Color.DimGray,
                IsLink       = true,
                LinkBehavior = LinkBehavior.NeverUnderline,
                Alignment    = ToolStripItemAlignment.Right,
                Margin       = new Padding(0, 4, 12, 0)
            };
            _lblBell.Click += (_, _) => ShowBellDropdown();

            _bellDropdown = new ContextMenuStrip
            {
                ShowImageMargin = false,
                Font            = new Font("Segoe UI", 9F)
            };

            menuStrip.Items.Add(_lblBell);
        }

        private void RefreshBellLabel()
        {
            if (_lblBell == null) return;
            _lblBell.Text      = $"🔔 ({_unreadCount})";
            _lblBell.ForeColor = _unreadCount > 0 ? Color.OrangeRed : Color.DimGray;
        }

        private void ShowBellDropdown()
        {
            if (_bellDropdown == null || _lblBell == null) return;

            _bellDropdown.Items.Clear();

            if (_bellItems.Count == 0)
            {
                _bellDropdown.Items.Add(new ToolStripMenuItem("(Chưa có thông báo nào)")
                {
                    Enabled = false,
                    Font    = new Font("Segoe UI", 9F, FontStyle.Italic)
                });
            }
            else
            {
                _bellDropdown.Items.Add(new ToolStripMenuItem($"📬 {_bellItems.Count} thông báo")
                {
                    Enabled = false,
                    Font    = new Font("Segoe UI", 9F, FontStyle.Bold)
                });
                _bellDropdown.Items.Add(new ToolStripSeparator());

                foreach (var item in _bellItems)
                {
                    var menuItem = new ToolStripMenuItem
                    {
                        Text      = $"{GetBellIcon(item.Level)}  {item.Title}\n{item.Message}\n{item.CreatedAt:HH:mm:ss dd/MM}",
                        ForeColor = GetBellColor(item.Level),
                        Font      = new Font("Segoe UI", 9F)
                    };
                    _bellDropdown.Items.Add(menuItem);
                }

                _bellDropdown.Items.Add(new ToolStripSeparator());
                var clearItem = new ToolStripMenuItem("✓ Đánh dấu đã đọc tất cả");
                clearItem.Click += (_, _) =>
                {
                    _unreadCount = 0;
                    RefreshBellLabel();
                };
                _bellDropdown.Items.Add(clearItem);
            }

            var screenPos = menuStrip.PointToScreen(new Point(_lblBell.Bounds.Left, menuStrip.Height));
            _bellDropdown.Show(screenPos);

            _unreadCount = 0;
            RefreshBellLabel();
        }

        private static string GetBellIcon(NotificationLevel level) => level switch
        {
            NotificationLevel.Success => "✅",
            NotificationLevel.Warning => "⚠️",
            NotificationLevel.Error   => "❌",
            _                         => "ℹ️"
        };

        private static Color GetBellColor(NotificationLevel level) => level switch
        {
            NotificationLevel.Success => Color.SeaGreen,
            NotificationLevel.Warning => Color.DarkOrange,
            NotificationLevel.Error   => Color.Firebrick,
            _                         => Color.SteelBlue
        };

        private void menuDashboard_Click(object sender, EventArgs e)
            => OpenDashboardTab(0);

        private void menuReportProgress_Click(object sender, EventArgs e)
            => OpenDashboardTab(1);

        private void menuReportBudget_Click(object sender, EventArgs e)
            => OpenDashboardTab(2);

        private static void ShowComingSoon(string featureName, string phase)
            => MessageBox.Show(
                $"Tính năng \"{featureName}\" đang được phát triển.\n\nDự kiến hoàn thành: {phase}.",
                "Đang phát triển", MessageBoxButtons.OK, MessageBoxIcon.Information);

        protected override void OnFormClosing(FormClosingEventArgs e)
        { 
            StopClock(); 
            StopNotificationPolling();   // ✅ THÊM
            base.OnFormClosing(e); 
        }

        private void StartNotificationPolling()
        {
            _lastNotificationCheckUtc = DateTime.UtcNow;

            _notificationPollingTimer = new System.Windows.Forms.Timer { Interval = PollingIntervalMs };
            _notificationPollingTimer.Tick += async (_, _) =>
            {
                try
                {
                    await PollNotificationsAsync();
                }
                catch (Exception ex)
                {
                    // Log để debug — KHÔNG để exception bubble lên crash app
                    System.Diagnostics.Debug.WriteLine($"[POLLING ERROR] {ex.GetType().Name}: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
            };
            _notificationPollingTimer.Start();
        }

        private void StopNotificationPolling()
        {
            if (_notificationPollingTimer != null)
            {
                _notificationPollingTimer.Stop();
                _notificationPollingTimer.Dispose();
                _notificationPollingTimer = null;
            }
        }

        private async Task PollNotificationsAsync()
        {
            if (!AppSession.IsLoggedIn) return;

            try
            {
                // Resolve TaskService trong scope mới (vì nó là Scoped)
                using var scope = _serviceProvider.CreateScope();
                var taskService = scope.ServiceProvider.GetRequiredService<ITaskService>();

                var checkpoint = _lastNotificationCheckUtc;
                _lastNotificationCheckUtc = DateTime.UtcNow;   // cập nhật trước, tránh trùng lặp

                var newNotifications = await taskService.GetNewNotificationsAsync(
                    AppSession.UserId, checkpoint);

                if (newNotifications.Count == 0) return;

                // Đẩy vào bell list (đã ở UI thread vì Timer.Tick chạy trên UI thread)
                foreach (var n in newNotifications)
                {
                    var args = new NotificationEventArgs
                    {
                        TargetUserId = AppSession.UserId,
                        Title        = n.Title,
                        Message      = n.Message,
                        Level        = n.Level,
                        CreatedAt    = n.UpdatedAt.ToLocalTime()
                    };

                    _bellItems.AddFirst(args);
                    while (_bellItems.Count > MaxBellItems)
                        _bellItems.RemoveLast();

                    _unreadCount++;
                }

                RefreshBellLabel();
            }
            catch
            {
                // Nuốt lỗi — polling fail không được làm crash app
                // (VD: mất mạng tạm thời, DB lock, ...)
            }
        }
    }
}
