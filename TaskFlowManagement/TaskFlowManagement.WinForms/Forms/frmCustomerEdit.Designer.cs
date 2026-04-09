using System.Windows.Forms;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmCustomerEdit
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new Panel();
            this.panelAccentLine = new Panel();
            this.lblTitleForm = new Label();

            this.panelBody = new Panel();
            this.tableFields = new TableLayoutPanel();
            this.lblCompany = new Label();
            this.txtCompany = new TextBox();
            this.lblContact = new Label();
            this.txtContact = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblPhone = new Label();
            this.txtPhone = new TextBox();
            this.lblAddress = new Label();
            this.txtAddress = new TextBox();

            this.panelFooter = new Panel();
            this.panelFooterLine = new Panel();
            this.lblError = new Label();
            this.flowButtons = new FlowLayoutPanel();
            this.btnCancel = new Button();
            this.btnSave = new Button();

            this.panelHeader.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.tableFields.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.flowButtons.SuspendLayout();
            this.SuspendLayout();

            // ── panelHeader ──────────────────────────────────────
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 64;
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Controls.Add(this.lblTitleForm);
            this.panelHeader.Controls.Add(this.panelAccentLine);

            this.panelAccentLine.Dock = DockStyle.Bottom;
            this.panelAccentLine.Height = 4;
            this.panelAccentLine.Name = "panelAccentLine";

            this.lblTitleForm.AutoSize = false;
            this.lblTitleForm.Dock = DockStyle.Fill;
            this.lblTitleForm.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitleForm.Name = "lblTitleForm";
            this.lblTitleForm.Padding = new Padding(20, 0, 0, 4);
            this.lblTitleForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── panelBody ────────────────────────────────────────
            this.panelBody.Dock = DockStyle.Fill;
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new Padding(24, 16, 24, 8);
            this.panelBody.Controls.Add(this.tableFields);

            // ── tableFields (layout engine cho các trường nhập liệu) ─
            this.tableFields.AutoSize = true;
            this.tableFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.tableFields.ColumnCount = 1;
            this.tableFields.Dock = DockStyle.Top;
            this.tableFields.Name = "tableFields";
            this.tableFields.Padding = new Padding(0);
            this.tableFields.RowCount = 10;
            this.tableFields.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;

            // Row styles: label nhỏ, textbox chuẩn, gap dưới textbox
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));   // lblCompany
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));   // txtCompany
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));   // gap
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));   // lblContact
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));   // txtContact
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));   // gap
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));   // lblEmail
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));   // txtEmail
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));   // gap
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));   // lblPhone
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));   // txtPhone
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));   // gap
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));   // lblAddress
            this.tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));   // txtAddress (multiline)
            this.tableFields.RowCount = 14;

            this.tableFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // lblCompany
            this.lblCompany.AutoSize = true;
            this.lblCompany.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblCompany.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Text = "TÊN CÔNG TY *";
            this.tableFields.Controls.Add(this.lblCompany, 0, 0);

            // txtCompany
            this.txtCompany.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtCompany.BorderStyle = BorderStyle.FixedSingle;
            this.txtCompany.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.PlaceholderText = "VD: FPT Software";
            this.txtCompany.Size = new System.Drawing.Size(352, 30);
            this.tableFields.Controls.Add(this.txtCompany, 0, 1);

            // gap row 2 — empty, handled by RowStyle height

            // lblContact
            this.lblContact.AutoSize = true;
            this.lblContact.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblContact.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblContact.Name = "lblContact";
            this.lblContact.Text = "NGƯỜI LIÊN HỆ";
            this.tableFields.Controls.Add(this.lblContact, 0, 3);

            // txtContact
            this.txtContact.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtContact.BorderStyle = BorderStyle.FixedSingle;
            this.txtContact.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContact.Name = "txtContact";
            this.txtContact.PlaceholderText = "Họ tên người liên hệ";
            this.txtContact.Size = new System.Drawing.Size(352, 30);
            this.tableFields.Controls.Add(this.txtContact, 0, 4);

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Text = "EMAIL";
            this.tableFields.Controls.Add(this.lblEmail, 0, 6);

            // txtEmail
            this.txtEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtEmail.BorderStyle = BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "contact@company.com";
            this.txtEmail.Size = new System.Drawing.Size(352, 30);
            this.tableFields.Controls.Add(this.txtEmail, 0, 7);

            // lblPhone
            this.lblPhone.AutoSize = true;
            this.lblPhone.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Text = "ĐIỆN THOẠI";
            this.tableFields.Controls.Add(this.lblPhone, 0, 9);

            // txtPhone
            this.txtPhone.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtPhone.BorderStyle = BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.PlaceholderText = "028 1234 5678";
            this.txtPhone.Size = new System.Drawing.Size(352, 30);
            this.tableFields.Controls.Add(this.txtPhone, 0, 10);

            // lblAddress
            this.lblAddress.AutoSize = true;
            this.lblAddress.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Text = "ĐỊA CHỈ";
            this.tableFields.Controls.Add(this.lblAddress, 0, 12);

            // txtAddress
            this.txtAddress.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtAddress.BorderStyle = BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PlaceholderText = "Số nhà, đường, quận/huyện, tỉnh/thành phố...";
            this.txtAddress.Size = new System.Drawing.Size(352, 58);
            this.tableFields.Controls.Add(this.txtAddress, 0, 13);

            // ── panelFooter ──────────────────────────────────────
            this.panelFooter.Dock = DockStyle.Bottom;
            this.panelFooter.Height = 80;
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new Padding(20, 0, 20, 0);
            this.panelFooter.Controls.Add(this.flowButtons);
            this.panelFooter.Controls.Add(this.lblError);
            this.panelFooter.Controls.Add(this.panelFooterLine);

            this.panelFooterLine.Dock = DockStyle.Top;
            this.panelFooterLine.Height = 1;
            this.panelFooterLine.Name = "panelFooterLine";

            // lblError — hiển thị thông báo lỗi validation
            this.lblError.AutoSize = false;
            this.lblError.Dock = DockStyle.Top;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblError.Height = 20;
            this.lblError.Name = "lblError";
            this.lblError.Padding = new Padding(0, 4, 0, 0);
            this.lblError.Text = "";

            // flowButtons — chứa 2 nút, căn phải
            this.flowButtons.AutoSize = false;
            this.flowButtons.Dock = DockStyle.Bottom;
            this.flowButtons.FlowDirection = FlowDirection.RightToLeft;
            this.flowButtons.Height = 52;
            this.flowButtons.Name = "flowButtons";
            this.flowButtons.WrapContents = false;
            this.flowButtons.Padding = new Padding(0, 8, 0, 8);
            this.flowButtons.Controls.Add(this.btnSave);
            this.flowButtons.Controls.Add(this.btnCancel);

            // btnSave — thứ tự RightToLeft: khai báo trước = hiển thị bên phải
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 36);
            this.btnSave.Text = "💾  Lưu";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.Margin = new Padding(0, 0, 0, 0);
            this.btnSave.Click += btnSave_Click;

            // btnCancel
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 36);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnCancel.Margin = new Padding(0, 0, 8, 0);
            this.btnCancel.Click += btnCancel_Click;

            // ── frmCustomerEdit ──────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 560);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCustomerEdit";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Khách hàng";

            this.panelHeader.ResumeLayout(false);
            this.tableFields.ResumeLayout(false);
            this.tableFields.PerformLayout();
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.flowButtons.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Fields ──────────────────────────────────────────────
        private Panel panelHeader;
        private Panel panelAccentLine;
        private Label lblTitleForm;
        private Panel panelBody;
        private TableLayoutPanel tableFields;
        private Label lblCompany;
        private TextBox txtCompany;
        private Label lblContact;
        private TextBox txtContact;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblAddress;
        private TextBox txtAddress;
        private Panel panelFooter;
        private Panel panelFooterLine;
        private Label lblError;
        private FlowLayoutPanel flowButtons;
        private Button btnSave;
        private Button btnCancel;
    }
}