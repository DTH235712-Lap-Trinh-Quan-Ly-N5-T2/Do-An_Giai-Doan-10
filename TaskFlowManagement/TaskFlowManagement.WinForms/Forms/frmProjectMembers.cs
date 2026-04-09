using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmProjectMembers : BaseForm
    {
        // ── Dependencies ──────────────────────────────────────────
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly Project _project;

        // ── State ─────────────────────────────────────────────────
        private List<ProjectMember> _members = new();
        private List<User> _availableUsers = new();

        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmProjectMembers()
        {
            InitializeComponent();
        }

        public frmProjectMembers(
            IProjectService projectService,
            IUserService userService,
            Project project)
        {
            _projectService = projectService;
            _userService = userService;
            _project = project;

            InitializeComponent();
            ApplyClientStyles();

            var title = $"👥  Thành viên — {_project.Name}";
            this.Text = title;
            lblTitle.Text = title;

            bool canEdit = AppSession.IsManager;
            panelAdd.Visible = canEdit;
            btnRemove.Visible = canEdit;
        }

        // ── Khởi tạo giao diện ────────────────────────────────────

        private void ApplyClientStyles()
        {
            // Header
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelAccent.BackColor = UIHelper.ColorPrimary;
            lblTitle.Font = UIHelper.FontHeaderLarge;
            lblTitle.ForeColor = UIHelper.ColorHeaderFg;

            // Khu vực thêm thành viên
            panelAdd.BackColor = UIHelper.ColorBackground;
            tableAdd.BackColor = UIHelper.ColorBackground;
            lblAddTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            lblAddTitle.ForeColor = UIHelper.ColorMuted;

            cboUser.Font = UIHelper.FontBase;
            cboUser.BackColor = UIHelper.ColorSurface;
            cboProjectRole.Font = UIHelper.FontBase;
            cboProjectRole.BackColor = UIHelper.ColorSurface;

            UIHelper.StyleButton(btnAddMember, UIHelper.ButtonVariant.Primary);

            // DataGridView
            UIHelper.StyleDataGridView(dgvMembers);
            UIHelper.ApplyAlternateRowColors(dgvMembers);

            colJoinedAt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Footer
            panelBottom.BackColor = UIHelper.ColorSurface;
            panelBottomLine.BackColor = UIHelper.ColorBorderLight;
            flowBottomBtns.BackColor = UIHelper.ColorSurface;
            lblCount.Font = UIHelper.FontSmall;
            lblCount.ForeColor = UIHelper.ColorMuted;

            UIHelper.StyleButton(btnRemove, UIHelper.ButtonVariant.Danger);
            UIHelper.StyleButton(btnClose, UIHelper.ButtonVariant.Secondary);
        }

        // ── Form Load ─────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadMembersAsync();
            await LoadAvailableUsersAsync();

            cboProjectRole.Items.AddRange(new object[] { "Developer", "Tester", "BA", "Tech Lead" });
            cboProjectRole.SelectedIndex = 0;
        }

        // ── Data Loading ──────────────────────────────────────────

        private async Task LoadMembersAsync()
        {
            _members = await _projectService.GetMembersAsync(_project.Id);
            dgvMembers.Rows.Clear();

            foreach (var m in _members)
            {
                dgvMembers.Rows.Add(
                    m.UserId,
                    m.User?.FullName ?? "—",
                    m.User?.Email ?? "—",
                    m.ProjectRole ?? "—",
                    m.JoinedAt.ToLocalTime().ToString("dd/MM/yyyy"));
            }

            lblCount.Text = $"{_members.Count} thành viên";
        }

        private async Task LoadAvailableUsersAsync()
        {
            var allActive = await _userService.GetAllActiveUsersAsync();
            var memberIds = _members.Select(m => m.UserId).ToHashSet();
            _availableUsers = allActive.Where(u => !memberIds.Contains(u.Id)).ToList();

            cboUser.Items.Clear();
            foreach (var u in _availableUsers)
                cboUser.Items.Add($"{u.FullName}  ({u.Username})");

            if (cboUser.Items.Count > 0) cboUser.SelectedIndex = 0;

            cboUser.AdjustDropDownWidth();
        }

        // ── Event Handlers ────────────────────────────────────────

        private async void btnAddMember_Click(object sender, EventArgs e)
        {
            if (cboUser.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn người dùng.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnAddMember.Enabled = false;
            try
            {
                var user = _availableUsers[cboUser.SelectedIndex];
                var role = cboProjectRole.SelectedItem?.ToString() ?? "Developer";

                var (ok, msg) = await _projectService.AddMemberAsync(_project.Id, user.Id, role);
                if (ok)
                {
                    await LoadMembersAsync();
                    await LoadAvailableUsersAsync();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                if (!this.IsDisposed) btnAddMember.Enabled = true;
            }
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0) return;

            int userId = (int)dgvMembers.SelectedRows[0].Cells["colMemberId"].Value;
            var member = _members.FirstOrDefault(m => m.UserId == userId);
            if (member == null) return;

            if (MessageBox.Show(
                    $"Xóa \"{member.User?.FullName}\" khỏi dự án?\n\nLịch sử tham gia vẫn được lưu lại.",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            btnRemove.Enabled = false;
            try
            {
                var (ok, _) = await _projectService.RemoveMemberAsync(_project.Id, userId);
                if (ok)
                {
                    await LoadMembersAsync();
                    await LoadAvailableUsersAsync();
                }
            }
            finally
            {
                if (!this.IsDisposed) btnRemove.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}