using TaskFlowManagement.WinForms.Common;

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
            btnSave = new Button();
            btnCancel = new Button();

            panelHeader.SuspendLayout();
            panelBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).BeginInit();
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
            lblTitleForm.Text = "➕  Thêm chi phí mới";
            lblTitleForm.TextAlign = ContentAlignment.MiddleLeft;

            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Height = 4;
            panelAccentLine.Name = "panelAccentLine";

            // ── panelBody ──────────────────────────────────────────────────────────
            panelBody.Dock = DockStyle.Fill;
            panelBody.Name = "panelBody";
            panelBody.Padding = new Padding(20);
            panelBody.Controls.AddRange(new Control[] {
        lblProject, cboProject, lblType, cboType,
        lblAmount, numAmount, lblDate, dtpDate,
        lblNote, txtNote
    });

            lblProject.Name = "lblProject";
            cboProject.Name = "cboProject";
            lblType.Name = "lblType";
            cboType.Name = "cboType";

            numAmount.Name = "numAmount";
            numAmount.Maximum = 999999999999M;

            dtpDate.Name = "dtpDate";
            dtpDate.Format = DateTimePickerFormat.Short;

            lblNote.Name = "lblNote";
            txtNote.Name = "txtNote";
            txtNote.Multiline = true;

            // ── panelFooter ────────────────────────────────────────────────────────
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Height = 80;
            panelFooter.Name = "panelFooter";
            panelFooter.Controls.AddRange(new Control[] { panelFooterLine, lblError, btnSave, btnCancel });

            panelFooterLine.Dock = DockStyle.Top;
            panelFooterLine.Height = 1;
            panelFooterLine.Name = "panelFooterLine";

            lblError.Location = new Point(20, 6);
            lblError.Name = "lblError";
            lblError.Size = new Size(400, 20);
            lblError.Text = "";

            btnSave.Location = new Point(20, 28);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(250, 40);
            btnSave.Text = "💾  Lưu chi phí";

            btnCancel.Location = new Point(280, 28);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.Text = "Hủy";

            // ── Form ───────────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(420, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExpenseEdit";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Chi tiết chi phí";

            this.Controls.Add(panelBody);
            this.Controls.Add(panelFooter);
            this.Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelBody.ResumeLayout(false);
            panelBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).EndInit();
            panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private static void SetLabel(Label lbl, string text, int x, int y)
        {
            lbl.AutoSize = true;
            lbl.Font = UIHelper.FontLabel;
            lbl.Location = new Point(x, y);
            lbl.Text = text;
        }

        private static void StyleCombo(ComboBox cbo, int x, int y, int w, int tabIdx)
        {
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.Font = UIHelper.FontBase;
            cbo.Location = new Point(x, y);
            cbo.Size = new Size(w, 30);
            cbo.TabIndex = tabIdx;
        }

        private Panel panelHeader, panelAccentLine, panelBody, panelFooter, panelFooterLine;
        private Label lblTitleForm, lblError, lblProject, lblType, lblAmount, lblDate, lblNote;
        private ComboBox cboProject, cboType;
        private NumericUpDown numAmount;
        private DateTimePicker dtpDate;
        private TextBox txtNote;
        private Button btnSave, btnCancel;
    }
}
