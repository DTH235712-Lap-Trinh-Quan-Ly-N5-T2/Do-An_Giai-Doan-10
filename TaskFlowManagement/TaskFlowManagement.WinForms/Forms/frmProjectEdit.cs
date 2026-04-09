using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmProjectEdit : BaseForm
    {
        // ── Dependencies ──────────────────────────────────────────
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly ICustomerRepository _customerRepo;
        private readonly Project? _editProject;
        private readonly bool _isEdit;

        // ── State ─────────────────────────────────────────────────
        private List<Customer> _customers = new();
        private List<User> _managers = new();

        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmProjectEdit()
        {
            InitializeComponent();
        }

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

        // ── Khởi tạo giao diện ────────────────────────────────────

        private void ApplyClientStyles()
        {
            // Header
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelAccentLine.BackColor = UIHelper.ColorPrimary;
            lblTitleForm.ForeColor = UIHelper.ColorSurface;

            // Body
            panelBody.BackColor = UIHelper.ColorBackground;
            tableFields.BackColor = UIHelper.ColorBackground;
            tableDateTime.BackColor = UIHelper.ColorBackground;
            tableBudgetPriority.BackColor = UIHelper.ColorBackground;
            panelStart.BackColor = UIHelper.ColorBackground;
            panelDeadline.BackColor = UIHelper.ColorBackground;
            panelDeadlineInput.BackColor = UIHelper.ColorBackground;
            panelBudget.BackColor = UIHelper.ColorBackground;
            panelPriority.BackColor = UIHelper.ColorBackground;

            // Labels
            var allLabels = new[]
            {
                lblName, lblCustomer, lblOwner,
                lblStart, lblDeadline, lblBudget, lblPriority, lblDesc
            };
            foreach (var lbl in allLabels)
            {
                lbl.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
                lbl.ForeColor = UIHelper.ColorMuted;
            }

            lblName.Text = "TÊN DỰ ÁN *";
            lblCustomer.Text = "KHÁCH HÀNG";
            lblOwner.Text = "QUẢN LÝ DỰ ÁN (PM) *";
            lblStart.Text = "NGÀY BẮT ĐẦU";
            lblDeadline.Text = "DEADLINE";
            lblBudget.Text = "NGÂN SÁCH (VNĐ)";
            lblPriority.Text = "ĐỘ ƯU TIÊN";
            lblDesc.Text = "MÔ TẢ (tùy chọn)";

            // TextBoxes & inputs
            var allInputs = new Control[] { txtName, txtBudget, txtDescription };
            foreach (var ctrl in allInputs)
            {
                ctrl.BackColor = UIHelper.ColorSurface;
                ctrl.ForeColor = UIHelper.ColorTextPrimary;
                ctrl.Font = UIHelper.FontBase;
            }

            var allCombos = new[] { cboCustomer, cboOwner, cboPriority };
            foreach (var cbo in allCombos)
            {
                cbo.BackColor = UIHelper.ColorSurface;
                cbo.ForeColor = UIHelper.ColorTextPrimary;
                cbo.Font = UIHelper.FontBase;
            }

            chkDeadline.Font = UIHelper.FontSmall;
            chkDeadline.ForeColor = UIHelper.ColorMuted;

            dtpStartDate.Font = UIHelper.FontBase;
            dtpDeadline.Font = UIHelper.FontBase;

            // Footer
            panelFooter.BackColor = UIHelper.ColorSurface;
            panelFooterLine.BackColor = UIHelper.ColorBorderLight;
            flowButtons.BackColor = UIHelper.ColorSurface;
            lblError.Font = UIHelper.FontSmall;
            lblError.ForeColor = UIHelper.ColorDanger;

            UIHelper.StyleButton(btnSave, UIHelper.ButtonVariant.Primary);
            UIHelper.StyleButton(btnCancel, UIHelper.ButtonVariant.Secondary);
        }

        // ── Form Load ─────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadDropdownsAsync();
            if (_isEdit) LoadEditData();
        }

        // ── Data Loading ──────────────────────────────────────────

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
                _managers = allUsers.Where(u =>
                    u.IsActive &&
                    u.UserRoles.Any(r => r.Role?.Name == "Manager" || r.Role?.Name == "Admin"))
                    .ToList();

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

        // ── Event Handlers ────────────────────────────────────────

        private async void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblError.Text = "⚠  Tên dự án không được để trống.";
                txtName.Focus();
                return;
            }

            if (cboOwner.SelectedIndex < 0)
            {
                lblError.Text = "⚠  Vui lòng chọn người quản lý.";
                return;
            }

            if (chkDeadline.Checked && dtpDeadline.Value.Date <= dtpStartDate.Value.Date)
            {
                lblError.Text = "⚠  Deadline phải sau ngày bắt đầu.";
                dtpDeadline.Focus();
                return;
            }

            SetLoading(true);
            try
            {
                int? customerId = cboCustomer.SelectedIndex > 0
                    ? _customers[cboCustomer.SelectedIndex - 1].Id
                    : null;

                int ownerId = _managers[cboOwner.SelectedIndex].Id;

                decimal budget = 0;
                if (!string.IsNullOrWhiteSpace(txtBudget.Text))
                    decimal.TryParse(
                        txtBudget.Text.Replace(",", "").Replace(".", ""),
                        out budget);

                DateOnly? deadline = chkDeadline.Checked
                    ? DateOnly.FromDateTime(dtpDeadline.Value)
                    : null;

                if (_isEdit)
                {
                    _editProject!.Name = txtName.Text.Trim();
                    _editProject.Description = NullIfEmpty(txtDescription.Text);
                    _editProject.CustomerId = customerId;
                    _editProject.OwnerId = ownerId;
                    _editProject.StartDate = DateOnly.FromDateTime(dtpStartDate.Value);
                    _editProject.PlannedEndDate = deadline;
                    _editProject.Budget = budget;
                    _editProject.Priority = (byte)(cboPriority.SelectedIndex + 1);

                    var (ok, msg) = await _projectService.UpdateProjectAsync(_editProject);
                    if (ok) { this.DialogResult = DialogResult.OK; this.Close(); }
                    else lblError.Text = "⚠  " + msg;
                }
                else
                {
                    var project = new Project
                    {
                        Name = txtName.Text.Trim(),
                        Description = NullIfEmpty(txtDescription.Text),
                        CustomerId = customerId,
                        OwnerId = ownerId,
                        StartDate = DateOnly.FromDateTime(dtpStartDate.Value),
                        PlannedEndDate = deadline,
                        Budget = budget,
                        Priority = (byte)(cboPriority.SelectedIndex + 1),
                        Status = "NotStarted"
                    };

                    var (ok, msg) = await _projectService.CreateProjectAsync(project);
                    if (ok) { this.DialogResult = DialogResult.OK; this.Close(); }
                    else lblError.Text = "⚠  " + msg;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "⚠  Lỗi: " + (ex.InnerException?.Message ?? ex.Message);
            }
            finally
            {
                if (!this.IsDisposed) SetLoading(false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkDeadline_CheckedChanged(object sender, EventArgs e)
        {
            dtpDeadline.Enabled = chkDeadline.Checked;
        }

        // ── Helpers ───────────────────────────────────────────────

        private void SetLoading(bool loading)
        {
            btnSave.Enabled = !loading;
            btnSave.Text = loading ? "⏳  Đang lưu..." : "💾  Lưu dự án";
        }

        private static string? NullIfEmpty(string? s)
            => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
    }
}