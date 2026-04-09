// ============================================================
//  frmMyTasks.cs
//  TaskFlowManagement.WinForms.Forms
// ============================================================
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    /// <summary>
    /// Form công việc cá nhân — hiển thị task liên quan đến user đang đăng nhập.
    /// Tab 1: task được giao | Tab 2: cần Review | Tab 3: cần Test.
    /// Developer chỉ thấy Tab 1. Manager/Admin thấy cả 3.
    /// </summary>
    public partial class frmMyTasks : BaseForm
    {
        // ── Dependencies ──────────────────────────────────────────────────────
        private readonly ITaskService _taskService = null!;
        private readonly IProjectService _projectService = null!;
        private readonly IUserService _userService = null!;

        // ── Constructors ──────────────────────────────────────────────────────

        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmMyTasks()
        {
            InitializeComponent();
        }

        public frmMyTasks(
            ITaskService taskService,
            IProjectService projectService,
            IUserService userService)
#pragma warning disable CS0618
            : this()
#pragma warning restore CS0618
        {
            _taskService = taskService;
            _projectService = projectService;
            _userService = userService;

            ApplyClientStyles();

            _taskService.TaskDataChanged += OnTaskDataChanged;
        }

        // ── Áp dụng toàn bộ style, font, màu sắc, cấu hình grid ─────────────
        private void ApplyClientStyles()
        {
            this.Font = UIHelper.FontBase;
            this.BackColor = UIHelper.ColorBackground;

            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelAccentLine.BackColor = UIHelper.ColorAccent;
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;

            panelFilter.BackColor = UIHelper.ColorBackground;
            lblUser.Font = UIHelper.FontBase;
            lblUser.ForeColor = UIHelper.ColorPrimary;

            UIHelper.StyleButton(btnRefresh, UIHelper.ButtonVariant.Secondary);

            panelStatus.BackColor = UIHelper.ColorHeaderBg;
            lblStatus.Font = UIHelper.FontSmall;
            lblStatus.ForeColor = UIHelper.ColorSubtitle;

            tabControl.Font = UIHelper.FontBase;

            // Cấu hình UI GridView
            ConfigureGrid(dgvMyTasks);
            ConfigureGrid(dgvReview);
            ConfigureGrid(dgvTesting);
        }

        // ── Cấu hình một DataGridView theo chuẩn chung ───────────────────────
        private static void ConfigureGrid(DataGridView dgv)
        {
            UIHelper.StyleDataGridView(dgv);
            UIHelper.ApplyAlternateRowColors(dgv);
            dgv.RowTemplate.Height = 35;

            var colId = new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                Width = 50,
                Visible = false,
            };
            var colTitle = new DataGridViewTextBoxColumn
            {
                Name = "colTitle",
                HeaderText = "Tiêu đề công việc",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 200,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft },
            };
            var colProject = new DataGridViewTextBoxColumn
            {
                Name = "colProject",
                HeaderText = "Dự án",
                Width = 150,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft },
            };
            var colPriority = new DataGridViewTextBoxColumn
            {
                Name = "colPriority",
                HeaderText = "Ưu tiên",
                Width = 85,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
            };
            var colStatus = new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Trạng thái",
                Width = 130,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
            };
            var colProgress = new DataGridViewTextBoxColumn
            {
                Name = "colProgress",
                HeaderText = "%",
                Width = 55,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight },
            };
            var colDueDate = new DataGridViewTextBoxColumn
            {
                Name = "colDueDate",
                HeaderText = "Hạn chót",
                Width = 105,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
            };

            dgv.Columns.AddRange(new DataGridViewColumn[]
                { colId, colTitle, colProject, colPriority, colStatus, colProgress, colDueDate });
        }

        // ── Form Load ─────────────────────────────────────────────────────────
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var title = $"📋  Công việc của tôi — {AppSession.FullName}";
            this.Text = title;
            lblHeader.Text = title;
            lblUser.Text = $"Đang hiển thị công việc của: {AppSession.FullName} ({AppSession.Username})";

            ApplyRolePermissions();
            await LoadAllTabsAsync();
        }

        // ── Phân quyền hiển thị tab ───────────────────────────────────────────
        private void ApplyRolePermissions()
        {
            bool canReviewOrTest = AppSession.IsManager;
            tabReview.Parent = canReviewOrTest ? tabControl : null;
            tabTesting.Parent = canReviewOrTest ? tabControl : null;
        }

        // ── Tải dữ liệu song song cho cả 3 tab ───────────────────────────────
        private async Task LoadAllTabsAsync()
        {
            SetStatus("⏳  Đang tải...");

            try
            {
                var tMine = _taskService.GetMyTasksAsync(AppSession.UserId);
                var tReview1 = _taskService.GetTasksForReviewer1Async(AppSession.UserId);
                var tReview2 = _taskService.GetTasksForReviewer2Async(AppSession.UserId);
                var tTest = _taskService.GetTasksForTesterAsync(AppSession.UserId);

                await Task.WhenAll(tMine, tReview1, tReview2, tTest);

                var reviewTasks = tReview1.Result.Concat(tReview2.Result).ToList();

                BindGrid(dgvMyTasks, tMine.Result);
                BindGrid(dgvReview, reviewTasks);
                BindGrid(dgvTesting, tTest.Result);

                tabMyTasks.Text = $"📋  Được giao ({tMine.Result.Count})";
                tabReview.Text = $"🔍  Review ({reviewTasks.Count})";
                tabTesting.Text = $"🧪  Testing ({tTest.Result.Count})";

                int total = tMine.Result.Count + reviewTasks.Count + tTest.Result.Count;
                SetStatus($"Tổng cộng {total} công việc liên quan đến bạn.");
            }
            catch (Exception ex)
            {
                SetStatus("⚠  Lỗi tải dữ liệu.");
                MessageBox.Show(
                    "Không thể tải dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Bind dữ liệu vào grid ─────────────────────────────────────────────
        private static void BindGrid(DataGridView dgv, List<TaskItem> items)
        {
            dgv.Rows.Clear();

            foreach (var t in items)
            {
                var due = t.DueDate.HasValue
                    ? t.DueDate.Value.ToLocalTime().ToString("dd/MM/yyyy")
                    : "—";

                int idx = dgv.Rows.Add(
                    t.Id,
                    t.Title,
                    t.Project?.Name ?? "—",
                    t.Priority?.Name ?? "—",
                    t.Status?.Name ?? "—",
                    $"{t.ProgressPercent}%",
                    due);

                UIHelper.ApplyTaskRowStyle(
                    dgv.Rows[idx],
                    t.Status?.Name,
                    t.IsCompleted,
                    t.DueDate);
            }
        }

        // ── Sự kiện ───────────────────────────────────────────────────────────
        private async void btnRefresh_Click(object sender, EventArgs e)
            => await LoadAllTabsAsync();

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (sender is not DataGridView dgv) return;

            var cell = dgv.Rows[e.RowIndex].Cells["colId"].Value;
            if (cell == null) return;

            int taskId = (int)cell;

            using var dlg = new frmTaskEdit(_taskService, _projectService, _userService, taskId);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                _ = LoadAllTabsAsync();
        }

        // ── Lắng nghe thay đổi dữ liệu từ form khác ──────────────────────────
        private async void OnTaskDataChanged(object? sender, EventArgs e)
        {
            if (this.IsHandleCreated && !this.IsDisposed)
                this.Invoke((MethodInvoker)(async () => await LoadAllTabsAsync()));
        }

        // ── Thanh trạng thái ─────────────────────────────────────────────────
        private void SetStatus(string msg)
        {
            if (lblStatus != null && !lblStatus.IsDisposed)
                lblStatus.Text = msg;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _taskService.TaskDataChanged -= OnTaskDataChanged;
            base.OnFormClosed(e);
        }
    }
}