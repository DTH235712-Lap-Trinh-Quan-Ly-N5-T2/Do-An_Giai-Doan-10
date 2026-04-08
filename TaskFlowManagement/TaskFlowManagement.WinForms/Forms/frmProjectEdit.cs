using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    /// <summary>
    /// Form thêm/sửa dự án.
    /// </summary>
    public partial class frmProjectEdit : BaseForm
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly ICustomerRepository _customerRepo;
        private readonly Project? _editProject;
        private readonly bool _isEdit;

        private List<Customer> _customers = new();
        private List<User> _managers = new();

        // ── Constructor rỗng: CHỈ dùng cho WinForms Designer ─────────────────
        public frmProjectEdit()
        {
            InitializeComponent();
        }

        // ── DI Constructor: dùng khi chạy thật ───────────────────────────────
        public frmProjectEdit(
            IProjectService projectService,
            IUserService userService,
            ICustomerRepository customerRepo,
            Project? editProject)
        {
            _projectService = projectService;
            _userService = userService;
            _customerRepo = customerRepo;
            _editProject = editProject;
            _isEdit = editProject != null;

            InitializeComponent();
            ApplyClientStyles();
        }

        // ── Tất cả UIHelper / helper methods / layout logic tách khỏi Designer
        private void ApplyClientStyles()
        {
            this.Font = UIHelper.FontBase;

            // ── panelHeader ───────────────────────────────────────────────────
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelAccentLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            lblTitleForm.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblTitleForm.ForeColor = UIHelper.ColorHeaderFg;

            // ── panelBody ─────────────────────────────────────────────────────
            panelBody.BackColor = UIHelper.ColorBackground;

            int y = 14, gap = 56, lx = 20, tw = 400;

            // Tên dự án
            ApplyLabel(lblName, "TÊN DỰ ÁN *", lx, y);
            ApplyTextBox(txtName, "Nhập tên dự án...", lx, y + 18, tw, 1);

            // Khách hàng
            y += gap;
            ApplyLabel(lblCustomer, "KHÁCH HÀNG", lx, y);
            ApplyCombo(cboCustomer, lx, y + 18, tw, 2);

            // PM
            y += gap;
            ApplyLabel(lblOwner, "QUẢN LÝ DỰ ÁN (PM) *", lx, y);
            ApplyCombo(cboOwner, lx, y + 18, tw, 3);

            // Ngày bắt đầu + Deadline
            y += gap;
            ApplyLabel(lblStart, "NGÀY BẮT ĐẦU", lx, y);
            dtpStartDate.Font = UIHelper.FontBase;
            dtpStartDate.Location = new System.Drawing.Point(lx, y + 18);
            dtpStartDate.Size = new System.Drawing.Size(190, 30);

            ApplyLabel(lblDeadline, "DEADLINE", lx + 210, y);

            chkDeadline.Font = UIHelper.FontSmall;
            chkDeadline.ForeColor = UIHelper.ColorMuted;
            chkDeadline.Location = new System.Drawing.Point(lx + 290, y + 1);

            dtpDeadline.Font = UIHelper.FontBase;
            dtpDeadline.Location = new System.Drawing.Point(lx + 210, y + 18);
            dtpDeadline.Size = new System.Drawing.Size(190, 30);

            // Ngân sách + Priority
            y += gap;
            ApplyLabel(lblBudget, "NGÂN SÁCH (VNĐ)", lx, y);
            ApplyTextBox(txtBudget, "VD: 500000000", lx, y + 18, 190, 6);

            ApplyLabel(lblPriority, "ĐỘ ƯU TIÊN", lx + 210, y);
            ApplyCombo(cboPriority, lx + 210, y + 18, 190, 7);

            // Mô tả
            y += gap;
            ApplyLabel(lblDesc, "MÔ TẢ (tùy chọn)", lx, y);
            txtDescription.BackColor = System.Drawing.Color.White;
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = UIHelper.FontBase;
            txtDescription.Location = new System.Drawing.Point(lx, y + 18);
            txtDescription.PlaceholderText = "Mô tả chi tiết dự án...";
            txtDescription.Size = new System.Drawing.Size(tw, 60);

            // ── panelFooter ───────────────────────────────────────────────────
            lblError.Font = UIHelper.FontSmall;
            lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);

            UIHelper.StyleButton(btnSave, UIHelper.ButtonVariant.Primary);
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            UIHelper.StyleButton(btnCancel, UIHelper.ButtonVariant.Secondary);
            btnCancel.Font = UIHelper.FontBase;
        }

        // ── Instance helpers — KHÔNG đặt trong Designer ───────────────────────
        private void ApplyLabel(Label lbl, string text, int x, int y)
        {
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = UIHelper.ColorDark;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.Text = text;
        }

        private void ApplyTextBox(TextBox txt, string placeholder, int x, int y, int w, int tabIdx)
        {
            txt.BackColor = System.Drawing.Color.White;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = UIHelper.FontBase;
            txt.Location = new System.Drawing.Point(x, y);
            txt.PlaceholderText = placeholder;
            txt.Size = new System.Drawing.Size(w, 30);
            txt.TabIndex = tabIdx;
        }

        private void ApplyCombo(ComboBox cbo, int x, int y, int w, int tabIdx)
        {
            cbo.BackColor = System.Drawing.Color.White;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.Font = UIHelper.FontBase;
            cbo.Location = new System.Drawing.Point(x, y);
            cbo.Size = new System.Drawing.Size(w, 30);
            cbo.TabIndex = tabIdx;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Phần còn lại: giữ nguyên hoàn toàn
        // ─────────────────────────────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadDropdownsAsync();
            if (_isEdit) LoadEditData();
        }

        private async Task LoadDropdownsAsync()
        {
            try
            {
                cboCustomer.DataSource = null; cboCustomer.Items.Clear();
                cboOwner.DataSource = null; cboOwner.Items.Clear();
                cboPriority.DataSource = null; cboPriority.Items.Clear();

                _customers = await _customerRepo.GetAllAsync();
                cboCustomer.Items.Add("— Không chọn —");
                foreach (var c in _customers)
                    cboCustomer.Items.Add(c.CompanyName);
                cboCustomer.SelectedIndex = 0;

                var allUsers = await _userService.GetAllUsersAsync();
                _managers = allUsers.Where(u => u.IsActive &&
                    u.UserRoles.Any(r => r.Role?.Name == "Manager" || r.Role?.Name == "Admin")).ToList();
                foreach (var m in _managers)
                    cboOwner.Items.Add(m.FullName);
                if (cboOwner.Items.Count > 0) cboOwner.SelectedIndex = 0;

                cboPriority.Items.AddRange(new object[] { "Low", "Medium", "High", "Critical" });
                cboPriority.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                lblError.Text = "⚠  Lỗi tải dữ liệu: " + (ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void LoadEditData()
        {
            this.Text = "Sửa thông tin dự án";
            lblTitleForm.Text = "✏️  Sửa thông tin dự án";

            txtName.Text = _editProject!.Name;
            txtDescription.Text = _editProject.Description ?? "";
            dtpStartDate.Value = _editProject.StartDate.ToDateTime(TimeOnly.MinValue);

            if (_editProject.PlannedEndDate.HasValue)
            {
                chkDeadline.Checked = true;
                dtpDeadline.Value = _editProject.PlannedEndDate.Value.ToDateTime(TimeOnly.MinValue);
            }

            txtBudget.Text = _editProject.Budget > 0 ? _editProject.Budget.ToString("0") : "";

            if (_editProject.CustomerId.HasValue)
            {
                var idx = _customers.FindIndex(c => c.Id == _editProject.CustomerId);
                cboCustomer.SelectedIndex = idx >= 0 ? idx + 1 : 0;
            }

            var ownerIdx = _managers.FindIndex(m => m.Id == _editProject.OwnerId);
            if (ownerIdx >= 0) cboOwner.SelectedIndex = ownerIdx;

            cboPriority.SelectedIndex = Math.Max(0, _editProject.Priority - 1);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtName.Text))
            { lblError.Text = "⚠  Tên dự án không được để trống."; txtName.Focus(); return; }

            if (cboOwner.SelectedIndex < 0)
            { lblError.Text = "⚠  Vui lòng chọn người quản lý."; return; }

            if (chkDeadline.Checked && dtpDeadline.Value.Date <= dtpStartDate.Value.Date)
            { lblError.Text = "⚠  Deadline phải sau ngày bắt đầu."; dtpDeadline.Focus(); return; }

            SetLoading(true);
            try
            {
                int? customerId = cboCustomer.SelectedIndex > 0
                    ? _customers[cboCustomer.SelectedIndex - 1].Id : null;
                int ownerId = _managers[cboOwner.SelectedIndex].Id;

                decimal budget = 0;
                if (!string.IsNullOrWhiteSpace(txtBudget.Text))
                    decimal.TryParse(txtBudget.Text.Replace(",", "").Replace(".", ""), out budget);

                DateOnly? deadline = chkDeadline.Checked
                    ? DateOnly.FromDateTime(dtpDeadline.Value) : null;

                if (_isEdit)
                {
                    _editProject!.Name = txtName.Text.Trim();
                    _editProject.Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim();
                    _editProject.CustomerId = customerId;
                    _editProject.OwnerId = ownerId;
                    _editProject.StartDate = DateOnly.FromDateTime(dtpStartDate.Value);
                    _editProject.PlannedEndDate = deadline;
                    _editProject.Budget = budget;
                    _editProject.Priority = (byte)(cboPriority.SelectedIndex + 1);

                    var (ok, msg) = await _projectService.UpdateProjectAsync(_editProject);
                    SetLoading(false);
                    if (ok) { this.DialogResult = DialogResult.OK; this.Close(); }
                    else lblError.Text = "⚠  " + msg;
                }
                else
                {
                    var project = new Project
                    {
                        Name = txtName.Text.Trim(),
                        Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                        CustomerId = customerId,
                        OwnerId = ownerId,
                        StartDate = DateOnly.FromDateTime(dtpStartDate.Value),
                        PlannedEndDate = deadline,
                        Budget = budget,
                        Priority = (byte)(cboPriority.SelectedIndex + 1),
                        Status = "NotStarted"
                    };
                    var (ok, msg) = await _projectService.CreateProjectAsync(project);
                    SetLoading(false);
                    if (ok) { this.DialogResult = DialogResult.OK; this.Close(); }
                    else lblError.Text = "⚠  " + msg;
                }
            }
            catch (Exception ex)
            {
                SetLoading(false);
                lblError.Text = "⚠  Lỗi: " + (ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        { this.DialogResult = DialogResult.Cancel; this.Close(); }

        private void chkDeadline_CheckedChanged(object sender, EventArgs e)
        { dtpDeadline.Enabled = chkDeadline.Checked; }

        private void SetLoading(bool loading)
        { btnSave.Enabled = !loading; btnSave.Text = loading ? "⏳  Đang lưu..." : "💾  Lưu dự án"; }
    }
}