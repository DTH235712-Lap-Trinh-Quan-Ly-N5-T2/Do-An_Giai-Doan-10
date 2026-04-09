using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    /// <summary>
    /// Màn hình trang chủ Dashboard sau khi đăng nhập thành công.
    /// Hiển thị: tên user, vai trò, 4 thẻ thống kê nhanh (số liệu thật từ DB).
    /// Là MDI Child đầu tiên tự động mở khi vào frmMain.
    /// </summary>
    public partial class frmHome : BaseForm
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        // ── Constructor rỗng: CHỈ dùng cho WinForms Designer ─────────────────
        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmHome()
        {
            InitializeComponent();
        }

        // ── DI Constructor: dùng khi chạy thật ───────────────────────────────
        public frmHome(IProjectService projectService, ITaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;

            InitializeComponent();
            ApplyClientStyles();
            LoadWelcomeInfo();
        }

        // ════════════════════════════════════════════════════════════════════════
        // ApplyClientStyles — TẤT CẢ màu sắc, font, style gọi qua UIHelper
        // TUYỆT ĐỐI không hard-code hex/RGB ở đây
        // ════════════════════════════════════════════════════════════════════════
        private void ApplyClientStyles()
        {
            this.BackColor = UIHelper.ColorBackground;
            this.Font = UIHelper.FontBase;

            // ── Header ────────────────────────────────────────────────────────
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelAccentLine.BackColor = UIHelper.ColorPrimary;
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;

            // ── Welcome panel ─────────────────────────────────────────────────
            panelBody.BackColor = UIHelper.ColorBackground;
            panelWelcome.BackColor = UIHelper.ColorHeaderBg;
            tblWelcome.BackColor = System.Drawing.Color.Transparent;

            lblGreeting.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblGreeting.ForeColor = UIHelper.ColorHeaderFg;

            lblRole.Font = UIHelper.FontBase;
            lblRole.ForeColor = UIHelper.ColorSubtitle;        // slate-400 — đọc được trên nền ColorHeaderBg tối

            // FIX BUG: lblLastLogin trước đây dùng Color.FromArgb hardcode → không đọc được trên nền tối
            lblLastLogin.Font = UIHelper.FontSmall;
            lblLastLogin.ForeColor = UIHelper.ColorSubtitle;   // slate-400 — đọc được trên nền ColorHeaderBg tối

            // ── Stats panel & note ────────────────────────────────────────────
            panelStats.BackColor = UIHelper.ColorBackground;
            lblNote.Font = UIHelper.FontSmall;
            lblNote.ForeColor = UIHelper.ColorMuted;

            // ── Kích thước & margin chung cho 4 cards ────────────────────────
            var cardSize = new System.Drawing.Size(248, 158);
            var cardMargin = new Padding(10, 8, 10, 8);

            // ── Card 1 — Dự án đang chạy (Primary Blue) ──────────────────────
            panelCard1.BackColor = UIHelper.ColorSurface;
            panelCard1.Size = cardSize;
            panelCard1.Margin = cardMargin;

            panelCard1Top.BackColor = UIHelper.ColorPrimary;
            tblCard1.BackColor = System.Drawing.Color.Transparent;

            lblCard1Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            lblCard1Icon.ForeColor = UIHelper.ColorPrimary;

            lblCard1Title.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            lblCard1Title.ForeColor = UIHelper.ColorMuted;

            lblStatProjects.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold);
            lblStatProjects.ForeColor = UIHelper.ColorPrimary;

            lblCard1Sub.Font = UIHelper.FontSmall;
            lblCard1Sub.ForeColor = UIHelper.ColorMuted;

            // ── Card 2 — Công việc của tôi (Success Green) ───────────────────
            panelCard2.BackColor = UIHelper.ColorSurface;
            panelCard2.Size = cardSize;
            panelCard2.Margin = cardMargin;

            panelCard2Top.BackColor = UIHelper.ColorSuccess;
            tblCard2.BackColor = System.Drawing.Color.Transparent;

            lblCard2Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            lblCard2Icon.ForeColor = UIHelper.ColorSuccess;

            lblCard2Title.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            lblCard2Title.ForeColor = UIHelper.ColorMuted;

            lblStatTasks.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold);
            lblStatTasks.ForeColor = UIHelper.ColorSuccess;

            lblCard2Sub.Font = UIHelper.FontSmall;
            lblCard2Sub.ForeColor = UIHelper.ColorMuted;

            // ── Card 3 — Quá hạn (Danger Red) ────────────────────────────────
            panelCard3.BackColor = UIHelper.ColorSurface;
            panelCard3.Size = cardSize;
            panelCard3.Margin = cardMargin;

            panelCard3Top.BackColor = UIHelper.ColorDanger;
            tblCard3.BackColor = System.Drawing.Color.Transparent;

            lblCard3Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            lblCard3Icon.ForeColor = UIHelper.ColorDanger;

            lblCard3Title.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            lblCard3Title.ForeColor = UIHelper.ColorMuted;

            lblStatOverdue.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold);
            lblStatOverdue.ForeColor = UIHelper.ColorDanger;

            lblCard3Sub.Font = UIHelper.FontSmall;
            lblCard3Sub.ForeColor = UIHelper.ColorMuted;

            // ── Card 4 — Hoàn thành tháng này (Warning / Accent) ─────────────
            panelCard4.BackColor = UIHelper.ColorSurface;
            panelCard4.Size = cardSize;
            panelCard4.Margin = cardMargin;

            panelCard4Top.BackColor = UIHelper.ColorWarning;
            tblCard4.BackColor = System.Drawing.Color.Transparent;

            lblCard4Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            lblCard4Icon.ForeColor = UIHelper.ColorWarning;

            lblCard4Title.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            lblCard4Title.ForeColor = UIHelper.ColorMuted;

            lblStatDone.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold);
            lblStatDone.ForeColor = UIHelper.ColorWarning;

            lblCard4Sub.Font = UIHelper.FontSmall;
            lblCard4Sub.ForeColor = UIHelper.ColorMuted;

            // DateTime.Now không được phép trong Designer — gán ở đây
            lblCard4Sub.Text = $"task hoàn thành T{DateTime.Now.Month}";
        }

        // ════════════════════════════════════════════════════════════════════════
        // LoadWelcomeInfo — gán text động dựa trên AppSession
        // ════════════════════════════════════════════════════════════════════════
        private void LoadWelcomeInfo()
        {
            var hour = DateTime.Now.Hour;
            var greeting = hour < 12 ? "Chào buổi sáng" :
                           hour < 18 ? "Chào buổi chiều" : "Chào buổi tối";

            lblHeader.Text = $"🏠  Trang chủ — {AppSession.FullName}";
            lblGreeting.Text = $"{greeting}, {AppSession.FullName}! 👋";
            lblRole.Text = $"Vai trò: {string.Join(", ", AppSession.Roles)}";
            lblLastLogin.Text = $"Đăng nhập lúc: {DateTime.Now:HH:mm  dd/MM/yyyy}";

            // Placeholder trong khi chờ async load
            lblStatProjects.Text = "...";
            lblStatTasks.Text = "...";
            lblStatOverdue.Text = "...";
            lblStatDone.Text = "...";
        }

        // ════════════════════════════════════════════════════════════════════════
        // OnLoad — khởi động load số liệu bất đồng bộ
        // ════════════════════════════════════════════════════════════════════════
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadStatsAsync();
        }

        // ════════════════════════════════════════════════════════════════════════
        // LoadStatsAsync — truy vấn DB và cập nhật 4 thẻ thống kê
        // ════════════════════════════════════════════════════════════════════════
        private async Task LoadStatsAsync()
        {
            try
            {
                bool isManager = AppSession.IsManager;
                int userId = AppSession.UserId;

                // Card 1 — Dự án đang InProgress
                var projects = await _projectService.GetProjectsForUserAsync(userId, isManager);
                var runningCount = projects.Count(p => p.Status == "InProgress");
                lblStatProjects.Text = runningCount.ToString();

                // Card 2 — Task được giao cho tôi
                var myTasks = await _taskService.GetMyTasksAsync(userId);
                lblStatTasks.Text = myTasks.Count.ToString();

                // Card 3 — Task quá hạn
                var overdue = await _taskService.GetOverdueTasksAsync();
                var overdueCount = isManager
                    ? overdue.Count
                    : overdue.Count(t => t.AssignedToId == userId);
                lblStatOverdue.Text = overdueCount.ToString();

                // Card 4 — Task hoàn thành trong tháng hiện tại
                var thisMonth = DateTime.Now;
                var doneThisMonth = myTasks.Count(t =>
                    t.IsCompleted &&
                    t.CompletedAt.HasValue &&
                    t.CompletedAt.Value.Month == thisMonth.Month &&
                    t.CompletedAt.Value.Year == thisMonth.Year);
                lblStatDone.Text = doneThisMonth.ToString();

                // Cập nhật sub-text card 4 với tháng thực
                lblCard4Sub.Text = $"task hoàn thành T{thisMonth.Month}";

                // Cập nhật ghi chú tổng hợp
                lblNote.Text = $"ℹ️  Cập nhật lúc {DateTime.Now:HH:mm}  —  {projects.Count} dự án tổng";
            }
            catch
            {
                lblStatProjects.Text = "—";
                lblStatTasks.Text = "—";
                lblStatOverdue.Text = "—";
                lblStatDone.Text = "—";
                lblNote.Text = "ℹ️  Không thể tải số liệu. Kiểm tra kết nối database.";
            }
        }
    }
}