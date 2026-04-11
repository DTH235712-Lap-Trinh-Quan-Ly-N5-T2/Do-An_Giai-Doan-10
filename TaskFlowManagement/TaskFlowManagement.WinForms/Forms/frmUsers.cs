using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmUsers : BaseForm
    {
        private readonly IUserService _userService;
        private List<User> _allUsers = new();
        private User? _selectedUser;

        // ── Constructor dành riêng cho WinForms Designer ──────────────────────
        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmUsers() { InitializeComponent(); }

        // ── Constructor chính với Dependency Injection ────────────────────────
        public frmUsers(IUserService userService)
        {
            _userService = userService;
            InitializeComponent();
            ApplyClientStyles();
        }

        // ── Áp dụng toàn bộ style giao diện (gọi UIHelper tập trung tại đây) ──
        private void ApplyClientStyles()
        {
            // Form
            BackColor = UIHelper.ColorBackground;

            // Header panel
            panelTop.BackColor = UIHelper.ColorHeaderBg;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;

            // Toolbar panel (chứa search + filter + actions)
            panelToolbar.BackColor = UIHelper.ColorSurface;

            // DataGridView — dùng UIHelper.StyleDataGridView để tránh duplicate logic
            UIHelper.StyleDataGridView(dgvUsers);
            UIHelper.ApplyAlternateRowColors(dgvUsers);
            dgvUsers.DefaultCellStyle.SelectionBackColor = UIHelper.ColorBorderLight;
            dgvUsers.DefaultCellStyle.SelectionForeColor = UIHelper.ColorDark;

            // Style nút hành động — dùng UIHelper.StyleButton với ButtonVariant
            UIHelper.StyleButton(btnAdd, UIHelper.ButtonVariant.Primary);
            UIHelper.StyleButton(btnEdit, UIHelper.ButtonVariant.Success);
            UIHelper.StyleButton(btnDeactivate, UIHelper.ButtonVariant.Danger);
            UIHelper.StyleButton(btnActivate, UIHelper.ButtonVariant.Purple);

            // Nút refresh — Secondary style
            UIHelper.StyleButton(btnRefresh, UIHelper.ButtonVariant.Secondary);
            btnRefresh.FlatAppearance.BorderSize = 1;
            btnRefresh.FlatAppearance.BorderColor = UIHelper.ColorBorderLight;

            // Labels phụ
            lblCount.ForeColor = UIHelper.ColorMuted;
            lblSeparator.ForeColor = UIHelper.ColorBorderLight;

            // Status bar
            panelStatus.BackColor = UIHelper.ColorHeaderBg;
            lblStatus.ForeColor = UIHelper.ColorSubtitle;
        }

        // ── Tải dữ liệu ──────────────────────────────────────────────────────
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            SetStatus("Đang tải...");
            _allUsers = await _userService.GetAllUsersAsync();
            ApplyFilter();

            var active = _allUsers.Count(u => u.IsActive);
            var inactive = _allUsers.Count(u => !u.IsActive);
            SetStatus($"Tổng: {_allUsers.Count} tài khoản  ({active} active · {inactive} inactive)");
        }

        // ── Lọc và binding ───────────────────────────────────────────────────
        private void ApplyFilter()
        {
            var keyword = txtSearch.Text.Trim().ToLower();
            var showRole = cboFilterRole.SelectedIndex > 0
                ? cboFilterRole.SelectedItem!.ToString()! : "";
            var showActive = cboFilterStatus.SelectedIndex;

            var filtered = _allUsers.Where(u =>
            {
                bool matchKeyword = string.IsNullOrEmpty(keyword)
                    || u.Username.ToLower().Contains(keyword)
                    || u.FullName.ToLower().Contains(keyword)
                    || u.Email.ToLower().Contains(keyword);

                bool matchRole = string.IsNullOrEmpty(showRole)
                    || u.UserRoles.Any(r => r.Role?.Name == showRole);

                bool matchStatus = showActive == 0
                    || (showActive == 1 && u.IsActive)
                    || (showActive == 2 && !u.IsActive);

                return matchKeyword && matchRole && matchStatus;
            }).ToList();

            BindGrid(filtered);
        }

        private void BindGrid(List<User> users)
        {
            dgvUsers.Rows.Clear();
            foreach (var u in users)
            {
                var roles = string.Join(", ", u.UserRoles.Select(r => r.Role?.Name ?? ""));
                var status = u.IsActive ? "✅ Active" : "🔴 Inactive";
                var lastLogin = u.LastLogin.HasValue
                    ? u.LastLogin.Value.ToLocalTime().ToString("dd/MM/yyyy HH:mm")
                    : "Chưa đăng nhập";

                int idx = dgvUsers.Rows.Add(u.Id, u.Username, u.FullName, u.Email,
                    u.Phone ?? "", roles, status, lastLogin);

                // Hiển thị mờ cho tài khoản không còn hoạt động
                if (!u.IsActive)
                {
                    dgvUsers.Rows[idx].DefaultCellStyle.ForeColor = UIHelper.ColorSubtitle;
                    dgvUsers.Rows[idx].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
                }
            }
            lblCount.Text = $"{users.Count} tài khoản";
        }

        // ── Xử lý sự kiện DataGridView ───────────────────────────────────────
        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                _selectedUser = null;
                UpdateButtons();
                return;
            }
            int id = (int)dgvUsers.SelectedRows[0].Cells["colId"].Value;
            _selectedUser = _allUsers.FirstOrDefault(u => u.Id == id);
            UpdateButtons();
        }

        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit_Click(sender, e);
        }

        // ── Cập nhật trạng thái nút theo ngữ cảnh ────────────────────────────
        private void UpdateButtons()
        {
            bool selected = _selectedUser != null;
            bool notSelf = _selectedUser?.Id != AppSession.UserId;

            btnEdit.Enabled = selected;
            btnDeactivate.Enabled = selected && notSelf && (_selectedUser?.IsActive ?? false);
            btnActivate.Enabled = selected && !(_selectedUser?.IsActive ?? true);
        }

        // ── Các hành động CRUD ────────────────────────────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new frmUserEdit(_userService, null);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                _ = LoadUsersAsync();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null) return;
            using var dlg = new frmUserEdit(_userService, _selectedUser);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                _ = LoadUsersAsync();
        }

        private async void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null) return;

            var confirm = MessageBox.Show(
                $"Vô hiệu hóa tài khoản \"{_selectedUser.Username}\"?\n\n" +
                "Tài khoản sẽ không thể đăng nhập nhưng dữ liệu vẫn được giữ lại.\n" +
                "Bạn có thể kích hoạt lại bất kỳ lúc nào.",
                "Xác nhận vô hiệu hóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            var (ok, msg) = await _userService.DeactivateUserAsync(_selectedUser.Id);
            if (ok) await LoadUsersAsync();
            MessageBox.Show(msg, ok ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private async void btnActivate_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null) return;

            var confirm = MessageBox.Show(
                $"Kích hoạt lại tài khoản \"{_selectedUser.Username}\"?",
                "Xác nhận kích hoạt",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            // Dùng ActivateUserAsync thay vì UpdateUserAsync để tránh ghi đè dữ liệu không đầy đủ
            var (ok, msg) = await _userService.ActivateUserAsync(_selectedUser.Id);
            if (ok) await LoadUsersAsync();
            MessageBox.Show(msg, ok ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        // ── Sự kiện filter / refresh ──────────────────────────────────────────
        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilter();
        private void cboFilterRole_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilter();
        private void cboFilterStatus_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilter();
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            try
            {
                await LoadUsersAsync();
            }
            finally
            {
                if (!this.IsDisposed) btnRefresh.Enabled = true;
            }
        }

        // ── Tiện ích ─────────────────────────────────────────────────────────
        private void SetStatus(string msg) => lblStatus.Text = msg;
    }
}