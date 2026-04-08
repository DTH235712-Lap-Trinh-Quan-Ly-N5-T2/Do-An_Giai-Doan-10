// ============================================================
//  frmKanban.cs  —  TaskFlowManagement.WinForms.Forms
//
//  NÂNG CẤP so với phiên bản cũ:
//    1. Bộ lọc nhanh: [Tất cả] [Của tôi] [Quá hạn]
//       → dùng card.Visible thay vì reload từ DB (nhanh hơn, không gây flicker)
//    2. Badge đếm số card trên mỗi đầu cột
//       → cập nhật sau mỗi thao tác drag-drop / filter
//    3. Toast notification sau drag-drop / inline edit
//       → Panel trượt lên từ dưới, tự ẩn sau 3 giây
//    4. Xử lý TitleChanged từ ucTaskCard → gọi UpdateTaskAsync
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
        private const int StatusCreated    = 1;
        private const int StatusAssigned   = 2;
        private const int StatusInProgress = 3;
        private const int StatusFailed     = 4;
        private const int StatusReview1    = 5;
        private const int StatusReview2    = 6;
        private const int StatusApproved   = 7;
        private const int StatusInTest     = 8;
        private const int StatusResolved   = 9;
        private const int StatusClosed     = 10;

        // ── Filter modes ─────────────────────────────────────────────────────
        private enum FilterMode { All, Mine, Overdue }
        private FilterMode _currentFilter = FilterMode.All;

        // ── Toast timer ──────────────────────────────────────────────────────
        private System.Windows.Forms.Timer? _toastTimer;

        // ── Dependencies ─────────────────────────────────────────────────────
        private readonly ITaskService?    _taskService;
        private readonly IProjectService? _projectService;
        private readonly IUserService?    _userService;
        private readonly int              _projectId;

        // ── Constructors ─────────────────────────────────────────────────────
        [Obsolete("Chỉ dành cho VS Designer.")]
        public frmKanban()
        {
            InitializeComponent();
            ApplyClientStyles();   // ← thêm dòng này
            InitializeForm();
        }

        private void ApplyClientStyles()
        {
            this.BackColor = UIHelper.ColorBackground;

            // ── panelHeader ───────────────────────────────────────────────────────
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelAccentLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;

            // ── panelFilter ───────────────────────────────────────────────────────
            panelFilter.BackColor = UIHelper.ColorBackground;

            UIHelper.StyleToolButton(btnRefresh, "🔄  Làm mới", UIHelper.ButtonVariant.Secondary, 12, 10, 110, 30);
            UIHelper.StyleToolButton(btnFilterAll, "Tất cả", UIHelper.ButtonVariant.Primary, 132, 10, 80, 30);
            UIHelper.StyleToolButton(btnFilterMine, "👤  Của tôi", UIHelper.ButtonVariant.Secondary, 222, 10, 100, 30);
            UIHelper.StyleToolButton(btnFilterOverdue, "⚠  Quá hạn", UIHelper.ButtonVariant.Secondary, 332, 10, 100, 30);

            lblFilterHint.Font = UIHelper.FontSmall;
            lblFilterHint.ForeColor = UIHelper.ColorMuted;

            // ── panelToast ────────────────────────────────────────────────────────
            panelToast.BackColor = UIHelper.ColorSuccess;
            lblToast.Font = UIHelper.FontBold;

            // ── panelStatus ───────────────────────────────────────────────────────
            panelStatus.BackColor = UIHelper.ColorHeaderBg;
            lblStatus.Font = UIHelper.FontSmall;
            lblStatus.ForeColor = UIHelper.ColorSubtitle;

            // ── tlpBoard — loop không được phép trong Designer ────────────────────
            tlpBoard.BackColor = UIHelper.ColorBackground;
            tlpBoard.ColumnStyles.Clear();
            for (int i = 0; i < 6; i++)
                tlpBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 6));

            // ── Build 6 cột Kanban ────────────────────────────────────────────────
            BuildColumn(pnlTodo, lblTodo, flpTodo, lblBadgeTodo,
                "📋  To Do",
                System.Drawing.Color.FromArgb(226, 232, 240),
                System.Drawing.Color.FromArgb(51, 65, 85),
                System.Drawing.Color.FromArgb(248, 250, 252));

            BuildColumn(pnlInProgress, lblInProgress, flpInProgress, lblBadgeInProgress,
                "🔄  In Progress",
                System.Drawing.Color.FromArgb(191, 219, 254),
                System.Drawing.Color.FromArgb(30, 58, 138),
                System.Drawing.Color.FromArgb(239, 246, 255));

            BuildColumn(pnlReview, lblReview, flpReview, lblBadgeReview,
                "🔍  Review",
                System.Drawing.Color.FromArgb(253, 230, 138),
                System.Drawing.Color.FromArgb(120, 53, 15),
                System.Drawing.Color.FromArgb(255, 251, 235));

            BuildColumn(pnlTesting, lblTesting, flpTesting, lblBadgeTesting,
                "🧪  Testing",
                System.Drawing.Color.FromArgb(221, 214, 254),
                System.Drawing.Color.FromArgb(76, 29, 149),
                System.Drawing.Color.FromArgb(245, 243, 255));

            BuildColumn(pnlFailed, lblFailed, flpFailed, lblBadgeFailed,
                "❌  Failed",
                System.Drawing.Color.FromArgb(254, 202, 202),
                System.Drawing.Color.FromArgb(127, 29, 29),
                System.Drawing.Color.FromArgb(254, 242, 242));

            BuildColumn(pnlDone, lblDone, flpDone, lblBadgeDone,
                "✅  Done",
                System.Drawing.Color.FromArgb(167, 243, 208),
                System.Drawing.Color.FromArgb(6, 78, 59),
                System.Drawing.Color.FromArgb(236, 253, 245));
        }

        private void BuildColumn(
            Panel pnlColumn,
            Label lblColName,
            DoubleBufferedFlowLayoutPanel flp,
            Label lblBadge,
            string text,
            System.Drawing.Color headerBg,
            System.Drawing.Color headerFg,
            System.Drawing.Color bodyBg)
        {
            pnlColumn.BackColor = bodyBg;
            pnlColumn.Padding = new Padding(0);

            // Header panel
            var pnlColHeader = new Panel
            {
                BackColor = headerBg,
                Dock = DockStyle.Top,
                Height = 44,
                Name = "pnlColHeader"
            };

            lblColName.AutoSize = false;
            lblColName.BackColor = System.Drawing.Color.Transparent;
            lblColName.Dock = DockStyle.Fill;
            lblColName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblColName.ForeColor = headerFg;
            lblColName.Padding = new Padding(10, 0, 40, 0);
            lblColName.Text = text;
            lblColName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            lblBadge.AutoSize = false;
            lblBadge.BackColor = System.Drawing.Color.FromArgb(80, 0, 0, 0);
            lblBadge.ForeColor = System.Drawing.Color.White;
            lblBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            lblBadge.Location = new System.Drawing.Point(0, 12);
            lblBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBadge.Size = new System.Drawing.Size(30, 20);
            lblBadge.Text = "0";
            lblBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblBadge.Paint += LblBadge_Paint;

            // Lambda SizeChanged — KHÔNG được phép trong Designer, đặt ở đây
            pnlColHeader.SizeChanged += (_, _) =>
                lblBadge.Left = pnlColHeader.Width - lblBadge.Width - 8;

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

        public frmKanban(
            ITaskService    taskService,
            IProjectService projectService,
            IUserService    userService,
            int             projectId)
#pragma warning disable CS0618
            : this()
#pragma warning restore CS0618
        {
            _taskService    = taskService    ?? throw new ArgumentNullException(nameof(taskService));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _userService    = userService    ?? throw new ArgumentNullException(nameof(userService));
            _projectId      = projectId;
        }

        // ── Form Init ─────────────────────────────────────────────────────────
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_taskService == null || DesignMode) return;

            var title      = $"🗂️  Kanban Board — Dự án #{_projectId}";
            this.Text      = title;
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

        // ── Toast setup ───────────────────────────────────────────────────────
        private void SetupToastTimer()
        {
            _toastTimer          = new System.Windows.Forms.Timer { Interval = 3000 };
            _toastTimer.Tick    += (_, _) =>
            {
                _toastTimer.Stop();
                panelToast.Visible = false;
            };
        }

        private void ShowToast(string message, bool isError = false)
        {
            _toastTimer?.Stop();
            lblToast.Text           = message;
            panelToast.BackColor    = isError
                ? UIHelper.ColorDanger
                : UIHelper.ColorSuccess;
            panelToast.Visible      = true;
            _toastTimer?.Start();
        }

        // ── Filter buttons ────────────────────────────────────────────────────
        private void WireFilterButtons()
        {
            btnFilterAll.Click    += (_, _) => ApplyFilter(FilterMode.All);
            btnFilterMine.Click   += (_, _) => ApplyFilter(FilterMode.Mine);
            btnFilterOverdue.Click += (_, _) => ApplyFilter(FilterMode.Overdue);
        }

        private void ApplyFilter(FilterMode mode)
        {
            _currentFilter = mode;

            // Cập nhật trạng thái nút filter (active / inactive)
            UIHelper.StyleButton(btnFilterAll,    mode == FilterMode.All    ? UIHelper.ButtonVariant.Primary   : UIHelper.ButtonVariant.Secondary);
            UIHelper.StyleButton(btnFilterMine,   mode == FilterMode.Mine   ? UIHelper.ButtonVariant.Info      : UIHelper.ButtonVariant.Secondary);
            UIHelper.StyleButton(btnFilterOverdue, mode == FilterMode.Overdue ? UIHelper.ButtonVariant.Danger   : UIHelper.ButtonVariant.Secondary);

            // Ẩn/hiện card theo filter — KHÔNG reload DB
            foreach (var panel in GetAllColumns())
            {
                foreach (Control ctrl in panel.Controls)
                {
                    if (ctrl is ucTaskCard card)
                        ctrl.Visible = IsCardVisible(card);
                }
            }

            UpdateAllColumnBadges();
        }

        private bool IsCardVisible(ucTaskCard card)
        {
            // Lấy TaskItem từ tag (được gán trong Bind helper bên dưới)
            if (card.Tag is not TaskItem task) return true;

            return _currentFilter switch
            {
                FilterMode.Mine    => task.AssignedToId == AppSession.UserId,
                FilterMode.Overdue => task.DueDate.HasValue
                                      && task.DueDate.Value < DateTime.UtcNow
                                      && !task.IsCompleted,
                _                  => true  // All
            };
        }

        // ── Load data ─────────────────────────────────────────────────────────
        private async Task LoadTasksAsync()
        {
            SetStatus("⏳  Đang tải dữ liệu...");
            ClearAllColumns();

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
        }

        /// <summary>
        /// Factory: tạo ucTaskCard, bind data, subscribe events.
        /// TaskItem được lưu trong card.Tag để IsCardVisible() dùng khi filter.
        /// </summary>
        private ucTaskCard CreateCard(TaskItem task)
        {
            var card = new ucTaskCard { Margin = new Padding(6), Tag = task };
            card.Bind(task);
            card.StatusChanged    += TaskCard_StatusChanged;
            card.CardDoubleClicked += TaskCard_DoubleClicked;
            card.TitleChanged     += TaskCard_TitleChanged;    // ← INLINE EDIT
            return card;
        }

        private void ClearAllColumns()
        {
            foreach (var panel in GetAllColumns())
            {
                foreach (Control ctrl in panel.Controls)
                {
                    if (ctrl is ucTaskCard card)
                    {
                        card.StatusChanged    -= TaskCard_StatusChanged;
                        card.CardDoubleClicked -= TaskCard_DoubleClicked;
                        card.TitleChanged     -= TaskCard_TitleChanged;
                    }
                }
                panel.Controls.Clear();
            }
        }

        // ── Event: Status changed (context-menu) ─────────────────────────────
        private async void TaskCard_StatusChanged(object? sender, StatusChangedEventArgs e)
        {
            if (sender is not ucTaskCard card) return;

            try
            {
                var (success, message) = await _taskService!.UpdateStatusAsync(
                    e.TaskId, e.NewStatusId, AppSession.UserId, AppSession.Roles);

                if (!success)
                {
                    ShowToast($"⚠  {message}", isError: true);
                    return;
                }

                if (card.Parent is FlowLayoutPanel src) src.Controls.Remove(card);

                // Cập nhật Tag để filter vẫn hoạt động đúng
                if (card.Tag is TaskItem task) task.StatusId = e.NewStatusId;

                card.UpdateBoundStatus(e.NewStatusId);
                MoveCardToStatusPanel(card, e.NewStatusId);

                UpdateAllColumnBadges();
                ShowToast($"✔  Đã chuyển sang {Core.Constants.WorkflowConstants.GetStatusName(e.NewStatusId)}");
            }
            catch (Exception ex)
            {
                ShowToast($"⚠  Lỗi: {ex.Message}", isError: true);
            }
        }

        // ── Event: Double-click → mở frmTaskEdit ─────────────────────────────
        private async void TaskCard_DoubleClicked(object? sender, int taskId)
        {
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
        }

        // ── Event: Inline title changed ───────────────────────────────────────
        /// <summary>
        /// Người dùng double-click lblTitle trên card và đã gõ tiêu đề mới.
        /// Gọi UpdateTaskAsync rồi thông báo Toast.
        /// </summary>
        private async void TaskCard_TitleChanged(object? sender, (int TaskId, string NewTitle) e)
        {
            if (_taskService == null) return;

            try
            {
                // Lấy task đầy đủ để update (cần set lại tất cả required fields)
                var task = await _taskService.GetByIdAsync(e.TaskId);
                if (task == null)
                {
                    ShowToast("⚠  Không tìm thấy task.", isError: true);
                    return;
                }

                task.Title     = e.NewTitle;
                task.UpdatedAt = DateTime.UtcNow;

                var (success, message) = await _taskService.UpdateTaskAsync(task);

                if (success)
                {
                    // Cập nhật Tag của card để reload filter không bị stale
                    if (sender is ucTaskCard card) card.Tag = task;
                    ShowToast($"✔  Đã lưu: \"{e.NewTitle}\"");
                }
                else
                {
                    // Khôi phục tiêu đề cũ nếu lưu thất bại
                    if (sender is ucTaskCard card)
                    {
                        card.Bind(task);    // rebind với dữ liệu gốc
                    }
                    ShowToast($"⚠  {message}", isError: true);
                }
            }
            catch (Exception ex)
            {
                ShowToast($"⚠  Lỗi lưu tiêu đề: {ex.Message}", isError: true);
            }
        }

        // ── Refresh button ────────────────────────────────────────────────────
        private async void btnRefresh_Click(object? sender, EventArgs e)
            => await LoadTasksAsync();

        // ── Drag-drop ─────────────────────────────────────────────────────────
        private void EnableDragDropOnAllColumns()
        {
            foreach (var panel in GetAllColumns())
            {
                panel.AllowDrop  = true;
                panel.DragEnter += Column_DragEnter;
                panel.DragDrop  += Column_DragDrop;
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

            // Cập nhật Tag
            if (card.Tag is TaskItem task) task.StatusId = newStatusId;

            card.UpdateBoundStatus(newStatusId);
            targetPanel.Controls.Add(card);

            // Áp dụng lại filter nếu đang lọc
            card.Visible = IsCardVisible(card);

            UpdateAllColumnBadges();
            ShowToast($"✔  Đã chuyển sang {Core.Constants.WorkflowConstants.GetStatusName(newStatusId)}");
        }

        // ── Column badge update ───────────────────────────────────────────────
        /// <summary>
        /// Đếm số card đang hiển thị (Visible = true) trong mỗi cột và cập nhật label badge.
        /// </summary>
        private void UpdateAllColumnBadges()
        {
            lblBadgeTodo.Text       = CountVisibleCards(flpTodo).ToString();
            lblBadgeInProgress.Text = CountVisibleCards(flpInProgress).ToString();
            lblBadgeReview.Text     = CountVisibleCards(flpReview).ToString();
            lblBadgeTesting.Text    = CountVisibleCards(flpTesting).ToString();
            lblBadgeFailed.Text     = CountVisibleCards(flpFailed).ToString();
            lblBadgeDone.Text       = CountVisibleCards(flpDone).ToString();
        }

        private static int CountVisibleCards(FlowLayoutPanel panel)
            => panel.Controls.OfType<ucTaskCard>().Count(c => c.Visible);

        // ── Routing helpers ───────────────────────────────────────────────────
        private void MoveCardToStatusPanel(ucTaskCard card, int statusId)
        {
            var target = GetPanelByStatusId(statusId);
            if (target != null)
            {
                target.Controls.Add(card);
                // Áp dụng filter hiện tại ngay khi thêm
                card.Visible = IsCardVisible(card);
            }
        }

        private DoubleBufferedFlowLayoutPanel? GetPanelByStatusId(int statusId) => statusId switch
        {
            StatusCreated or StatusAssigned  => flpTodo,
            StatusInProgress                 => flpInProgress,
            StatusReview1 or StatusReview2   => flpReview,
            StatusInTest                     => flpTesting,
            StatusFailed                     => flpFailed,
            StatusApproved or StatusResolved
                           or StatusClosed   => flpDone,
            _                                => null
        };

        private int GetPrimaryStatusForPanel(DoubleBufferedFlowLayoutPanel panel)
        {
            if (panel == flpTodo)       return StatusCreated;
            if (panel == flpInProgress) return StatusInProgress;
            if (panel == flpReview)     return StatusReview1;
            if (panel == flpTesting)    return StatusInTest;
            if (panel == flpFailed)     return StatusFailed;
            if (panel == flpDone)       return StatusResolved;
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

        // ── Status bar ────────────────────────────────────────────────────────
        private void SetStatus(string msg)
        {
            if (lblStatus != null && !lblStatus.IsDisposed)
                lblStatus.Text = msg;
        }

        // ── Dispose ───────────────────────────────────────────────────────────
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _toastTimer?.Stop();
            _toastTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }

    // ── Double-buffered FlowLayoutPanel (giữ nguyên) ──────────────────────────
    public class DoubleBufferedFlowLayoutPanel : FlowLayoutPanel
    {
        public DoubleBufferedFlowLayoutPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw   = true;
        }
    }
}
