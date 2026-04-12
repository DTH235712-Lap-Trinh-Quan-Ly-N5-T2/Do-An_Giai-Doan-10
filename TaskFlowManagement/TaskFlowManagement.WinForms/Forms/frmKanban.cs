// ============================================================
//  frmKanban.cs  —  TaskFlowManagement.WinForms.Forms
// ============================================================
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;
using TaskFlowManagement.WinForms.Controls;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmKanban : BaseForm
    {
        // ── Status ID constants ───────────────────────────────────────────────
        private const int StatusCreated = 1;
        private const int StatusAssigned = 2;
        private const int StatusInProgress = 3;
        private const int StatusFailed = 4;
        private const int StatusReview1 = 5;
        private const int StatusReview2 = 6;
        private const int StatusApproved = 7;
        private const int StatusInTest = 8;
        private const int StatusResolved = 9;
        private const int StatusClosed = 10;

        // ── Filter modes ─────────────────────────────────────────────────────
        private enum FilterMode { All, Mine, Overdue }
        private FilterMode _currentFilter = FilterMode.All;

        // ── Toast timer ──────────────────────────────────────────────────────
        private System.Windows.Forms.Timer? _toastTimer;

        // ── Dependencies ─────────────────────────────────────────────────────
        private readonly ITaskService? _taskService;
        private readonly IProjectService? _projectService;
        private readonly IUserService? _userService;
        private readonly int _projectId;

        // ── Constructors ─────────────────────────────────────────────────────

        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmKanban()
        {
            InitializeComponent();
        }

        public frmKanban(
            ITaskService taskService,
            IProjectService projectService,
            IUserService userService,
            int projectId)
#pragma warning disable CS0618
            : this()
#pragma warning restore CS0618
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _projectId = projectId;

            ApplyClientStyles();
            InitializeForm();

            if (_taskService != null)
                _taskService.TaskDataChanged += OnTaskDataChanged;
        }

        private async void OnTaskDataChanged(object? sender, EventArgs e)
        {
            if (this.IsHandleCreated && !this.IsDisposed)
                // BeginInvoke (thay vì Invoke) tránh deadlock với async lambda
                this.BeginInvoke(async () =>
                {
                    if (!this.IsDisposed) await LoadTasksAsync();
                });
        }

        // ── Làm đẹp: toàn bộ màu sắc, font, style ────────────────────────────
        private void ApplyClientStyles()
        {
            this.BackColor = UIHelper.ColorBackground;

            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelAccentLine.BackColor = UIHelper.ColorAccent;
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;

            panelFilter.BackColor = UIHelper.ColorSurface;
            lblFilterHint.Font = UIHelper.FontSmall;
            lblFilterHint.ForeColor = UIHelper.ColorTextSecondary;

            UIHelper.StyleButton(btnRefresh, UIHelper.ButtonVariant.Secondary);
            UIHelper.StyleButton(btnFilterAll, UIHelper.ButtonVariant.Primary);
            UIHelper.StyleButton(btnFilterMine, UIHelper.ButtonVariant.Secondary);
            UIHelper.StyleButton(btnFilterOverdue, UIHelper.ButtonVariant.Secondary);

            panelToast.BackColor = UIHelper.ColorSuccess;
            lblToast.Font = UIHelper.FontBold;

            panelStatus.BackColor = UIHelper.ColorHeaderBg;
            lblStatus.Font = UIHelper.FontSmall;
            lblStatus.ForeColor = UIHelper.ColorTextSecondary;

            // Khởi tạo layout lưới Kanban
            tlpBoard.BackColor = UIHelper.ColorBackground;
            tlpBoard.ColumnStyles.Clear();
            for (int i = 0; i < 6; i++)
                tlpBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 6));

            // Cấu hình màu nền cho từng cột Kanban
            BuildColumn(pnlTodo, lblTodo, flpTodo, lblBadgeTodo,
                "📋  To Do",
                UIHelper.ColorColumnTodoBg,
                UIHelper.ColorColumnTodoFg,
                UIHelper.ColorColumnTodoBody);

            BuildColumn(pnlInProgress, lblInProgress, flpInProgress, lblBadgeInProgress,
                "🔄  In Progress",
                UIHelper.ColorColumnInProgressBg,
                UIHelper.ColorColumnInProgressFg,
                UIHelper.ColorColumnInProgressBody);

            BuildColumn(pnlReview, lblReview, flpReview, lblBadgeReview,
                "🔍  Review",
                UIHelper.ColorColumnReviewBg,
                UIHelper.ColorColumnReviewFg,
                UIHelper.ColorColumnReviewBody);

            BuildColumn(pnlTesting, lblTesting, flpTesting, lblBadgeTesting,
                "🧪  Testing",
                UIHelper.ColorColumnTestingBg,
                UIHelper.ColorColumnTestingFg,
                UIHelper.ColorColumnTestingBody);

            BuildColumn(pnlFailed, lblFailed, flpFailed, lblBadgeFailed,
                "❌  Failed",
                UIHelper.ColorColumnFailedBg,
                UIHelper.ColorColumnFailedFg,
                UIHelper.ColorColumnFailedBody);

            BuildColumn(pnlDone, lblDone, flpDone, lblBadgeDone,
                "✅  Done",
                UIHelper.ColorColumnDoneBg,
                UIHelper.ColorColumnDoneFg,
                UIHelper.ColorColumnDoneBody);
        }

        // ── Tạo thành phần thẻ cột Kanban ────────────────────────────────────
        private void BuildColumn(
            Panel pnlColumn,
            Label lblColName,
            DoubleBufferedFlowLayoutPanel flp,
            Label lblBadge,
            string title,
            System.Drawing.Color headerBg,
            System.Drawing.Color headerFg,
            System.Drawing.Color bodyBg)
        {
            pnlColumn.BackColor = bodyBg;
            pnlColumn.Padding = new Padding(0);

            var pnlColHeader = new Panel
            {
                BackColor = headerBg,
                Dock = DockStyle.Top,
                Height = 44,
            };

            lblColName.AutoSize = false;
            lblColName.BackColor = System.Drawing.Color.Transparent;
            lblColName.Dock = DockStyle.Fill;
            lblColName.Font = UIHelper.FontColumnHeader;
            lblColName.ForeColor = headerFg;
            lblColName.Padding = new Padding(10, 0, 40, 0);
            lblColName.Text = title;
            lblColName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            lblBadge.AutoSize = false;
            lblBadge.BackColor = System.Drawing.Color.Transparent;
            lblBadge.ForeColor = System.Drawing.Color.White;
            lblBadge.Font = UIHelper.FontBadge;
            lblBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBadge.Size = new System.Drawing.Size(28, 20);
            lblBadge.Text = "0";
            lblBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblBadge.Paint += LblBadge_Paint;

            // Căn badge về phía phải header khi resize
            pnlColHeader.SizeChanged += (_, _) =>
                lblBadge.Location = new System.Drawing.Point(pnlColHeader.Width - lblBadge.Width - 8, 12);

            pnlColHeader.Controls.AddRange(new Control[] { lblColName, lblBadge });

            flp.AutoScroll = true;
            flp.BackColor = bodyBg;
            flp.Dock = DockStyle.Fill;
            flp.FlowDirection = FlowDirection.TopDown;
            flp.Padding = new Padding(6);
            flp.WrapContents = false;

            pnlColumn.Controls.Clear();
            pnlColumn.Controls.AddRange(new Control[] { flp, pnlColHeader });
        }

        // ── Form Init ─────────────────────────────────────────────────────────
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_taskService == null || DesignMode) return;

            string projectName = $"Dự án #{_projectId}";
            if (_projectService != null)
            {
                var project = await _projectService.GetProjectDetailsAsync(_projectId);
                if (project != null)
                {
                    projectName = project.Name;
                }
            }

            var title = $"🗂️  Kanban Board — {projectName}";
            this.Text = title;
            lblHeader.Text = title;

            await LoadTasksAsync();
        }

        private void InitializeForm()
        {
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1000, 650);
            EnableDragDropOnAllColumns();
            SetupToastTimer();
            WireFilterButtons();
        }

        // ── Toast ─────────────────────────────────────────────────────────────
        private void SetupToastTimer()
        {
            _toastTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _toastTimer.Tick += (_, _) =>
            {
                _toastTimer.Stop();
                panelToast.Visible = false;
            };
        }

        private void ShowToast(string message, bool isError = false)
        {
            _toastTimer?.Stop();
            lblToast.Text = message;
            panelToast.BackColor = isError ? UIHelper.ColorDanger : UIHelper.ColorSuccess;
            panelToast.Visible = true;
            _toastTimer?.Start();
        }

        // ── Bộ lọc nhanh ─────────────────────────────────────────────────────
        private void WireFilterButtons()
        {
            btnFilterAll.Click += (_, _) => ApplyFilter(FilterMode.All);
            btnFilterMine.Click += (_, _) => ApplyFilter(FilterMode.Mine);
            btnFilterOverdue.Click += (_, _) => ApplyFilter(FilterMode.Overdue);
        }

        private void ApplyFilter(FilterMode mode)
        {
            _currentFilter = mode;

            UIHelper.StyleButton(btnFilterAll, mode == FilterMode.All ? UIHelper.ButtonVariant.Primary : UIHelper.ButtonVariant.Secondary);
            UIHelper.StyleButton(btnFilterMine, mode == FilterMode.Mine ? UIHelper.ButtonVariant.Info : UIHelper.ButtonVariant.Secondary);
            UIHelper.StyleButton(btnFilterOverdue, mode == FilterMode.Overdue ? UIHelper.ButtonVariant.Danger : UIHelper.ButtonVariant.Secondary);

            foreach (var panel in GetAllColumns())
                foreach (Control ctrl in panel.Controls)
                    if (ctrl is ucTaskCard card)
                        ctrl.Visible = IsCardVisible(card);

            UpdateAllColumnBadges();
        }

        private bool IsCardVisible(ucTaskCard card)
        {
            if (card.Tag is not TaskItem task) return true;

            return _currentFilter switch
            {
                FilterMode.Mine => task.AssignedToId == AppSession.UserId,
                FilterMode.Overdue => task.DueDate.HasValue
                                      && task.DueDate.Value < DateTime.UtcNow
                                      && !task.IsCompleted,
                _ => true,
            };
        }

        // ── Tải dữ liệu ──────────────────────────────────────────────────────
        private async Task LoadTasksAsync()
        {
            SetStatus("⏳  Đang tải dữ liệu...");
            ClearAllColumns();

            // ── Chống Lag: Tạm dừng layout toàn bộ cột trước khi render ──────
            foreach (var flp in GetAllColumns())
                flp.SuspendLayout();

            try
            {
                var tasks = await _taskService!.GetBoardTasksAsync(_projectId);

                foreach (var task in tasks)
                {
                    var card = CreateCard(task);
                    MoveCardToStatusPanel(card, task.StatusId);
                }

                UpdateAllColumnBadges();
                SetStatus($"✔  Đã tải {tasks.Count} công việc");
            }
            catch (Exception ex)
            {
                SetStatus("⚠  Lỗi tải dữ liệu.");
                MessageBox.Show("Không thể tải Kanban board:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // ── Chống Lag: Khôi phục layout sau khi render xong ──────────
                foreach (var flp in GetAllColumns())
                    flp.ResumeLayout(performLayout: true);
            }
        }

        // ── Tạo thẻ công việc ────────────────────────────────────────────────
        private ucTaskCard CreateCard(TaskItem task)
        {
            var card = new ucTaskCard { Margin = new Padding(0, 0, 0, 8), Tag = task };
            card.Bind(task);
            card.StatusChanged += TaskCard_StatusChanged;
            card.CardDoubleClicked += TaskCard_DoubleClicked;
            card.TitleChanged += TaskCard_TitleChanged;
            return card;
        }

        private void ClearAllColumns()
        {
            foreach (var panel in GetAllColumns())
            {
                // ── Chống Memory Leak: hủy event và Dispose từng card ────────
                var cards = panel.Controls.OfType<ucTaskCard>().ToList();
                foreach (var card in cards)
                {
                    card.StatusChanged     -= TaskCard_StatusChanged;
                    card.CardDoubleClicked -= TaskCard_DoubleClicked;
                    card.TitleChanged      -= TaskCard_TitleChanged;
                    card.Dispose();
                }
                panel.Controls.Clear();
            }
        }

        // ── Xử lý sự kiện: đổi trạng thái qua context menu ──────────────────
        private async void TaskCard_StatusChanged(object? sender, StatusChangedEventArgs e)
        {
            if (sender is not ucTaskCard card) return;

            try
            {
                // ── GIỮ NGUYÊN: gọi DB bình thường, await được phép ───────────────
                var (success, message) = await _taskService!.UpdateStatusAsync(
                    e.TaskId, e.NewStatusId, AppSession.UserId, AppSession.Roles);

                if (!success)
                {
                    ShowToast($"⚠  {message}", isError: true);
                    return;
                }

                // ── FIX ObjectDisposedException: bọc phần cập nhật UI vào BeginInvoke ──
                // DB đã được commit, event call stack đã kết thúc sau await ở trên.
                // BeginInvoke đảm bảo các thao tác UI này chạy sau khi stack tă kết thúc.
                this.BeginInvoke(() =>
                {
                    if (this.IsDisposed || card.IsDisposed) return;

                    if (card.Parent is FlowLayoutPanel src) src.Controls.Remove(card);

                    if (card.Tag is TaskItem task) task.StatusId = e.NewStatusId;

                    card.UpdateBoundStatus(e.NewStatusId);
                    MoveCardToStatusPanel(card, e.NewStatusId);
                    UpdateAllColumnBadges();

                    ShowToast($"✔  Đã chuyển sang {Core.Constants.WorkflowConstants.GetStatusName(e.NewStatusId)}");
                });
            }
            catch (Exception ex)
            {
                ShowToast($"⚠  Lỗi: {ex.Message}", isError: true);
            }
        }

        // ── Xử lý sự kiện: mở form chi tiết ─────────────────────────────────
        private void TaskCard_DoubleClicked(object? sender, int taskId)
        {
            // ── FIX ObjectDisposedException ──────────────────────────────────
            // Dùng BeginInvoke để post lệnh mở form ra khỏi call stack hiện tại.
            // Điều này cho phép event handler kết thúc an toàn TRƯỚC khi
            // LoadTasksAsync() → ClearAllColumns() → card.Dispose() được gọi.
            this.BeginInvoke(async () =>
            {
                if (this.IsDisposed) return;
                try
                {
                    using var detailForm = new frmTaskEdit(
                        _taskService!, _projectService!, _userService!, taskId);

                    if (detailForm.ShowDialog() == DialogResult.OK)
                        await LoadTasksAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở chi tiết: " + ex.Message, "Lỗi");
                }
            });
        }

        // ── Xử lý sự kiện: sửa tiêu đề inline ───────────────────────────────
        private async void TaskCard_TitleChanged(object? sender, (int TaskId, string NewTitle) e)
        {
            if (_taskService == null) return;

            try
            {
                var task = await _taskService.GetByIdAsync(e.TaskId);
                if (task == null)
                {
                    ShowToast("⚠  Không tìm thấy task.", isError: true);
                    return;
                }

                task.Title = e.NewTitle;
                task.UpdatedAt = DateTime.UtcNow;

                var (success, message) = await _taskService.UpdateTaskAsync(task);

                if (success)
                {
                    // card.Tag cập nhật nhẹ — không reload, không Dispose risk
                    if (sender is ucTaskCard card && !card.IsDisposed) card.Tag = task;
                    ShowToast($"✔  Đã lưu: \"{e.NewTitle}\"");
                }
                else
                {
                    // Chỉ gọi Bind() nếu card vẫn còn sống
                    if (sender is ucTaskCard card && !card.IsDisposed) card.Bind(task);
                    ShowToast($"⚠  {message}", isError: true);
                }
            }
            catch (Exception ex)
            {
                ShowToast($"⚠  Lỗi lưu tiêu đề: {ex.Message}", isError: true);
            }
        }

        // ── Nút Làm mới ──────────────────────────────────────────────────────
        private async void btnRefresh_Click(object? sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            try
            {
                await LoadTasksAsync();
            }
            finally
            {
                if (!this.IsDisposed) btnRefresh.Enabled = true;
            }
        }

        // ── Drag-drop ─────────────────────────────────────────────────────────
        private void EnableDragDropOnAllColumns()
        {
            foreach (var panel in GetAllColumns())
            {
                panel.AllowDrop = true;
                panel.DragEnter += Column_DragEnter;
                panel.DragDrop += Column_DragDrop;
            }
        }

        private void Column_DragEnter(object? sender, DragEventArgs e)
            => e.Effect = e.Data?.GetDataPresent(typeof(int)) == true
                ? DragDropEffects.Move
                : DragDropEffects.None;

        private async void Column_DragDrop(object? sender, DragEventArgs e)
        {
            if (sender is not DoubleBufferedFlowLayoutPanel targetPanel) return;
            if (e.Data?.GetData(typeof(int)) is not int taskId) return;

            int newStatusId = GetPrimaryStatusForPanel(targetPanel);
            if (newStatusId <= 0) return;

            var (success, message) = await _taskService!.UpdateStatusAsync(
                taskId, newStatusId, AppSession.UserId, AppSession.Roles);

            if (!success)
            {
                ShowToast($"⚠  {message}", isError: true);
                return;
            }

            var card = FindCardById(taskId);
            if (card == null) return;

            if (card.Parent is FlowLayoutPanel srcPanel) srcPanel.Controls.Remove(card);

            if (card.Tag is TaskItem task) task.StatusId = newStatusId;

            card.UpdateBoundStatus(newStatusId);
            targetPanel.Controls.Add(card);
            card.Visible = IsCardVisible(card);

            UpdateAllColumnBadges();
            ShowToast($"✔  Đã chuyển sang {Core.Constants.WorkflowConstants.GetStatusName(newStatusId)}");
        }

        // ── Cập nhật badge đếm card ───────────────────────────────────────────
        private void UpdateAllColumnBadges()
        {
            lblBadgeTodo.Text = CountVisibleCards(flpTodo).ToString();
            lblBadgeInProgress.Text = CountVisibleCards(flpInProgress).ToString();
            lblBadgeReview.Text = CountVisibleCards(flpReview).ToString();
            lblBadgeTesting.Text = CountVisibleCards(flpTesting).ToString();
            lblBadgeFailed.Text = CountVisibleCards(flpFailed).ToString();
            lblBadgeDone.Text = CountVisibleCards(flpDone).ToString();
        }

        private static int CountVisibleCards(FlowLayoutPanel panel)
            => panel.Controls.OfType<ucTaskCard>().Count(c => c.Visible);

        // ── Routing helpers ───────────────────────────────────────────────────
        private void MoveCardToStatusPanel(ucTaskCard card, int statusId)
        {
            var target = GetPanelByStatusId(statusId);
            if (target == null) return;
            target.Controls.Add(card);
            card.Visible = IsCardVisible(card);
        }

        private DoubleBufferedFlowLayoutPanel? GetPanelByStatusId(int statusId) => statusId switch
        {
            StatusCreated or StatusAssigned => flpTodo,
            StatusInProgress => flpInProgress,
            StatusReview1 or StatusReview2 => flpReview,
            StatusInTest => flpTesting,
            StatusFailed => flpFailed,
            StatusApproved or StatusResolved or StatusClosed => flpDone,
            _ => null,
        };

        private int GetPrimaryStatusForPanel(DoubleBufferedFlowLayoutPanel panel)
        {
            if (panel == flpTodo) return StatusCreated;
            if (panel == flpInProgress) return StatusInProgress;
            if (panel == flpReview) return StatusReview1;
            if (panel == flpTesting) return StatusInTest;
            if (panel == flpFailed) return StatusFailed;
            if (panel == flpDone) return StatusResolved;
            return 0;
        }

        private IEnumerable<DoubleBufferedFlowLayoutPanel> GetAllColumns()
        {
            yield return flpTodo;
            yield return flpInProgress;
            yield return flpReview;
            yield return flpTesting;
            yield return flpFailed;
            yield return flpDone;
        }

        private ucTaskCard? FindCardById(int taskId)
        {
            foreach (var panel in GetAllColumns())
                foreach (Control ctrl in panel.Controls)
                    if (ctrl is ucTaskCard card && card.TaskId == taskId)
                        return card;
            return null;
        }

        // ── Thanh trạng thái ─────────────────────────────────────────────────
        private void SetStatus(string msg)
        {
            if (lblStatus != null && !lblStatus.IsDisposed)
                lblStatus.Text = msg;
        }

        // ── Dispose ───────────────────────────────────────────────────────────
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_taskService != null)
                _taskService.TaskDataChanged -= OnTaskDataChanged;

            _toastTimer?.Stop();
            _toastTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}