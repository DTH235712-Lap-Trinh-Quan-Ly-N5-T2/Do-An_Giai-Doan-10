using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmCustomerEdit : BaseForm
    {
        private readonly ICustomerService _customerService;
        private readonly Customer? _editCustomer;
        private readonly bool _isEdit;

        // Constructor dành riêng cho WinForms Designer — không dùng trực tiếp
        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmCustomerEdit()
        {
            InitializeComponent();
        }

        public frmCustomerEdit(ICustomerService customerService, Customer? editCustomer)
        {
            _customerService = customerService;
            _editCustomer = editCustomer;
            _isEdit = editCustomer != null;

            InitializeComponent();
            ApplyClientStyles();
            LoadForm();
        }

        // ── Khởi tạo giao diện ──────────────────────────────────

        private void ApplyClientStyles()
        {
            // Header
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelAccentLine.BackColor = UIHelper.ColorPrimary;
            lblTitleForm.ForeColor = UIHelper.ColorSurface;

            // Body
            panelBody.BackColor = UIHelper.ColorBackground;
            tableFields.BackColor = UIHelper.ColorBackground;

            // Labels
            var fieldLabels = new[] { lblCompany, lblContact, lblEmail, lblPhone, lblAddress };
            foreach (var lbl in fieldLabels)
                lbl.ForeColor = UIHelper.ColorMuted;          // ← ColorTextSecondary → ColorMuted

            // TextBoxes
            var fieldInputs = new[] { txtCompany, txtContact, txtEmail, txtPhone, txtAddress };
            foreach (var txt in fieldInputs)
            {
                txt.BackColor = UIHelper.ColorSurface;
                txt.ForeColor = UIHelper.ColorTextPrimary;
            }

            // Footer
            panelFooter.BackColor = UIHelper.ColorSurface;
            panelFooterLine.BackColor = UIHelper.ColorBorderLight;  // ← ColorBorder → ColorBorderLight
            flowButtons.BackColor = UIHelper.ColorSurface;
            lblError.ForeColor = UIHelper.ColorDanger;

            // Nút bấm
            UIHelper.StyleButton(btnSave, UIHelper.ButtonVariant.Primary);
            UIHelper.StyleButton(btnCancel, UIHelper.ButtonVariant.Secondary);
        }

        private void LoadForm()
        {
            if (_isEdit)
            {
                this.Text = "Sửa thông tin khách hàng";
                lblTitleForm.Text = "✏️  Sửa thông tin";
                txtCompany.Text = _editCustomer!.CompanyName;
                txtContact.Text = _editCustomer.ContactName ?? "";
                txtEmail.Text = _editCustomer.Email ?? "";
                txtPhone.Text = _editCustomer.Phone ?? "";
                txtAddress.Text = _editCustomer.Address ?? "";
            }
            else
            {
                this.Text = "Thêm khách hàng mới";
                lblTitleForm.Text = "➕  Thêm khách hàng mới";
            }
        }

        // ── Event Handlers ───────────────────────────────────────

        private async void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtCompany.Text))
            {
                lblError.Text = "⚠  Tên công ty không được để trống.";
                txtCompany.Focus();
                return;
            }

            var emailInput = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(emailInput) && !emailInput.Contains('@'))
            {
                lblError.Text = "⚠  Email không hợp lệ.";
                txtEmail.Focus();
                return;
            }

            SetLoading(true);
            try
            {
                if (_isEdit)
                {
                    _editCustomer!.CompanyName = txtCompany.Text.Trim();
                    _editCustomer.ContactName = NullIfEmpty(txtContact.Text);
                    _editCustomer.Email = NullIfEmpty(emailInput);
                    _editCustomer.Phone = NullIfEmpty(txtPhone.Text);
                    _editCustomer.Address = NullIfEmpty(txtAddress.Text);
                    
                    var (ok, msg) = await _customerService.UpdateAsync(_editCustomer);
                    if (!ok) { lblError.Text = "⚠  " + msg; return; }
                }
                else
                {
                    var newCustomer = new Customer
                    {
                        CompanyName = txtCompany.Text.Trim(),
                        ContactName = NullIfEmpty(txtContact.Text),
                        Email = NullIfEmpty(emailInput),
                        Phone = NullIfEmpty(txtPhone.Text),
                        Address = NullIfEmpty(txtAddress.Text),
                    };
                    
                    var (ok, msg, _) = await _customerService.CreateAsync(newCustomer);
                    if (!ok) { lblError.Text = "⚠  " + msg; return; }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = "⚠  Lỗi khi lưu: " + (ex.InnerException?.Message ?? ex.Message);
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

        // ── Helpers ──────────────────────────────────────────────

        private void SetLoading(bool loading)
        {
            btnSave.Enabled = !loading;
            btnSave.Text = loading ? "Đang lưu..." : "💾  Lưu";
        }

        private static string? NullIfEmpty(string? s)
            => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
    }
}