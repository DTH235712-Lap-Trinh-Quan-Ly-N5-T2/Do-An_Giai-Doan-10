using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmProjectEdit   // BaseForm declared in frmProjectEdit.cs
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Instantiation ──────────────────────────────────────────────────────
            panelHeader = new Panel();
            lblTitleForm = new Label();
            panelAccentLine = new Panel();
            panelBody = new Panel();
            panelFooter = new Panel();
            panelFooterLine = new Panel();
            lblError = new Label();
            btnSave = new Button();
            btnCancel = new Button();

            lblName = new Label(); txtName = new TextBox();
            lblCustomer = new Label(); cboCustomer = new ComboBox();
            lblOwner = new Label(); cboOwner = new ComboBox();
            lblStart = new Label(); dtpStartDate = new DateTimePicker();
            lblDeadline = new Label(); dtpDeadline = new DateTimePicker();
            chkDeadline = new CheckBox();
            lblBudget = new Label(); txtBudget = new TextBox();
            lblPriority = new Label(); cboPriority = new ComboBox();
            lblDesc = new Label(); txtDescription = new TextBox();

            panelHeader.SuspendLayout();
            panelBody.SuspendLayout();
            panelFooter.SuspendLayout();
            this.SuspendLayout();

            // ── panelHeader ────────────────────────────────────────────────────────
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 64;
            panelHeader.Name = "panelHeader";
            panelHeader.Controls.Add(lblTitleForm);
            panelHeader.Controls.Add(panelAccentLine);

            lblTitleForm.AutoSize = false;
            lblTitleForm.Dock = DockStyle.Fill;
            lblTitleForm.Name = "lblTitleForm";
            lblTitleForm.Padding = new Padding(18, 0, 0, 4);
            lblTitleForm.Text = "➕  Tạo dự án mới";
            lblTitleForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Height = 4;
            panelAccentLine.Name = "panelAccentLine";

            // ── panelBody ──────────────────────────────────────────────────────────
            panelBody.AutoScroll = true;
            panelBody.Dock = DockStyle.Fill;
            panelBody.Name = "panelBody";
            panelBody.Controls.AddRange(new Control[]
            {
        lblName, txtName, lblCustomer, cboCustomer, lblOwner, cboOwner,
        lblStart, dtpStartDate, lblDeadline, chkDeadline, dtpDeadline,
        lblBudget, txtBudget, lblPriority, cboPriority,
        lblDesc, txtDescription
            });

            // Tên — placeholder location/size; thực tế set trong ApplyClientStyles()
            lblName.Name = "lblName";
            txtName.Name = "txtName";
            txtName.TabIndex = 1;

            lblCustomer.Name = "lblCustomer";
            cboCustomer.Name = "cboCustomer";
            cboCustomer.TabIndex = 2;

            lblOwner.Name = "lblOwner";
            cboOwner.Name = "cboOwner";
            cboOwner.TabIndex = 3;

            lblStart.Name = "lblStart";
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.TabIndex = 4;

            lblDeadline.Name = "lblDeadline";
            chkDeadline.Name = "chkDeadline";
            chkDeadline.Text = "Có deadline";
            chkDeadline.AutoSize = true;
            chkDeadline.CheckedChanged += chkDeadline_CheckedChanged;

            dtpDeadline.Name = "dtpDeadline";
            dtpDeadline.Format = DateTimePickerFormat.Short;
            dtpDeadline.Enabled = false;
            dtpDeadline.TabIndex = 5;

            lblBudget.Name = "lblBudget";
            txtBudget.Name = "txtBudget";
            txtBudget.TabIndex = 6;

            lblPriority.Name = "lblPriority";
            cboPriority.Name = "cboPriority";
            cboPriority.TabIndex = 7;
            cboPriority.DropDownStyle = ComboBoxStyle.DropDownList;

            lblDesc.Name = "lblDesc";
            txtDescription.Name = "txtDescription";
            txtDescription.Multiline = true;
            txtDescription.TabIndex = 8;

            // ── panelFooter ────────────────────────────────────────────────────────
            panelFooter.BackColor = System.Drawing.Color.White;
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Height = 76;
            panelFooter.Name = "panelFooter";
            panelFooter.Controls.AddRange(new Control[]
            { panelFooterLine, lblError, btnSave, btnCancel });

            panelFooterLine.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            panelFooterLine.Dock = DockStyle.Top;
            panelFooterLine.Height = 1;
            panelFooterLine.Name = "panelFooterLine";

            lblError.Location = new System.Drawing.Point(16, 6);
            lblError.Name = "lblError";
            lblError.Size = new System.Drawing.Size(420, 18);
            lblError.Text = "";

            btnSave.Location = new System.Drawing.Point(16, 28);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(270, 40);
            btnSave.Text = "💾  Lưu dự án";
            btnSave.Click += btnSave_Click;

            btnCancel.Location = new System.Drawing.Point(296, 28);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(130, 40);
            btnCancel.Text = "Hủy";
            btnCancel.Click += btnCancel_Click;

            // ── Form ───────────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 590);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProjectEdit";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Dự án";

            // Thứ tự Add: Fill → Bottom → Top
            this.Controls.Add(panelBody);
            this.Controls.Add(panelFooter);
            this.Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelBody.ResumeLayout(false);
            panelBody.PerformLayout();
            panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Private layout helpers ────────────────────────────────────────────

        /// <summary>Tạo Label field chuẩn — dùng UIHelper thay vì hard-code.</summary>
        private static void SetLabel(Label lbl, string text, int x, int y)
        {
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = UIHelper.ColorDark;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.Text = text;
        }

        /// <summary>Tạo TextBox field chuẩn — dùng UIHelper thay vì hard-code.</summary>
        private static void SetTextBox(TextBox txt, string placeholder, int x, int y, int w, int tabIdx)
        {
            txt.BackColor = System.Drawing.Color.White;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = UIHelper.FontBase;
            txt.Location = new System.Drawing.Point(x, y);
            txt.PlaceholderText = placeholder;
            txt.Size = new System.Drawing.Size(w, 30);
            txt.TabIndex = tabIdx;
        }

        /// <summary>Tạo ComboBox field chuẩn.</summary>
        private static void StyleCombo(ComboBox cbo, int x, int y, int w, int tabIdx)
        {
            cbo.BackColor = System.Drawing.Color.White;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.Font = UIHelper.FontBase;
            cbo.Location = new System.Drawing.Point(x, y);
            cbo.Size = new System.Drawing.Size(w, 30);
            cbo.TabIndex = tabIdx;
        }

        // ── Field declarations ────────────────────────────────────────────────
        private Panel panelHeader, panelAccentLine, panelBody, panelFooter, panelFooterLine;
        private Label lblTitleForm, lblError;
        private Label lblName, lblCustomer, lblOwner, lblStart, lblDeadline;
        private Label lblBudget, lblPriority, lblDesc;
        private TextBox txtName, txtBudget, txtDescription;
        private ComboBox cboCustomer, cboOwner, cboPriority;
        private DateTimePicker dtpStartDate, dtpDeadline;
        private CheckBox chkDeadline;
        private Button btnSave, btnCancel;
    }
}