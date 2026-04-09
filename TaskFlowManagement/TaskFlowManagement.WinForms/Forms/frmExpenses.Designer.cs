// ============================================================
//  frmExpenses.Designer.cs
//  TaskFlowManagement.WinForms.Forms
// ============================================================
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmExpenses
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Khởi tạo toàn bộ control ──────────────────────────────────────
            panelHeader = new Panel();
            lblHeader = new Label();

            panelFilter = new Panel();
            lblProjectFilter = new Label();
            cboProject = new ComboBox();
            lblTypeFilter = new Label();
            cboExpenseType = new ComboBox();
            btnRefresh = new Button();

            panelSummary = new Panel();

            // Các label vùng thống kê — layout do tlpSummary trong ApplyClientStyles()
            lblBudgetTitle = new Label();
            lblBudgetVal = new Label();
            lblTotalExpenseTitle = new Label();
            lblTotalExpenseVal = new Label();
            lblRemainingTitle = new Label();
            lblRemainingVal = new Label();
            lblUsagePct = new Label();

            panelToolbar = new Panel();
            flpToolbar = new FlowLayoutPanel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnDetail = new Button();
            btnExportReport = new Button();
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
            panelToolbar.SuspendLayout();
            flpToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).BeginInit();
            panelStatus.SuspendLayout();
            this.SuspendLayout();

            // ── panelHeader ────────────────────────────────────────────────────
            panelHeader.Controls.Add(lblHeader);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 58;
            panelHeader.Name = "panelHeader";

            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new Padding(18, 0, 0, 0);
            lblHeader.Text = "💸  Quản lý Chi phí & Ngân sách";
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── panelFilter ────────────────────────────────────────────────────
            panelFilter.Controls.AddRange(new Control[]
                { lblProjectFilter, cboProject, lblTypeFilter, cboExpenseType, btnRefresh });
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Height = 46;
            panelFilter.Name = "panelFilter";

            lblProjectFilter.AutoSize = true;
            lblProjectFilter.Location = new System.Drawing.Point(14, 14);
            lblProjectFilter.Name = "lblProjectFilter";
            lblProjectFilter.Text = "Dự án:";

            cboProject.Location = new System.Drawing.Point(65, 10);
            cboProject.Name = "cboProject";
            cboProject.Size = new System.Drawing.Size(220, 26);

            lblTypeFilter.AutoSize = true;
            lblTypeFilter.Location = new System.Drawing.Point(300, 14);
            lblTypeFilter.Name = "lblTypeFilter";
            lblTypeFilter.Text = "Loại:";

            cboExpenseType.Location = new System.Drawing.Point(340, 10);
            cboExpenseType.Name = "cboExpenseType";
            cboExpenseType.Size = new System.Drawing.Size(160, 26);

            btnRefresh.Location = new System.Drawing.Point(515, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(110, 26);
            btnRefresh.Text = "🔄 Làm mới";

            // ── panelSummary — nội dung do tlpSummary trong ApplyClientStyles() ──
            panelSummary.Dock = DockStyle.Top;
            panelSummary.Height = 110;
            panelSummary.Name = "panelSummary";
            panelSummary.Padding = new Padding(10, 10, 10, 6);

            // Khai báo tên các label thống kê — vị trí giao cho tlpSummary
            lblBudgetTitle.Name = "lblBudgetTitle";
            lblBudgetTitle.Text = "NGÂN SÁCH";
            lblBudgetVal.Name = "lblBudgetVal";
            lblBudgetVal.Text = "—";
            lblTotalExpenseTitle.Name = "lblTotalExpenseTitle";
            lblTotalExpenseTitle.Text = "TỔNG CHI PHÍ";
            lblTotalExpenseVal.Name = "lblTotalExpenseVal";
            lblTotalExpenseVal.Text = "—";
            lblRemainingTitle.Name = "lblRemainingTitle";
            lblRemainingTitle.Text = "CÒN LẠI";
            lblRemainingVal.Name = "lblRemainingVal";
            lblRemainingVal.Text = "—";
            lblUsagePct.Name = "lblUsagePct";
            lblUsagePct.Text = "0%";

            // ── panelToolbar ───────────────────────────────────────────────────
            panelToolbar.Controls.AddRange(new Control[] { flpToolbar, lblCount });
            panelToolbar.Dock = DockStyle.Top;
            panelToolbar.Height = 52;
            panelToolbar.Name = "panelToolbar";

            flpToolbar.Controls.AddRange(new Control[]
                { btnAdd, btnEdit, btnDelete, btnDetail, btnExportReport });
            flpToolbar.Dock = DockStyle.Left;
            flpToolbar.AutoSize = true;
            flpToolbar.FlowDirection = FlowDirection.LeftToRight;
            flpToolbar.Padding = new Padding(10, 9, 0, 0);
            flpToolbar.WrapContents = false;
            flpToolbar.Name = "flpToolbar";

            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(140, 34);
            btnAdd.Text = "➕ Thêm chi phí";
            btnAdd.Margin = new Padding(0, 0, 6, 0);

            btnEdit.Name = "btnEdit";
            btnEdit.Size = new System.Drawing.Size(80, 34);
            btnEdit.Text = "✏️ Sửa";
            btnEdit.Margin = new Padding(0, 0, 6, 0);
            btnEdit.Enabled = false;

            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(80, 34);
            btnDelete.Text = "🗑️ Xóa";
            btnDelete.Margin = new Padding(0, 0, 6, 0);
            btnDelete.Enabled = false;

            btnDetail.Name = "btnDetail";
            btnDetail.Size = new System.Drawing.Size(100, 34);
            btnDetail.Text = "📋 Chi tiết";
            btnDetail.Margin = new Padding(0, 0, 6, 0);
            btnDetail.Enabled = false;

            btnExportReport.Name = "btnExportReport";
            btnExportReport.Size = new System.Drawing.Size(145, 34);
            btnExportReport.Text = "📊 Xuất báo cáo";
            btnExportReport.Margin = new Padding(0, 0, 0, 0);

            lblCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCount.AutoSize = false;
            lblCount.Name = "lblCount";
            lblCount.Size = new System.Drawing.Size(200, 34);
            lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ── dgvExpenses ────────────────────────────────────────────────────
            dgvExpenses.Dock = DockStyle.Fill;
            dgvExpenses.Name = "dgvExpenses";

            colId.Name = "colId";
            colId.HeaderText = "ID";
            colId.Visible = false;

            colProject.Name = "colProject";
            colProject.HeaderText = "Dự án";
            colProject.Width = 180;

            colType.Name = "colType";
            colType.HeaderText = "Loại chi phí";
            colType.Width = 130;

            colAmount.Name = "colAmount";
            colAmount.HeaderText = "Số tiền";
            colAmount.Width = 140;

            colDate.Name = "colDate";
            colDate.HeaderText = "Ngày";
            colDate.Width = 100;

            colNote.Name = "colNote";
            colNote.HeaderText = "Ghi chú";
            colNote.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNote.MinimumWidth = 150;

            colCreatedBy.Name = "colCreatedBy";
            colCreatedBy.HeaderText = "Người tạo";
            colCreatedBy.Width = 140;

            dgvExpenses.Columns.AddRange(new DataGridViewColumn[]
                { colId, colProject, colType, colAmount, colDate, colNote, colCreatedBy });

            // ── panelStatus ────────────────────────────────────────────────────
            panelStatus.Controls.Add(lblStatus);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Height = 28;
            panelStatus.Name = "panelStatus";

            lblStatus.AutoSize = false;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(12, 0, 0, 0);
            lblStatus.Text = "Sẵn sàng";
            lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── Form ───────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 720);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmExpenses";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "💸  Quản lý Chi phí & Ngân sách";

            // Thứ tự Add: Fill → Bottom → Top
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
            panelToolbar.ResumeLayout(false);
            flpToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).EndInit();
            panelStatus.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #region Control declarations

        private Panel panelHeader;
        private Label lblHeader;
        private Panel panelFilter;
        private Label lblProjectFilter;
        private ComboBox cboProject;
        private Label lblTypeFilter;
        private ComboBox cboExpenseType;
        private Button btnRefresh;
        private Panel panelSummary;
        private Label lblBudgetTitle;
        private Label lblBudgetVal;
        private Label lblTotalExpenseTitle;
        private Label lblTotalExpenseVal;
        private Label lblRemainingTitle;
        private Label lblRemainingVal;
        private Label lblUsagePct;
        private Panel panelToolbar;
        private FlowLayoutPanel flpToolbar;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnDetail;
        private Button btnExportReport;
        private Label lblCount;
        private DataGridView dgvExpenses;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colProject;
        private DataGridViewTextBoxColumn colType;
        private DataGridViewTextBoxColumn colAmount;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colNote;
        private DataGridViewTextBoxColumn colCreatedBy;
        private Panel panelStatus;
        private Label lblStatus;

        #endregion
    }
}