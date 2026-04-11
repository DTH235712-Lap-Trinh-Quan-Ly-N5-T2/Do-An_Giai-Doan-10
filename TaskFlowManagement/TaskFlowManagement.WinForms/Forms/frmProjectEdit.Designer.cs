namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmProjectEdit
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitleForm = new Label();
            panelAccentLine = new Panel();
            panelBody = new Panel();
            tableFields = new TableLayoutPanel();
            lblName = new Label();
            txtName = new TextBox();
            lblProjectCode = new Label();
            txtProjectCode = new TextBox();
            lblCustomer = new Label();
            cboCustomer = new ComboBox();
            lblOwner = new Label();
            cboOwner = new ComboBox();
            tableDateTime = new TableLayoutPanel();
            panelStart = new Panel();
            dtpStartDate = new DateTimePicker();
            lblStart = new Label();
            panelDeadline = new Panel();
            dtpDeadline = new DateTimePicker();
            panelDeadlineInput = new Panel();
            chkDeadline = new CheckBox();
            lblDeadline = new Label();
            tableBudgetPriority = new TableLayoutPanel();
            panelBudget = new Panel();
            txtBudget = new TextBox();
            lblBudget = new Label();
            panelPriority = new Panel();
            cboPriority = new ComboBox();
            lblPriority = new Label();
            lblDesc = new Label();
            txtDescription = new TextBox();
            panelFooter = new Panel();
            flowButtons = new FlowLayoutPanel();
            btnSave = new Button();
            btnCancel = new Button();
            lblError = new Label();
            panelFooterLine = new Panel();
            panelHeader.SuspendLayout();
            panelBody.SuspendLayout();
            tableFields.SuspendLayout();
            tableDateTime.SuspendLayout();
            panelStart.SuspendLayout();
            panelDeadline.SuspendLayout();
            panelDeadlineInput.SuspendLayout();
            tableBudgetPriority.SuspendLayout();
            panelBudget.SuspendLayout();
            panelPriority.SuspendLayout();
            panelFooter.SuspendLayout();
            flowButtons.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(lblTitleForm);
            panelHeader.Controls.Add(panelAccentLine);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(549, 85);
            panelHeader.TabIndex = 2;
            // 
            // lblTitleForm
            // 
            lblTitleForm.Dock = DockStyle.Fill;
            lblTitleForm.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitleForm.Location = new Point(0, 0);
            lblTitleForm.Name = "lblTitleForm";
            lblTitleForm.Padding = new Padding(23, 0, 0, 5);
            lblTitleForm.Size = new Size(549, 80);
            lblTitleForm.TabIndex = 0;
            lblTitleForm.Text = "➕  Tạo dự án mới";
            lblTitleForm.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelAccentLine
            // 
            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Location = new Point(0, 80);
            panelAccentLine.Margin = new Padding(3, 4, 3, 4);
            panelAccentLine.Name = "panelAccentLine";
            panelAccentLine.Size = new Size(549, 5);
            panelAccentLine.TabIndex = 1;
            // 
            // panelBody
            // 
            panelBody.AutoScroll = true;
            panelBody.Controls.Add(tableFields);
            panelBody.Dock = DockStyle.Fill;
            panelBody.Location = new Point(0, 85);
            panelBody.Margin = new Padding(3, 4, 3, 4);
            panelBody.Name = "panelBody";
            panelBody.Padding = new Padding(23, 16, 23, 11);
            panelBody.Size = new Size(549, 641);
            panelBody.TabIndex = 0;
            // 
            // tableFields
            // 
            tableFields.AutoSize = true;
            tableFields.ColumnCount = 1;
            tableFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableFields.Controls.Add(lblName, 0, 0);
            tableFields.Controls.Add(txtName, 0, 1);
            tableFields.Controls.Add(lblProjectCode, 0, 2);
            tableFields.Controls.Add(txtProjectCode, 0, 3);
            tableFields.Controls.Add(lblCustomer, 0, 5);
            tableFields.Controls.Add(cboCustomer, 0, 6);
            tableFields.Controls.Add(lblOwner, 0, 8);
            tableFields.Controls.Add(cboOwner, 0, 9);
            tableFields.Controls.Add(tableDateTime, 0, 11);
            tableFields.Controls.Add(tableBudgetPriority, 0, 13);
            tableFields.Controls.Add(lblDesc, 0, 15);
            tableFields.Controls.Add(txtDescription, 0, 16);
            tableFields.Dock = DockStyle.Top;
            tableFields.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableFields.Location = new Point(23, 16);
            tableFields.Margin = new Padding(3, 4, 3, 4);
            tableFields.Name = "tableFields";
            tableFields.RowCount = 17;
            // Row 0: lblName label (27px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            // Row 1: txtName (43px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            // Row 2: lblProjectCode label (27px) — NEW
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            // Row 3: txtProjectCode (43px) — NEW
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            // Row 4: spacer (19px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
            // Row 5: lblCustomer label (27px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            // Row 6: cboCustomer (43px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            // Row 7: spacer (19px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
            // Row 8: lblOwner label (27px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            // Row 9: cboOwner (43px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            // Row 10: spacer (19px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
            // Row 11: tableDateTime (96px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            // Row 12: spacer (19px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
            // Row 13: tableBudgetPriority (96px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            // Row 14: spacer (19px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
            // Row 15: lblDesc label (27px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            // Row 16: txtDescription (93px)
            tableFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 93F));
            tableFields.Size = new Size(503, 703);
            tableFields.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblName.AutoSize = true;
            lblName.Location = new Point(3, 6);
            lblName.Name = "lblName";
            lblName.Size = new Size(0, 21);
            lblName.TabIndex = 0;
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(3, 34);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Nhập tên dự án...";
            txtName.Size = new Size(497, 29);
            txtName.TabIndex = 1;
            // 
            // lblProjectCode
            // 
            lblProjectCode.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblProjectCode.AutoSize = true;
            lblProjectCode.Location = new Point(3, 77);
            lblProjectCode.Name = "lblProjectCode";
            lblProjectCode.Size = new Size(0, 21);
            lblProjectCode.TabIndex = 20;
            lblProjectCode.Text = "Mã dự án:";
            // 
            // txtProjectCode
            // 
            txtProjectCode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtProjectCode.BorderStyle = BorderStyle.FixedSingle;
            txtProjectCode.CharacterCasing = CharacterCasing.Upper;
            txtProjectCode.Location = new Point(3, 104);
            txtProjectCode.Margin = new Padding(3, 4, 3, 4);
            txtProjectCode.MaxLength = 10;
            txtProjectCode.Name = "txtProjectCode";
            txtProjectCode.PlaceholderText = "VD: ALPHA (tự sinh nếu để trống)";
            txtProjectCode.Size = new Size(497, 29);
            txtProjectCode.TabIndex = 21;
            // 
            // lblCustomer
            // 
            lblCustomer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(3, 95);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(0, 21);
            lblCustomer.TabIndex = 2;
            // 
            // cboCustomer
            // 
            cboCustomer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCustomer.Location = new Point(3, 123);
            cboCustomer.Margin = new Padding(3, 4, 3, 4);
            cboCustomer.Name = "cboCustomer";
            cboCustomer.Size = new Size(497, 29);
            cboCustomer.TabIndex = 2;
            // 
            // lblOwner
            // 
            lblOwner.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblOwner.AutoSize = true;
            lblOwner.Location = new Point(3, 184);
            lblOwner.Name = "lblOwner";
            lblOwner.Size = new Size(0, 21);
            lblOwner.TabIndex = 3;
            // 
            // cboOwner
            // 
            cboOwner.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboOwner.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOwner.Location = new Point(3, 212);
            cboOwner.Margin = new Padding(3, 4, 3, 4);
            cboOwner.Name = "cboOwner";
            cboOwner.Size = new Size(497, 29);
            cboOwner.TabIndex = 3;
            // 
            // tableDateTime
            // 
            tableDateTime.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableDateTime.ColumnCount = 2;
            tableDateTime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableDateTime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableDateTime.Controls.Add(panelStart, 0, 0);
            tableDateTime.Controls.Add(panelDeadline, 1, 0);
            tableDateTime.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableDateTime.Location = new Point(3, 271);
            tableDateTime.Margin = new Padding(3, 4, 3, 4);
            tableDateTime.Name = "tableDateTime";
            tableDateTime.RowCount = 1;
            tableDateTime.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableDateTime.Size = new Size(497, 88);
            tableDateTime.TabIndex = 4;
            // 
            // panelStart
            // 
            panelStart.Controls.Add(dtpStartDate);
            panelStart.Controls.Add(lblStart);
            panelStart.Dock = DockStyle.Fill;
            panelStart.Location = new Point(3, 4);
            panelStart.Margin = new Padding(3, 4, 3, 4);
            panelStart.Name = "panelStart";
            panelStart.Padding = new Padding(0, 0, 9, 0);
            panelStart.Size = new Size(242, 80);
            panelStart.TabIndex = 0;
            // 
            // dtpStartDate
            // 
            dtpStartDate.Dock = DockStyle.Top;
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new Point(0, 21);
            dtpStartDate.Margin = new Padding(3, 4, 3, 4);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(233, 29);
            dtpStartDate.TabIndex = 4;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Dock = DockStyle.Top;
            lblStart.Location = new Point(0, 0);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(0, 21);
            lblStart.TabIndex = 5;
            // 
            // panelDeadline
            // 
            panelDeadline.Controls.Add(dtpDeadline);
            panelDeadline.Controls.Add(panelDeadlineInput);
            panelDeadline.Controls.Add(lblDeadline);
            panelDeadline.Dock = DockStyle.Fill;
            panelDeadline.Location = new Point(251, 4);
            panelDeadline.Margin = new Padding(3, 4, 3, 4);
            panelDeadline.Name = "panelDeadline";
            panelDeadline.Padding = new Padding(9, 0, 0, 0);
            panelDeadline.Size = new Size(243, 80);
            panelDeadline.TabIndex = 1;
            // 
            // dtpDeadline
            // 
            dtpDeadline.Dock = DockStyle.Top;
            dtpDeadline.Enabled = false;
            dtpDeadline.Format = DateTimePickerFormat.Short;
            dtpDeadline.Location = new Point(9, 48);
            dtpDeadline.Margin = new Padding(3, 4, 3, 4);
            dtpDeadline.Name = "dtpDeadline";
            dtpDeadline.Size = new Size(234, 29);
            dtpDeadline.TabIndex = 5;
            // 
            // panelDeadlineInput
            // 
            panelDeadlineInput.Controls.Add(chkDeadline);
            panelDeadlineInput.Dock = DockStyle.Top;
            panelDeadlineInput.Location = new Point(9, 21);
            panelDeadlineInput.Margin = new Padding(3, 4, 3, 4);
            panelDeadlineInput.Name = "panelDeadlineInput";
            panelDeadlineInput.Size = new Size(234, 27);
            panelDeadlineInput.TabIndex = 6;
            // 
            // chkDeadline
            // 
            chkDeadline.AutoSize = true;
            chkDeadline.Dock = DockStyle.Left;
            chkDeadline.Location = new Point(0, 0);
            chkDeadline.Margin = new Padding(3, 4, 3, 4);
            chkDeadline.Name = "chkDeadline";
            chkDeadline.Size = new Size(114, 27);
            chkDeadline.TabIndex = 0;
            chkDeadline.Text = "Có deadline";
            chkDeadline.CheckedChanged += chkDeadline_CheckedChanged;
            // 
            // lblDeadline
            // 
            lblDeadline.AutoSize = true;
            lblDeadline.Dock = DockStyle.Top;
            lblDeadline.Location = new Point(9, 0);
            lblDeadline.Name = "lblDeadline";
            lblDeadline.Size = new Size(0, 21);
            lblDeadline.TabIndex = 7;
            // 
            // tableBudgetPriority
            // 
            tableBudgetPriority.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableBudgetPriority.ColumnCount = 2;
            tableBudgetPriority.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableBudgetPriority.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableBudgetPriority.Controls.Add(panelBudget, 0, 0);
            tableBudgetPriority.Controls.Add(panelPriority, 1, 0);
            tableBudgetPriority.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableBudgetPriority.Location = new Point(3, 386);
            tableBudgetPriority.Margin = new Padding(3, 4, 3, 4);
            tableBudgetPriority.Name = "tableBudgetPriority";
            tableBudgetPriority.RowCount = 1;
            tableBudgetPriority.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableBudgetPriority.Size = new Size(497, 88);
            tableBudgetPriority.TabIndex = 5;
            // 
            // panelBudget
            // 
            panelBudget.Controls.Add(txtBudget);
            panelBudget.Controls.Add(lblBudget);
            panelBudget.Dock = DockStyle.Fill;
            panelBudget.Location = new Point(3, 4);
            panelBudget.Margin = new Padding(3, 4, 3, 4);
            panelBudget.Name = "panelBudget";
            panelBudget.Padding = new Padding(0, 0, 9, 0);
            panelBudget.Size = new Size(242, 80);
            panelBudget.TabIndex = 0;
            // 
            // txtBudget
            // 
            txtBudget.BorderStyle = BorderStyle.FixedSingle;
            txtBudget.Dock = DockStyle.Top;
            txtBudget.Location = new Point(0, 21);
            txtBudget.Margin = new Padding(3, 4, 3, 4);
            txtBudget.Name = "txtBudget";
            txtBudget.PlaceholderText = "VD: 500000000";
            txtBudget.Size = new Size(233, 29);
            txtBudget.TabIndex = 6;
            // 
            // lblBudget
            // 
            lblBudget.AutoSize = true;
            lblBudget.Dock = DockStyle.Top;
            lblBudget.Location = new Point(0, 0);
            lblBudget.Name = "lblBudget";
            lblBudget.Size = new Size(0, 21);
            lblBudget.TabIndex = 7;
            // 
            // panelPriority
            // 
            panelPriority.Controls.Add(cboPriority);
            panelPriority.Controls.Add(lblPriority);
            panelPriority.Dock = DockStyle.Fill;
            panelPriority.Location = new Point(251, 4);
            panelPriority.Margin = new Padding(3, 4, 3, 4);
            panelPriority.Name = "panelPriority";
            panelPriority.Padding = new Padding(9, 0, 0, 0);
            panelPriority.Size = new Size(243, 80);
            panelPriority.TabIndex = 1;
            // 
            // cboPriority
            // 
            cboPriority.Dock = DockStyle.Top;
            cboPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPriority.Location = new Point(9, 21);
            cboPriority.Margin = new Padding(3, 4, 3, 4);
            cboPriority.Name = "cboPriority";
            cboPriority.Size = new Size(234, 29);
            cboPriority.TabIndex = 7;
            // 
            // lblPriority
            // 
            lblPriority.AutoSize = true;
            lblPriority.Dock = DockStyle.Top;
            lblPriority.Location = new Point(9, 0);
            lblPriority.Name = "lblPriority";
            lblPriority.Size = new Size(0, 21);
            lblPriority.TabIndex = 8;
            // 
            // lblDesc
            // 
            lblDesc.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblDesc.AutoSize = true;
            lblDesc.Location = new Point(3, 503);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(0, 21);
            lblDesc.TabIndex = 6;
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Location = new Point(3, 528);
            txtDescription.Margin = new Padding(3, 4, 3, 4);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Mô tả chi tiết dự án...";
            txtDescription.Size = new Size(497, 85);
            txtDescription.TabIndex = 8;
            // 
            // panelFooter
            // 
            panelFooter.Controls.Add(flowButtons);
            panelFooter.Controls.Add(lblError);
            panelFooter.Controls.Add(panelFooterLine);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Location = new Point(0, 726);
            panelFooter.Margin = new Padding(3, 4, 3, 4);
            panelFooter.Name = "panelFooter";
            panelFooter.Padding = new Padding(23, 0, 23, 0);
            panelFooter.Size = new Size(549, 101);
            panelFooter.TabIndex = 1;
            // 
            // flowButtons
            // 
            flowButtons.Controls.Add(btnSave);
            flowButtons.Controls.Add(btnCancel);
            flowButtons.Dock = DockStyle.Bottom;
            flowButtons.FlowDirection = FlowDirection.RightToLeft;
            flowButtons.Location = new Point(23, 37);
            flowButtons.Margin = new Padding(3, 4, 3, 4);
            flowButtons.Name = "flowButtons";
            flowButtons.Padding = new Padding(0, 8, 0, 8);
            flowButtons.Size = new Size(503, 64);
            flowButtons.TabIndex = 0;
            flowButtons.WrapContents = false;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSave.Location = new Point(366, 8);
            btnSave.Margin = new Padding(0);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(137, 48);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾  Lưu dự án";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 9.5F);
            btnCancel.Location = new Point(220, 8);
            btnCancel.Margin = new Padding(0, 0, 9, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(137, 48);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Hủy";
            btnCancel.Click += btnCancel_Click;
            // 
            // lblError
            // 
            lblError.Dock = DockStyle.Top;
            lblError.Location = new Point(23, 1);
            lblError.Name = "lblError";
            lblError.Padding = new Padding(0, 5, 0, 0);
            lblError.Size = new Size(503, 27);
            lblError.TabIndex = 1;
            // 
            // panelFooterLine
            // 
            panelFooterLine.Dock = DockStyle.Top;
            panelFooterLine.Location = new Point(23, 0);
            panelFooterLine.Margin = new Padding(3, 4, 3, 4);
            panelFooterLine.Name = "panelFooterLine";
            panelFooterLine.Size = new Size(503, 1);
            panelFooterLine.TabIndex = 2;
            // 
            // frmProjectEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(549, 827);
            Controls.Add(panelBody);
            Controls.Add(panelFooter);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 5, 3, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmProjectEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Dự án";
            panelHeader.ResumeLayout(false);
            panelBody.ResumeLayout(false);
            panelBody.PerformLayout();
            tableFields.ResumeLayout(false);
            tableFields.PerformLayout();
            tableDateTime.ResumeLayout(false);
            panelStart.ResumeLayout(false);
            panelStart.PerformLayout();
            panelDeadline.ResumeLayout(false);
            panelDeadline.PerformLayout();
            panelDeadlineInput.ResumeLayout(false);
            panelDeadlineInput.PerformLayout();
            tableBudgetPriority.ResumeLayout(false);
            panelBudget.ResumeLayout(false);
            panelBudget.PerformLayout();
            panelPriority.ResumeLayout(false);
            panelPriority.PerformLayout();
            panelFooter.ResumeLayout(false);
            flowButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Fields ───────────────────────────────────────────────
        private Panel panelHeader;
        private Panel panelAccentLine;
        private Label lblTitleForm;
        private Panel panelBody;
        private TableLayoutPanel tableFields;
        private Label lblName;
        private TextBox txtName;
        private Label lblCustomer;
        private ComboBox cboCustomer;
        private Label lblOwner;
        private ComboBox cboOwner;
        private TableLayoutPanel tableDateTime;
        private Panel panelStart;
        private Label lblStart;
        private DateTimePicker dtpStartDate;
        private Panel panelDeadline;
        private Label lblDeadline;
        private Panel panelDeadlineInput;
        private CheckBox chkDeadline;
        private DateTimePicker dtpDeadline;
        private TableLayoutPanel tableBudgetPriority;
        private Panel panelBudget;
        private Label lblBudget;
        private TextBox txtBudget;
        private Panel panelPriority;
        private Label lblPriority;
        private ComboBox cboPriority;
        private Label lblProjectCode;
        private TextBox txtProjectCode;
        private Label lblDesc;
        private TextBox txtDescription;
        private Panel panelFooter;
        private Panel panelFooterLine;
        private Label lblError;
        private FlowLayoutPanel flowButtons;
        private Button btnSave;
        private Button btnCancel;
    }
}