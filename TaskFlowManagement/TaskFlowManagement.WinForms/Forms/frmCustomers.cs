using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmCustomers : BaseForm
    {
        private readonly ICustomerService _customerService;
        private List<Customer> _allCustomers = new();
        private List<Customer> _displayedCustomers = new();
        private Customer? _selectedCustomer;
        private readonly System.Windows.Forms.Timer _searchTimer;

        // ── Constructor rỗng: CHỈ dùng cho WinForms Designer ─────────────────
        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmCustomers()
        {
            InitializeComponent();
        }

        // ── DI Constructor: dùng khi chạy thật ───────────────────────────────
        public frmCustomers(ICustomerService customerService)
        {
            _customerService = customerService;
            _searchTimer = new System.Windows.Forms.Timer { Interval = 350 };

            InitializeComponent();
            ApplyClientStyles();

            _searchTimer.Tick += async (s, e) =>
            {
                _searchTimer.Stop();
                await SearchAsync();
            };
        }

        // ── Toàn bộ logic làm đẹp UI tập trung tại đây ───────────────────────
        private void ApplyClientStyles()
        {
            this.Font = UIHelper.FontBase;

            // ── panelTop ──────────────────────────────────────────────────────
            panelTop.BackColor = UIHelper.ColorHeaderBg;
            panelAccentLine.BackColor = UIHelper.ColorPrimary;
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;

            // ── panelTopbar: thanh công cụ hợp nhất (search + buttons) ────────
            panelTopbar.BackColor = UIHelper.ColorBackground;

            UIHelper.StyleSearchBox(txtSearch, "🔍  Tìm theo tên công ty, liên hệ, email...");
            txtSearch.Size = new Size(340, 32);

            // Nút Làm mới — Secondary style với viền rõ ràng, căn cao bằng TextBox
            UIHelper.StyleButton(btnRefresh, UIHelper.ButtonVariant.Secondary);
            btnRefresh.Text = "🔄  Làm mới";
            btnRefresh.Size = new Size(110, 32);
            btnRefresh.Padding = new Padding(10, 0, 10, 0);

            // Nhóm nút CRUD — AutoSize để đồng nhất padding, không bị cắt text
            UIHelper.StyleButton(btnAdd, UIHelper.ButtonVariant.Primary);
            UIHelper.StyleButton(btnEdit, UIHelper.ButtonVariant.Success);
            UIHelper.StyleButton(btnDelete, UIHelper.ButtonVariant.Danger);
            UIHelper.StyleButton(btnDetail, UIHelper.ButtonVariant.Slate);

            foreach (var btn in new[] { btnAdd, btnEdit, btnDelete, btnDetail })
            {
                btn.AutoSize = true;
                btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                btn.Padding = new Padding(14, 6, 14, 6);
            }

            btnAdd.Text = "➕  Thêm mới";
            btnEdit.Text = "✏️  Sửa";
            btnDelete.Text = "🗑️  Xóa";
            btnDetail.Text = "📋  Xem dự án";

            // ── DataGridView ──────────────────────────────────────────────────
            UIHelper.StyleDataGridView(dgvCustomers);
            UIHelper.ApplyAlternateRowColors(dgvCustomers);

            // Cấu hình AutoSize cột: Tên công ty, Email, Địa chỉ → Fill; còn lại cố định
            colCompany.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCompany.FillWeight = 25;
            colCustEmail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCustEmail.FillWeight = 20;
            colAddress.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAddress.FillWeight = 30;

            colContact.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colContact.Width = 150;
            colCustPhone.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colCustPhone.Width = 120;
            colCreatedAt.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colCreatedAt.Width = 110;

            // ── panelStatus ───────────────────────────────────────────────────
            panelStatus.BackColor = UIHelper.ColorHeaderBg;
            lblStatus.Font = UIHelper.FontSmall;
            lblStatus.ForeColor = UIHelper.ColorSubtitle;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Event handlers & business logic
        // ─────────────────────────────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadAllAsync();
        }

        private async Task LoadAllAsync()
        {
            SetStatus("⏳  Đang tải...");
            _allCustomers = await _customerService.GetAllAsync();
            BindGrid(_allCustomers);
            SetStatus($"Tổng: {_allCustomers.Count} khách hàng");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private async Task SearchAsync()
        {
            var kw = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(kw)) { BindGrid(_allCustomers); return; }
            var results = await _customerService.SearchAsync(kw);
            BindGrid(results);
            SetStatus($"Tìm thấy: {results.Count} kết quả");
        }

        private void BindGrid(List<Customer> list)
        {
            _displayedCustomers = list;
            _selectedCustomer = null;
            dgvCustomers.Rows.Clear();

            foreach (var c in list)
            {
                dgvCustomers.Rows.Add(
                    c.Id, c.CompanyName,
                    c.ContactName ?? "",
                    c.Email ?? "",
                    c.Phone ?? "",
                    c.Address ?? "",
                    c.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy"));
            }

            UpdateButtons();
        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                _selectedCustomer = null;
                UpdateButtons();
                return;
            }

            int id = (int)dgvCustomers.SelectedRows[0].Cells["colCustId"].Value;
            _selectedCustomer = _displayedCustomers.FirstOrDefault(c => c.Id == id);
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool sel = _selectedCustomer != null;
            btnEdit.Enabled = sel;
            btnDelete.Enabled = sel;
            btnDetail.Enabled = sel;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new frmCustomerEdit(_customerService, null);
            if (dlg.ShowDialog(this) == DialogResult.OK) _ = LoadAllAsync();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null) return;
            using var dlg = new frmCustomerEdit(_customerService, _selectedCustomer);
            if (dlg.ShowDialog(this) == DialogResult.OK) _ = LoadAllAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null) return;

            var confirm = MessageBox.Show(
                $"Xóa khách hàng \"{_selectedCustomer.CompanyName}\"?\n\n" +
                "⚠️  Lưu ý: Chỉ xóa được nếu khách hàng chưa có dự án nào.",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            btnDelete.Enabled = false;
            try
            {
                var (success, message) = await _customerService.DeleteAsync(_selectedCustomer.Id);
                if (success)
                {
                    txtSearch.Clear();
                    await LoadAllAsync();
                    MessageBox.Show(message, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(message, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                btnDelete.Enabled = true;
            }
        }

        private async void btnDetail_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null) return;

            var customer = await _customerService.GetWithProjectsAsync(_selectedCustomer.Id);
            if (customer == null)
            {
                MessageBox.Show("Không tìm thấy khách hàng.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int count = customer.Projects?.Count ?? 0;
            if (count == 0)
            {
                MessageBox.Show(
                    $"\"{customer.CompanyName}\" chưa có dự án nào.",
                    "Chi tiết khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var lines = customer.Projects!
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => $"  📁  {p.Name}  —  {p.Status}  (PM: {p.Owner?.FullName ?? "?"})");

            MessageBox.Show(
                $"Khách hàng: {customer.CompanyName}\nTổng dự án: {count}\n\n" +
                string.Join("\n", lines),
                "Danh sách dự án", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit_Click(sender, e);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            await LoadAllAsync();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Dispose();
            base.OnFormClosed(e);
        }

        private void SetStatus(string msg) => lblStatus.Text = msg;
    }
}