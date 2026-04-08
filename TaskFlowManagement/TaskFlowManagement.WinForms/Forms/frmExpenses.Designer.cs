using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmExpenses
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
            lblHeader = new Label();
            panelFilter = new Panel();
            lblProjectFilter = new Label();
            cboProject = new ComboBox();
            lblTypeFilter = new Label();
            cboExpenseType = new ComboBox();
            btnRefresh = new Button();
            panelSummary = new Panel();
            pnlSummaryCard = new Panel();
            lblUsagePct = new Label();
            lblRemainingVal = new Label();
            lblTotalExpenseVal = new Label();
            lblBudgetVal = new Label();
            lblRemainingTitle = new Label();
            lblTotalExpenseTitle = new Label();
            lblBudgetTitle = new Label();
            panelToolbar = new Panel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnDetail = new Button();
            lblCount = new Label();
            dgvExpenses = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colProject = new DataGridViewTextBoxColumn();
            colType = new DataGridViewTextBoxColumn();
            colAmount = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colNote = new DataGridViewTextBoxColumn();
            colCreatedBy = new DataGridViewTextBoxColumn();
            panelStatus = new Panel();
            lblStatus = new Label();

            panelHeader.SuspendLayout();
            panelFilter.SuspendLayout();
            panelSummary.SuspendLayout();
            pnlSummaryCard.SuspendLayout();
            panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).BeginInit();
            this.SuspendLayout();

            // ── panelHeader ────────────────────────────────────────────────────────
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 58;
            panelHeader.Name = "panelHeader";
            panelHeader.Controls.Add(lblHeader);

            lblHeader.AutoSize = false;
            lblHeader.Location = new Point(20, 14);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(700, 30);
            lblHeader.Text = "💸  Quản lý Chi phí & Ngân sách";

            // ── panelFilter ────────────────────────────────────────────────────────
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Height = 46;
            panelFilter.Name = "panelFilter";
            panelFilter.Controls.AddRange(new Control[]
            { lblProjectFilter, cboProject, lblTypeFilter, cboExpenseType, btnRefresh });

            lblProjectFilter.AutoSize = true;
            lblProjectFilter.Location = new Point(14, 15);
            lblProjectFilter.Name = "lblProjectFilter";
            lblProjectFilter.Text = "Dự án:";

            cboProject.Location = new Point(65, 11);
            cboProject.Name = "cboProject";
            cboProject.Size = new Size(220, 26);

            lblTypeFilter.AutoSize = true;
            lblTypeFilter.Location = new Point(300, 15);
            lblTypeFilter.Name = "lblTypeFilter";
            lblTypeFilter.Text = "Loại:";

            cboExpenseType.Location = new Point(340, 11);
            cboExpenseType.Name = "cboExpenseType";
            cboExpenseType.Size = new Size(160, 26);

            btnRefresh.Location = new Point(515, 11);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(110, 26);
            btnRefresh.Text = "🔄 Làm mới";

            // ── panelSummary ───────────────────────────────────────────────────────
            panelSummary.Dock = DockStyle.Top;
            panelSummary.Height = 100;
            panelSummary.Name = "panelSummary";
            panelSummary.Controls.Add(pnlSummaryCard);

            pnlSummaryCard.Location = new Point(14, 10);
            pnlSummaryCard.Name = "pnlSummaryCard";
            pnlSummaryCard.Size = new Size(970, 80);
            pnlSummaryCard.BorderStyle = BorderStyle.FixedSingle;
            pnlSummaryCard.Controls.AddRange(new Control[]
            {
        lblUsagePct, lblRemainingVal, lblTotalExpenseVal, lblBudgetVal,
        lblRemainingTitle, lblTotalExpenseTitle, lblBudgetTitle
            });

            lblBudgetTitle.Location = new Point(20, 15);
            lblBudgetTitle.Name = "lblBudgetTitle";
            lblBudgetTitle.Size = new Size(150, 20);
            lblBudgetTitle.Text = "NGÂN SÁCH";

            lblBudgetVal.Location = new Point(20, 35);
            lblBudgetVal.Name = "lblBudgetVal";
            lblBudgetVal.Size = new Size(200, 30);
            lblBudgetVal.Text = "0 ₫";

            lblTotalExpenseTitle.Location = new Point(250, 15);
            lblTotalExpenseTitle.Name = "lblTotalExpenseTitle";
            lblTotalExpenseTitle.Size = new Size(150, 20);
            lblTotalExpenseTitle.Text = "TỔNG CHI PHÍ";

            lblTotalExpenseVal.Location = new Point(250, 35);
            lblTotalExpenseVal.Name = "lblTotalExpenseVal";
            lblTotalExpenseVal.Size = new Size(200, 30);
            lblTotalExpenseVal.Text = "0 ₫";

            lblRemainingTitle.Location = new Point(500, 15);
            lblRemainingTitle.Name = "lblRemainingTitle";
            lblRemainingTitle.Size = new Size(150, 20);
            lblRemainingTitle.Text = "CÒN LẠI";

            lblRemainingVal.Location = new Point(500, 35);
            lblRemainingVal.Name = "lblRemainingVal";
            lblRemainingVal.Size = new Size(200, 30);
            lblRemainingVal.Text = "0 ₫";

            lblUsagePct.Location = new Point(780, 15);
            lblUsagePct.Name = "lblUsagePct";
            lblUsagePct.Size = new Size(160, 50);
            lblUsagePct.Text = "0%";
            lblUsagePct.TextAlign = ContentAlignment.MiddleRight;

            // ── panelToolbar ───────────────────────────────────────────────────────
            panelToolbar.Dock = DockStyle.Top;
            panelToolbar.Height = 52;
            panelToolbar.Name = "panelToolbar";
            panelToolbar.Controls.AddRange(new Control[]
            { btnAdd, btnEdit, btnDelete, btnDetail, lblCount });

            btnAdd.Location = new Point(14, 9);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(140, 34);
            btnAdd.Text = "➕ Thêm chi phí";

            btnEdit.Location = new Point(160, 9);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 34);
            btnEdit.Text = "✏️ Sửa";
            btnEdit.Enabled = false;

            btnDelete.Location = new Point(246, 9);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 34);
            btnDelete.Text = "🗑️ Xóa";
            btnDelete.Enabled = false;

            btnDetail.Location = new Point(332, 9);
            btnDetail.Name = "btnDetail";
            btnDetail.Size = new Size(100, 34);
            btnDetail.Text = "📋 Chi tiết";
            btnDetail.Enabled = false;

            lblCount.AutoSize = false;
            lblCount.Location = new Point(444, 9);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(200, 34);
            lblCount.TextAlign = ContentAlignment.MiddleLeft;

            // ── dgvExpenses ────────────────────────────────────────────────────────
            dgvExpenses.Dock = DockStyle.Fill;
            dgvExpenses.Name = "dgvExpenses";
            dgvExpenses.RowTemplate.Height = 34;

            colId.Name = "colId"; colId.Visible = false;
            colProject.Name = "colProject"; colProject.HeaderText = "Dự án"; colProject.Width = 180;
            colType.Name = "colType"; colType.HeaderText = "Loại chi phí"; colType.Width = 140;
            colAmount.Name = "colAmount"; colAmount.HeaderText = "Số tiền"; colAmount.Width = 140;
            colDate.Name = "colDate"; colDate.HeaderText = "Ngày"; colDate.Width = 100;
            colNote.Name = "colNote"; colNote.HeaderText = "Ghi chú"; colNote.Width = 250;
            colCreatedBy.Name = "colCreatedBy"; colCreatedBy.HeaderText = "Người tạo";
            colCreatedBy.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvExpenses.Columns.AddRange(new DataGridViewColumn[]
            { colId, colProject, colType, colAmount, colDate, colNote, colCreatedBy });

            // ── panelStatus (placeholder — được tạo lại bởi UIHelper.CreateStatusBar) ──
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Height = 28;
            panelStatus.Name = "panelStatus";
            panelStatus.Controls.Add(lblStatus);

            lblStatus.AutoSize = false;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(12, 0, 0, 0);
            lblStatus.Text = "";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;

            // ── Form ───────────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1000, 700);
            this.Name = "frmExpenses";
            this.Text = "Quản lý Chi phí & Ngân sách";
            this.StartPosition = FormStartPosition.CenterParent;

            // Thứ tự Add: Fill → Bottom → Top (ngược chiều Dock)
            this.Controls.Add(dgvExpenses);
            this.Controls.Add(panelStatus);
            this.Controls.Add(panelToolbar);
            this.Controls.Add(panelSummary);
            this.Controls.Add(panelFilter);
            this.Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            panelSummary.ResumeLayout(false);
            pnlSummaryCard.ResumeLayout(false);
            panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).EndInit();
            this.ResumeLayout(false);
        }

        private Panel panelHeader, panelFilter, panelSummary, pnlSummaryCard, panelToolbar, panelStatus;
        private Label lblHeader, lblProjectFilter, lblTypeFilter, lblBudgetTitle, lblBudgetVal, lblTotalExpenseTitle, lblTotalExpenseVal, lblRemainingTitle, lblRemainingVal, lblUsagePct, lblCount, lblStatus;
        private ComboBox cboProject, cboExpenseType;
        private Button btnRefresh, btnAdd, btnEdit, btnDelete, btnDetail, btnExportReport;
        private DataGridView dgvExpenses;
        private DataGridViewTextBoxColumn colId, colProject, colType, colAmount, colDate, colNote, colCreatedBy;
    }
}
