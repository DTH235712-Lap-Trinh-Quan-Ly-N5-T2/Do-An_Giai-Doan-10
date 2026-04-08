using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    /// <summary>
    /// Màn hình chào sau khi đăng nhập thành công.
    /// Hiển thị: tên user, vai trò, thống kê nhanh (số liệu thật từ DB).
    /// Là MDI Child đầu tiên tự động mở khi vào frmMain.
    /// </summary>
    public partial class frmHome : BaseForm
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        // ── Constructor rỗng: CHỈ dùng cho WinForms Designer ─────────────────
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

        // ── Tất cả UIHelper / font / color / local-var logic tách khỏi Designer
        private void ApplyClientStyles()
        {
            this.BackColor = UIHelper.ColorBackground;
            this.Font = UIHelper.FontBase;

            // ── panelHeader ───────────────────────────────────────────────────
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelAccentLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;

            // ── panelBody / panelWelcome ──────────────────────────────────────
            panelBody.BackColor = UIHelper.ColorBackground;
            panelWelcome.BackColor = UIHelper.ColorHeaderBg;

            lblGreeting.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblGreeting.ForeColor = System.Drawing.Color.White;

            lblRole.Font = UIHelper.FontBase;
            lblRole.ForeColor = UIHelper.ColorSubtitle;

            lblLastLogin.Font = UIHelper.FontSmall;
            lblLastLogin.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            // ── panelStats / lblNote ──────────────────────────────────────────
            panelStats.BackColor = UIHelper.ColorBackground;

            lblNote.Font = UIHelper.FontSmall;
            lblNote.ForeColor = UIHelper.ColorMuted;

            // ── Card size/margin dùng chung ───────────────────────────────────
            var cardSize = new System.Drawing.Size(240, 162);
            var cardMargin = new Padding(10, 8, 10, 8);

            // ── Card 1 — Dự án đang chạy (Blue) ──────────────────────────────
            panelCard1.Size = cardSize;
            panelCard1.Margin = cardMargin;

            panelCard1Top.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);

            lblCard1Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            lblCard1Icon.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);

            lblCard1Title.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            lblCard1Title.ForeColor = UIHelper.ColorMuted;

            lblStatProjects.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            lblStatProjects.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);

            lblCard1Sub.Font = UIHelper.FontSmall;
            lblCard1Sub.ForeColor = UIHelper.ColorSubtitle;

            // ── Card 2 — Công việc của tôi (Green) ───────────────────────────
            panelCard2.Size = cardSize;
            panelCard2.Margin = cardMargin;

            panelCard2Top.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);

            lblCard2Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            lblCard2Icon.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);

            lblCard2Title.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            lblCard2Title.ForeColor = UIHelper.ColorMuted;

            lblStatTasks.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            lblStatTasks.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);

            lblCard2Sub.Font = UIHelper.FontSmall;
            lblCard2Sub.ForeColor = UIHelper.ColorSubtitle;

            // ── Card 3 — Quá hạn (Red) ────────────────────────────────────────
            panelCard3.Size = cardSize;
            panelCard3.Margin = cardMargin;

            panelCard3Top.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);

            lblCard3Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            lblCard3Icon.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);

            lblCard3Title.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            lblCard3Title.ForeColor = UIHelper.ColorMuted;

            lblStatOverdue.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            lblStatOverdue.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);

            lblCard3Sub.Font = UIHelper.FontSmall;
            lblCard3Sub.ForeColor = UIHelper.ColorSubtitle;

            // ── Card 4 — Hoàn thành tháng này (Purple) ───────────────────────
            panelCard4.Size = cardSize;
            panelCard4.Margin = cardMargin;

            panelCard4Top.BackColor = System.Drawing.Color.FromArgb(124, 58, 237);

            lblCard4Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            lblCard4Icon.ForeColor = System.Drawing.Color.FromArgb(124, 58, 237);

            lblCard4Title.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            lblCard4Title.ForeColor = UIHelper.ColorMuted;

            lblStatDone.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            lblStatDone.ForeColor = System.Drawing.Color.FromArgb(124, 58, 237);

            lblCard4Sub.Font = UIHelper.FontSmall;
            lblCard4Sub.ForeColor = UIHelper.ColorSubtitle;
            // DateTime.Now không được phép trong Designer — gán ở đây
            lblCard4Sub.Text = $"task hoàn thành T{DateTime.Now.Month}";
        }

        // ─────────────────────────────────────────────────────────────────────
        // Phần còn lại: giữ nguyên hoàn toàn
        // ─────────────────────────────────────────────────────────────────────

        private void LoadWelcomeInfo()
        {
            var hour = DateTime.Now.Hour;
            var greeting = hour < 12 ? "Chào buổi sáng" :
                           hour < 18 ? "Chào buổi chiều" : "Chào buổi tối";

            lblHeader.Text = $"🏠  Trang chủ — {AppSession.FullName}";
            lblGreeting.Text = $"{greeting}, {AppSession.FullName}! 👋";
            lblRole.Text = $"Vai trò: {string.Join(", ", AppSession.Roles)}";
            lblLastLogin.Text = $"Đăng nhập lúc: {DateTime.Now:HH:mm  dd/MM/yyyy}";

            lblStatProjects.Text = "...";
            lblStatTasks.Text = "...";
            lblStatOverdue.Text = "...";
            lblStatDone.Text = "...";
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadStatsAsync();
        }

        private async Task LoadStatsAsync()
        {
            try
            {
                bool isManager = AppSession.IsManager;
                int userId = AppSession.UserId;

                var projects = await _projectService.GetProjectsForUserAsync(userId, isManager);
                var runningProjects = projects.Count(p => p.Status == "InProgress");
                lblStatProjects.Text = runningProjects.ToString();

                var myTasks = await _taskService.GetMyTasksAsync(userId);
                lblStatTasks.Text = myTasks.Count.ToString();

                var overdue = await _taskService.GetOverdueTasksAsync();
                var overdueCount = isManager
                    ? overdue.Count
                    : overdue.Count(t => t.AssignedToId == userId);
                lblStatOverdue.Text = overdueCount.ToString();

                var thisMonth = DateTime.Now;
                var doneThisMonth = myTasks.Count(t =>
                    t.IsCompleted &&
                    t.CompletedAt.HasValue &&
                    t.CompletedAt.Value.Month == thisMonth.Month &&
                    t.CompletedAt.Value.Year == thisMonth.Year);
                lblStatDone.Text = doneThisMonth.ToString();

                // Cập nhật sub-text card 4 theo tháng thực
                lblCard4Sub.Text = $"task hoàn thành T{thisMonth.Month}";

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