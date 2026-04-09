using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmProjects : BaseForm
    {
        // ── Dependencies ──────────────────────────────────────────
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        private readonly ICustomerRepository _customerRepo;

        // ── State ─────────────────────────────────────────────────
        private List<Project> _allProjects = new();
        private Project? _selectedProject;

        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmProjects()
        {
            InitializeComponent();
        }

        public frmProjects(
            IProjectService projectService,
            IUserService userService,
            ITaskService taskService,
            ICustomerRepository customerRepo)
        {
            _projectService = projectService;
            _userService = userService;
            _taskService = taskService;
            _customerRepo = customerRepo;

            InitializeComponent();
            ApplyClientStyles();
            SetupPermissions();
        }

        // ── Khởi tạo giao diện ────────────────────────────────────

        private void ApplyClientStyles()
        {
            // Header
            panelTop.BackColor = UIHelper.ColorHeaderBg;
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;

            // Toolbar — nền nhạt, chứa cả search lẫn action buttons
            panelToolbar.BackColor = UIHelper.ColorBackground;

            txtSearch.Font = UIHelper.FontSmall;

            UIHelper.StyleFilterCombo(cboFilterStatus);
            cboFilterStatus.Items.Clear();
            cboFilterStatus.Items.AddRange(new object[]
            {
                "— Tất cả trạng thái —",
                "NotStarted", "InProgress", "OnHold", "Completed", "Cancelled"
            });
            cboFilterStatus.SelectedIndex = 0;

            UIHelper.StyleButton(btnRefresh, UIHelper.ButtonVariant.Secondary);

            // Nhóm nút chức năng
            UIHelper.StyleButton(btnAdd, UIHelper.ButtonVariant.Primary);
            UIHelper.StyleButton(btnEdit, UIHelper.ButtonVariant.Success);
            UIHelper.StyleButton(btnDelete, UIHelper.ButtonVariant.Danger);
            UIHelper.StyleButton(btnStatus, UIHelper.ButtonVariant.Warning);
            UIHelper.StyleButton(btnMembers, UIHelper.ButtonVariant.Purple);
            UIHelper.StyleButton(btnDetail, UIHelper.ButtonVariant.Slate);
            UIHelper.StyleButton(btnKanban, UIHelper.ButtonVariant.Info);

            // DataGridView
            UIHelper.StyleDataGridView(dgvProjects);
            UIHelper.ApplyAlternateRowColors(dgvProjects);

            colMembers.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDeadline.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStartDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colBudget.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Status bar
            panelStatus.BackColor = UIHelper.ColorHeaderBg;
            lblStatus.Font = UIHelper.FontSmall;
            lblStatus.ForeColor = UIHelper.ColorSubtitle;
        }

        // ── Permissions ───────────────────────────────────────────

        private void SetupPermissions()
        {
            bool canEdit = AppSession.IsManager;
            btnAdd.Visible = canEdit;
            btnEdit.Visible = canEdit;
            btnDelete.Visible = canEdit;
            btnStatus.Visible = canEdit;
        }

        // ── Form Load ─────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadProjectsAsync();
        }

        // ── Data Loading ──────────────────────────────────────────

        private async Task LoadProjectsAsync()
        {
            SetStatus("⏳ Đang tải...");
            try
            {
                _allProjects = await _projectService.GetProjectsForUserAsync(
                    AppSession.UserId, AppSession.IsManager);

                for (int i = 0; i < _allProjects.Count; i++)
                {
                    var detail = await _projectService.GetProjectDetailsAsync(_allProjects[i].Id);
                    if (detail != null) _allProjects[i] = detail;
                }

                ApplyFilter();
                SetStatus($"✅ Tổng: {_allProjects.Count} dự án");
            }
            catch (Exception ex)
            {
                SetStatus("⚠ Lỗi tải dữ liệu.");
                MessageBox.Show(
                    $"Không thể tải danh sách dự án:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Filter / Bind ─────────────────────────────────────────

        private void ApplyFilter()
        {
            var keyword = txtSearch.Text.Trim().ToLowerInvariant();
            var statusFilter = cboFilterStatus.SelectedIndex > 0
                ? cboFilterStatus.SelectedItem!.ToString()!
                : string.Empty;

            var filtered = _allProjects.Where(p =>
            {
                bool matchKeyword = string.IsNullOrEmpty(keyword)
                    || p.Name.ToLowerInvariant().Contains(keyword)
                    || (p.Customer?.CompanyName?.ToLowerInvariant().Contains(keyword) ?? false)
                    || (p.Owner?.FullName?.ToLowerInvariant().Contains(keyword) ?? false);

                bool matchStatus = string.IsNullOrEmpty(statusFilter)
                    || p.Status == statusFilter;

                return matchKeyword && matchStatus;
            }).ToList();

            BindGrid(filtered);
        }

        private void BindGrid(List<Project> projects)
        {
            dgvProjects.Rows.Clear();

            foreach (var p in projects)
            {
                var statusText = UIHelper.FormatProjectStatus(p.Status);
                var deadline = p.PlannedEndDate.HasValue
                    ? p.PlannedEndDate.Value.ToString("dd/MM/yyyy") : "—";
                var budget = p.Budget > 0 ? p.Budget.ToString("N0") + " ₫" : "—";
                var memberCount = p.Members?.Count(m => m.LeftAt == null) ?? 0;

                int idx = dgvProjects.Rows.Add(
                    p.Id, p.Name,
                    p.Customer?.CompanyName ?? "—",
                    p.Owner?.FullName ?? "—",
                    statusText,
                    $"{memberCount} người",
                    deadline, budget,
                    p.StartDate.ToString("dd/MM/yyyy"));

                UIHelper.ApplyProjectRowStyle(dgvProjects.Rows[idx], p.Status, p.PlannedEndDate);
            }

            SetStatus($"✅ Tổng: {projects.Count} dự án");
        }

        // ── Selection ─────────────────────────────────────────────

        private void dgvProjects_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProjects.SelectedRows.Count == 0)
            {
                _selectedProject = null;
                UpdateButtons();
                return;
            }

            int id = (int)dgvProjects.SelectedRows[0].Cells["colId"].Value;
            _selectedProject = _allProjects.FirstOrDefault(p => p.Id == id);
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool sel = _selectedProject != null;
            btnEdit.Enabled = sel;
            btnDelete.Enabled = sel;
            btnMembers.Enabled = sel;
            btnStatus.Enabled = sel;
            btnDetail.Enabled = sel;
            btnKanban.Enabled = sel;
        }

        // ── CRUD Actions ──────────────────────────────────────────

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new frmProjectEdit(_projectService, _userService, _customerRepo, null);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                await LoadProjectsAsync();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null) return;
            var detail = await _projectService.GetProjectDetailsAsync(_selectedProject.Id);
            if (detail == null) return;

            using var dlg = new frmProjectEdit(_projectService, _userService, _customerRepo, detail);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                await LoadProjectsAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null) return;
            if (MessageBox.Show(
                    $"Xóa dự án \"{_selectedProject.Name}\"?\n\nChỉ xóa được nếu dự án chưa có công việc nào.",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes) return;

            btnDelete.Enabled = false;
            try
            {
                var (ok, msg) = await _projectService.DeleteProjectAsync(_selectedProject.Id);
                MessageBox.Show(msg,
                    ok ? "Thành công" : "Không thể xóa",
                    MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (ok) await LoadProjectsAsync();
            }
            finally
            {
                btnDelete.Enabled = true;
            }
        }

        // ── Status Change ─────────────────────────────────────────

        private ContextMenuStrip? _statusMenu;

        private async void btnStatus_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null) return;

            btnStatus.Enabled = false;
            try
            {
                _statusMenu?.Dispose();
                _statusMenu = new ContextMenuStrip();
                var menu = _statusMenu;
                var statuses = new[] { "NotStarted", "InProgress", "OnHold", "Completed", "Cancelled" };

                foreach (var s in statuses)
                {
                    var status = s;
                    var item = menu.Items.Add(UIHelper.FormatProjectStatus(status));
                    item.Click += async (_, _) =>
                    {
                        var (ok, msg) = await _projectService.ChangeStatusAsync(_selectedProject.Id, status);
                        if (ok) await LoadProjectsAsync();
                        else MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    };
                }

                menu.Show(btnStatus, new Point(0, btnStatus.Height));
            }
            finally
            {
                btnStatus.Enabled = true;
            }
        }

        // ── Members ───────────────────────────────────────────────

        private async void btnMembers_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null) return;
            using var dlg = new frmProjectMembers(_projectService, _userService, _selectedProject);
            dlg.ShowDialog(this);
            await LoadProjectsAsync();
        }

        // ── Kanban ────────────────────────────────────────────────

        private void btnKanban_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn một dự án để xem Kanban Board.",
                    "Chưa chọn dự án", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new frmKanban(_taskService, _projectService, _userService, _selectedProject.Id);
            dlg.ShowDialog(this);
        }

        // ── Detail ────────────────────────────────────────────────

        private async void btnDetail_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null) return;
            var detail = await _projectService.GetProjectDetailsAsync(_selectedProject.Id);
            if (detail == null)
            {
                MessageBox.Show("Không tìm thấy dự án.", "Lỗi");
                return;
            }

            var memberCount = detail.Members?.Count(m => m.LeftAt == null) ?? 0;
            var taskCount = detail.Tasks?.Count ?? 0;
            var totalExpense = detail.Expenses?.Sum(ex => ex.Amount) ?? 0;

            var memberList = detail.Members?
                .Where(m => m.LeftAt == null)
                .Select(m => $"  👤 {m.User?.FullName ?? "?"} — {m.ProjectRole ?? "Developer"}")
                .ToList() ?? new();

            var info =
                $"📁  {detail.Name}\n" +
                $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                $"Khách hàng:    {detail.Customer?.CompanyName ?? "—"}\n" +
                $"Quản lý:       {detail.Owner?.FullName ?? "—"}\n" +
                $"Trạng thái:    {UIHelper.FormatProjectStatus(detail.Status)}\n" +
                $"Ngày bắt đầu:  {detail.StartDate:dd/MM/yyyy}\n" +
                $"Deadline:      {(detail.PlannedEndDate.HasValue ? detail.PlannedEndDate.Value.ToString("dd/MM/yyyy") : "—")}\n" +
                $"Ngân sách:     {detail.Budget:N0} ₫\n" +
                $"Chi phí thực:  {totalExpense:N0} ₫\n" +
                $"Tiến độ:       {detail.ProgressPercent}%\n\n" +
                $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                $"👥  Thành viên ({memberCount})\n" +
                (memberList.Count > 0 ? string.Join("\n", memberList) : "  Chưa có thành viên") +
                $"\n\n📋  Công việc: {taskCount} task";

            MessageBox.Show(info, "Chi tiết dự án", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ── Filter & Refresh Events ───────────────────────────────

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilter();
        private void cboFilterStatus_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilter();
        private async void btnRefresh_Click(object sender, EventArgs e) => await LoadProjectsAsync();

        private void dgvProjects_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (AppSession.IsManager) btnEdit_Click(sender, e);
            else btnDetail_Click(sender, e);
        }

        // ── Status Bar ────────────────────────────────────────────

        private void SetStatus(string msg) => lblStatus.Text = msg;
    }
}