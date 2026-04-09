namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmExpenseEdit
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitleForm = new Label();
            panelAccentLine = new Panel();

            panelBody = new Panel();
            tableLayout = new TableLayoutPanel();

            lblProject = new Label();
            cboProject = new ComboBox();
            lblType = new Label();
            cboType = new ComboBox();
            lblAmount = new Label();
            numAmount = new NumericUpDown();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            lblNote = new Label();
            txtNote = new TextBox();

            panelFooter = new Panel();
            panelFooterLine = new Panel();
            lblError = new Label();
            flowButtons = new FlowLayoutPanel();
            btnSave = new Button();
            btnCancel = new Button();

            panelHeader.SuspendLayout();
            panelBody.SuspendLayout();
            tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).BeginInit();
            panelFooter.SuspendLayout();
            flowButtons.SuspendLayout();
            this.SuspendLayout();

            // ── panelHeader ───────────────────────────────────────────────────
            panelHeader.Controls.Add(lblTitleForm);
            panelHeader.Controls.Add(panelAccentLine);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 64;
            panelHeader.Name = "panelHeader";

            lblTitleForm.AutoSize = false;
            lblTitleForm.Dock = DockStyle.Fill;
            lblTitleForm.Name = "lblTitleForm";
            lblTitleForm.Padding = new Padding(18, 0, 0, 4);
            lblTitleForm.Text = "➕  Thêm chi phí mới";
            lblTitleForm.TextAlign = ContentAlignment.MiddleLeft;

            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Height = 4;
            panelAccentLine.Name = "panelAccentLine";

            // ── panelBody ─────────────────────────────────────────────────────
            panelBody.Controls.Add(tableLayout);
            panelBody.Dock = DockStyle.Fill;
            panelBody.Name = "panelBody";
            panelBody.Padding = new Padding(20, 16, 20, 0);

            // ── tableLayout (1 cột, 10 hàng: 5 cặp Label + Input) ────────────
            tableLayout.ColumnCount = 1;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            tableLayout.RowCount = 10;
            // Label rows: AutoSize; Input rows: cố định chiều cao để dễ kiểm soát spacing
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // row 0: lblProject
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));     // row 1: cboProject
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // row 2: lblType
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));     // row 3: cboType
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // row 4: lblAmount
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));     // row 5: numAmount
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // row 6: lblDate
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));     // row 7: dtpDate
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // row 8: lblNote
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));     // row 9: txtNote

            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(0);

            // ── lblProject / cboProject ───────────────────────────────────────
            lblProject.AutoSize = true;
            lblProject.Margin = new Padding(0, 0, 0, 4);
            lblProject.Name = "lblProject";
            lblProject.Text = "DỰ ÁN *";
            tableLayout.Controls.Add(lblProject, 0, 0);

            cboProject.Dock = DockStyle.Fill;
            cboProject.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProject.Margin = new Padding(0, 0, 0, 15);
            cboProject.Name = "cboProject";
            cboProject.TabIndex = 1;
            tableLayout.Controls.Add(cboProject, 0, 1);

            // ── lblType / cboType ─────────────────────────────────────────────
            lblType.AutoSize = true;
            lblType.Margin = new Padding(0, 0, 0, 4);
            lblType.Name = "lblType";
            lblType.Text = "LOẠI CHI PHÍ *";
            tableLayout.Controls.Add(lblType, 0, 2);

            cboType.Dock = DockStyle.Fill;
            cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboType.Margin = new Padding(0, 0, 0, 15);
            cboType.Name = "cboType";
            cboType.TabIndex = 2;
            tableLayout.Controls.Add(cboType, 0, 3);

            // ── lblAmount / numAmount ─────────────────────────────────────────
            lblAmount.AutoSize = true;
            lblAmount.Margin = new Padding(0, 0, 0, 4);
            lblAmount.Name = "lblAmount";
            lblAmount.Text = "SỐ TIỀN (VNĐ) *";
            tableLayout.Controls.Add(lblAmount, 0, 4);

            numAmount.Dock = DockStyle.Fill;
            numAmount.Margin = new Padding(0, 0, 0, 15);
            numAmount.Maximum = 999_999_999_999M;
            numAmount.Minimum = 0M;
            numAmount.Name = "numAmount";
            numAmount.TabIndex = 3;
            numAmount.ThousandsSeparator = true;
            tableLayout.Controls.Add(numAmount, 0, 5);

            // ── lblDate / dtpDate ─────────────────────────────────────────────
            lblDate.AutoSize = true;
            lblDate.Margin = new Padding(0, 0, 0, 4);
            lblDate.Name = "lblDate";
            lblDate.Text = "NGÀY PHÁT SINH";
            tableLayout.Controls.Add(lblDate, 0, 6);

            dtpDate.Dock = DockStyle.Fill;
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Margin = new Padding(0, 0, 0, 15);
            dtpDate.Name = "dtpDate";
            dtpDate.TabIndex = 4;
            tableLayout.Controls.Add(dtpDate, 0, 7);

            // ── lblNote / txtNote ─────────────────────────────────────────────
            lblNote.AutoSize = true;
            lblNote.Margin = new Padding(0, 0, 0, 4);
            lblNote.Name = "lblNote";
            lblNote.Text = "GHI CHÚ";
            tableLayout.Controls.Add(lblNote, 0, 8);

            txtNote.Dock = DockStyle.Fill;
            txtNote.Margin = new Padding(0, 0, 0, 0);
            txtNote.Multiline = true;
            txtNote.Name = "txtNote";
            txtNote.PlaceholderText = "Nhập ghi chú (nếu có)...";
            txtNote.TabIndex = 5;
            tableLayout.Controls.Add(txtNote, 0, 9);

            // ── panelFooter ───────────────────────────────────────────────────
            panelFooter.Controls.Add(flowButtons);
            panelFooter.Controls.Add(lblError);
            panelFooter.Controls.Add(panelFooterLine);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Height = 90;
            panelFooter.Name = "panelFooter";

            panelFooterLine.Dock = DockStyle.Top;
            panelFooterLine.Height = 1;
            panelFooterLine.Name = "panelFooterLine";

            lblError.AutoSize = false;
            lblError.Location = new Point(16, 8);
            lblError.Name = "lblError";
            lblError.Size = new Size(380, 20);
            lblError.Text = "";

            // FlowLayoutPanel chứa nút, căn phải, khoảng cách đều
            flowButtons.Controls.Add(btnCancel);
            flowButtons.Controls.Add(btnSave);
            flowButtons.Dock = DockStyle.Bottom;
            flowButtons.FlowDirection = FlowDirection.RightToLeft;
            flowButtons.Height = 55;
            flowButtons.Name = "flowButtons";
            flowButtons.Padding = new Padding(10, 8, 10, 8);
            flowButtons.WrapContents = false;

            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 36);
            btnSave.TabIndex = 10;
            btnSave.Text = "💾  Lưu";

            btnCancel.Margin = new Padding(8, 0, 0, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 36);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Hủy";

            // ── Form ──────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(440, 580);
            this.Controls.Add(panelBody);
            this.Controls.Add(panelFooter);
            this.Controls.Add(panelHeader);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExpenseEdit";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Chi tiết chi phí";

            panelHeader.ResumeLayout(false);
            panelBody.ResumeLayout(false);
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).EndInit();
            panelFooter.ResumeLayout(false);
            flowButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Khai báo controls ─────────────────────────────────────────────────
        private Panel panelHeader, panelAccentLine;
        private Label lblTitleForm;
        private Panel panelBody;
        private TableLayoutPanel tableLayout;
        private Label lblProject, lblType, lblAmount, lblDate, lblNote;
        private ComboBox cboProject, cboType;
        private NumericUpDown numAmount;
        private DateTimePicker dtpDate;
        private TextBox txtNote;
        private Panel panelFooter, panelFooterLine;
        private Label lblError;
        private FlowLayoutPanel flowButtons;
        private Button btnSave, btnCancel;
    }
}