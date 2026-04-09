using System.Windows.Forms;

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

            panelToolbar = new Panel();
            txtSearch = new TextBox();
            cboFilterStatus = new ComboBox();
            btnRefresh = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnStatus = new Button();
            btnMembers = new Button();
            btnDetail = new Button();
            btnKanban = new Button();

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
            panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProjects).BeginInit();
            this.SuspendLayout();

            // ── panelTop ──────────────────────────────────────────
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 56;
            panelTop.Name = "panelTop";
            panelTop.Controls.Add(lblHeader);

            lblHeader.AutoSize = false;
            lblHeader.Location = new Point(20, 13);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(700, 30);
            lblHeader.Text = "📁  Quản lý Dự án";

            // ── panelToolbar (gộp search + filter + actions) ──────
            panelToolbar.Dock = DockStyle.Top;
            panelToolbar.Height = 52;
            panelToolbar.Name = "panelToolbar";
            panelToolbar.Controls.AddRange(new Control[]
            {
                txtSearch, cboFilterStatus, btnRefresh,
                btnAdd, btnEdit, btnDelete,
                btnStatus, btnMembers, btnDetail, btnKanban
            });

            // Search box
            txtSearch.Location = new Point(14, 12);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "🔍  Tìm theo tên, khách hàng, PM...";
            txtSearch.Size = new Size(200, 28);
            txtSearch.TextChanged += txtSearch_TextChanged;

            // ComboBox trạng thái
            cboFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterStatus.Location = new Point(220, 12);
            cboFilterStatus.Name = "cboFilterStatus";
            cboFilterStatus.Size = new Size(150, 28);
            cboFilterStatus.SelectedIndexChanged += cboFilterStatus_SelectedIndexChanged;

            // Nút làm mới
            btnRefresh.Location = new Point(378, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(90, 28);
            btnRefresh.Text = "🔄  Làm mới";
            btnRefresh.Click += btnRefresh_Click;

            // Separator: 14px gap trước nhóm action buttons
            // btnAdd
            btnAdd.Location = new Point(484, 9);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(108, 34);
            btnAdd.Text = "➕  Thêm mới";
            btnAdd.Click += btnAdd_Click;

            btnEdit.Location = new Point(598, 9);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 34);
            btnEdit.Text = "✏️  Sửa";
            btnEdit.Enabled = false;
            btnEdit.Click += btnEdit_Click;

            btnDelete.Location = new Point(684, 9);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 34);
            btnDelete.Text = "🗑️  Xóa";
            btnDelete.Enabled = false;
            btnDelete.Click += btnDelete_Click;

            btnStatus.Location = new Point(770, 9);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(118, 34);
            btnStatus.Text = "🔄  Trạng thái";
            btnStatus.Enabled = false;
            btnStatus.Click += btnStatus_Click;

            btnMembers.Location = new Point(894, 9);
            btnMembers.Name = "btnMembers";
            btnMembers.Size = new Size(118, 34);
            btnMembers.Text = "👥  Thành viên";
            btnMembers.Enabled = false;
            btnMembers.Click += btnMembers_Click;

            btnDetail.Location = new Point(1018, 9);
            btnDetail.Name = "btnDetail";
            btnDetail.Size = new Size(100, 34);
            btnDetail.Text = "📋  Chi tiết";
            btnDetail.Enabled = false;
            btnDetail.Click += btnDetail_Click;

            btnKanban.Location = new Point(1124, 9);
            btnKanban.Name = "btnKanban";
            btnKanban.Size = new Size(108, 34);
            btnKanban.Text = "🗂  Kanban";
            btnKanban.Enabled = false;
            btnKanban.Click += btnKanban_Click;

            // ── dgvProjects ───────────────────────────────────────
            dgvProjects.Dock = DockStyle.Fill;
            dgvProjects.Name = "dgvProjects";
            dgvProjects.BorderStyle = BorderStyle.None;
            dgvProjects.RowTemplate.Height = 38;
            dgvProjects.SelectionChanged += dgvProjects_SelectionChanged;
            dgvProjects.CellDoubleClick += dgvProjects_CellDoubleClick;

            colId.Name = "colId";
            colId.Visible = false;

            colName.Name = "colName";
            colName.HeaderText = "Tên dự án";
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            colCustomer.Name = "colCustomer";
            colCustomer.HeaderText = "Khách hàng";
            colCustomer.Width = 140;

            colOwner.Name = "colOwner";
            colOwner.HeaderText = "PM";
            colOwner.Width = 120;

            colProjStatus.Name = "colProjStatus";
            colProjStatus.HeaderText = "Trạng thái";
            colProjStatus.Width = 145;

            colMembers.Name = "colMembers";
            colMembers.HeaderText = "Thành viên";
            colMembers.Width = 90;

            colDeadline.Name = "colDeadline";
            colDeadline.HeaderText = "Deadline";
            colDeadline.Width = 95;

            colBudget.Name = "colBudget";
            colBudget.HeaderText = "Ngân sách";
            colBudget.Width = 120;

            colStartDate.Name = "colStartDate";
            colStartDate.HeaderText = "Bắt đầu";
            colStartDate.Width = 95;

            dgvProjects.Columns.AddRange(new DataGridViewColumn[]
            {
                colId, colName, colCustomer, colOwner,
                colProjStatus, colMembers, colDeadline, colBudget, colStartDate
            });

            // ── panelStatus ───────────────────────────────────────
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

            // ── Form ──────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1240, 640);
            this.Name = "frmProjects";
            this.Text = "Quản lý Dự án";
            this.StartPosition = FormStartPosition.Manual;

            this.Controls.Add(dgvProjects);
            this.Controls.Add(panelStatus);
            this.Controls.Add(panelToolbar);
            this.Controls.Add(panelTop);

            panelTop.ResumeLayout(false);
            panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProjects).EndInit();
            this.ResumeLayout(false);
        }

        private Panel panelTop;
        private Label lblHeader;
        private Panel panelToolbar;
        private TextBox txtSearch;
        private ComboBox cboFilterStatus;
        private Button btnRefresh;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnStatus;
        private Button btnMembers;
        private Button btnDetail;
        private Button btnKanban;
        private DataGridView dgvProjects;
        private Panel panelStatus;
        private Label lblStatus;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colCustomer;
        private DataGridViewTextBoxColumn colOwner;
        private DataGridViewTextBoxColumn colProjStatus;
        private DataGridViewTextBoxColumn colMembers;
        private DataGridViewTextBoxColumn colDeadline;
        private DataGridViewTextBoxColumn colBudget;
        private DataGridViewTextBoxColumn colStartDate;
    }
}