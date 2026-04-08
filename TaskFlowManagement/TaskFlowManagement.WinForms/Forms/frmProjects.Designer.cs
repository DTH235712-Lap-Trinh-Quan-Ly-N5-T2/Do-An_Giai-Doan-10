// ============================================================
//  frmProjects.Designer.cs  (REFACTORED)
//  TaskFlowManagement.WinForms.Forms
// ============================================================
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmProjects
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelTop = new Panel();
            lblHeader = new Label();
            panelFilter = new Panel();
            txtSearch = new TextBox();
            cboFilterStatus = new ComboBox();
            btnRefresh = new Button();
            panelToolbar = new Panel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnStatus = new Button();
            btnMembers = new Button();
            btnDetail = new Button();
            btnKanban = new Button();
            lblCount = new Label();
            dgvProjects = new DataGridView();
            panelStatus = new Panel();
            lblStatus = new Label();

            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colCustomer = new DataGridViewTextBoxColumn();
            colOwner = new DataGridViewTextBoxColumn();
            colProjStatus = new DataGridViewTextBoxColumn();
            colMembers = new DataGridViewTextBoxColumn();
            colDeadline = new DataGridViewTextBoxColumn();
            colBudget = new DataGridViewTextBoxColumn();
            colStartDate = new DataGridViewTextBoxColumn();

            panelTop.SuspendLayout();
            panelFilter.SuspendLayout();
            panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProjects).BeginInit();
            this.SuspendLayout();

            // ── panelTop ───────────────────────────────────────────────────────
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 58;
            panelTop.Name = "panelTop";
            panelTop.Controls.Add(lblHeader);

            lblHeader.AutoSize = false;
            lblHeader.Location = new Point(20, 14);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(700, 30);
            lblHeader.Text = "📁  Quản lý Dự án";

            // ── panelFilter ────────────────────────────────────────────────────
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Height = 46;
            panelFilter.Name = "panelFilter";
            panelFilter.Controls.AddRange(new Control[] { txtSearch, cboFilterStatus, btnRefresh });

            txtSearch.Location = new Point(14, 10);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "🔍  Tìm theo tên, khách hàng, PM...";
            txtSearch.Size = new Size(220, 26);
            txtSearch.TextChanged += txtSearch_TextChanged;

            cboFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterStatus.Location = new Point(244, 10);
            cboFilterStatus.Name = "cboFilterStatus";
            cboFilterStatus.Size = new Size(160, 26);
            cboFilterStatus.SelectedIndexChanged += cboFilterStatus_SelectedIndexChanged;
            // Items populated in ApplyClientStyles()

            btnRefresh.Location = new Point(414, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(95, 26);
            btnRefresh.Text = "🔄 Làm mới";
            btnRefresh.Click += btnRefresh_Click;

            // ── panelToolbar ───────────────────────────────────────────────────
            panelToolbar.Dock = DockStyle.Top;
            panelToolbar.Height = 52;
            panelToolbar.Name = "panelToolbar";
            panelToolbar.Controls.AddRange(new Control[]
            { btnAdd, btnEdit, btnDelete, btnStatus, btnMembers, btnDetail, btnKanban, lblCount });

            // Tọa độ tuyệt đối khớp với bx tích lũy trong ApplyClientStyles()
            btnAdd.Location = new Point(14, 9); btnAdd.Name = "btnAdd"; btnAdd.Size = new Size(110, 34); btnAdd.Text = "➕ Thêm mới";
            btnEdit.Location = new Point(130, 9); btnEdit.Name = "btnEdit"; btnEdit.Size = new Size(80, 34); btnEdit.Text = "✏️  Sửa";
            btnDelete.Location = new Point(216, 9); btnDelete.Name = "btnDelete"; btnDelete.Size = new Size(80, 34); btnDelete.Text = "🗑️  Xóa";
            btnStatus.Location = new Point(302, 9); btnStatus.Name = "btnStatus"; btnStatus.Size = new Size(120, 34); btnStatus.Text = "🔄 Trạng thái";
            btnMembers.Location = new Point(428, 9); btnMembers.Name = "btnMembers"; btnMembers.Size = new Size(120, 34); btnMembers.Text = "👥 Thành viên";
            btnDetail.Location = new Point(554, 9); btnDetail.Name = "btnDetail"; btnDetail.Size = new Size(100, 34); btnDetail.Text = "📋 Chi tiết";
            btnKanban.Location = new Point(660, 9); btnKanban.Name = "btnKanban"; btnKanban.Size = new Size(110, 34); btnKanban.Text = "🗂 Kanban";

            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnStatus.Enabled = false;
            btnMembers.Enabled = false;
            btnDetail.Enabled = false;
            btnKanban.Enabled = false;

            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnStatus.Click += btnStatus_Click;
            btnMembers.Click += btnMembers_Click;
            btnDetail.Click += btnDetail_Click;
            btnKanban.Click += btnKanban_Click;

            lblCount.AutoSize = false;
            lblCount.Location = new Point(776, 9);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(140, 34);
            lblCount.TextAlign = ContentAlignment.MiddleLeft;

            // ── dgvProjects ────────────────────────────────────────────────────
            dgvProjects.Dock = DockStyle.Fill;
            dgvProjects.Name = "dgvProjects";
            dgvProjects.RowTemplate.Height = 34;
            dgvProjects.SelectionChanged += dgvProjects_SelectionChanged;
            dgvProjects.CellDoubleClick += dgvProjects_CellDoubleClick;

            colId.Name = "colId"; colId.Visible = false;
            colName.Name = "colName"; colName.HeaderText = "Tên dự án"; colName.Width = 200;
            colCustomer.Name = "colCustomer"; colCustomer.HeaderText = "Khách hàng"; colCustomer.Width = 140;
            colOwner.Name = "colOwner"; colOwner.HeaderText = "PM"; colOwner.Width = 120;
            colProjStatus.Name = "colProjStatus"; colProjStatus.HeaderText = "Trạng thái"; colProjStatus.Width = 145;
            colMembers.Name = "colMembers"; colMembers.HeaderText = "Thành viên"; colMembers.Width = 90;
            colDeadline.Name = "colDeadline"; colDeadline.HeaderText = "Deadline"; colDeadline.Width = 95;
            colBudget.Name = "colBudget"; colBudget.HeaderText = "Ngân sách"; colBudget.Width = 115;
            colStartDate.Name = "colStartDate"; colStartDate.HeaderText = "Bắt đầu";
            colStartDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvProjects.Columns.AddRange(new DataGridViewColumn[]
            { colId, colName, colCustomer, colOwner, colProjStatus, colMembers, colDeadline, colBudget, colStartDate });

            // ── panelStatus (placeholder — CreateStatusBar thay thế khi runtime) ─
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

            // ── Form ───────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1000, 600);
            this.Name = "frmProjects";
            this.Text = "Quản lý Dự án";
            this.StartPosition = FormStartPosition.Manual;

            // Thứ tự Add: Fill → Bottom → Top
            this.Controls.Add(dgvProjects);
            this.Controls.Add(panelStatus);
            this.Controls.Add(panelToolbar);
            this.Controls.Add(panelFilter);
            this.Controls.Add(panelTop);

            panelTop.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProjects).EndInit();
            this.ResumeLayout(false);
        }

        // ── Field declarations ─────────────────────────────────────────────────
        private Panel panelTop, panelFilter, panelToolbar, panelStatus;
        private Label lblHeader, lblCount, lblStatus;
        private TextBox txtSearch;
        private ComboBox cboFilterStatus;
        private Button btnRefresh, btnAdd, btnEdit, btnDelete, btnStatus, btnMembers, btnDetail, btnKanban;
        private DataGridView dgvProjects;
        private DataGridViewTextBoxColumn colId, colName, colCustomer, colOwner;
        private DataGridViewTextBoxColumn colProjStatus, colMembers, colDeadline, colBudget, colStartDate;
    }
}