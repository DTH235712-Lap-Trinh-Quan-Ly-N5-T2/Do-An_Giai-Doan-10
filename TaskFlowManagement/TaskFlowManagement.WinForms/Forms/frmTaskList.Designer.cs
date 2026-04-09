namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmTaskList
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
            lblHeader = new Label();

            panelTop = new Panel();
            flowToolbar = new FlowLayoutPanel();
            txtSearch = new TextBox();
            cboProjectFilter = new ComboBox();
            cboStatusFilter = new ComboBox();
            btnAddNew = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();

            dgvTasks = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colProject = new DataGridViewTextBoxColumn();
            colAssignee = new DataGridViewTextBoxColumn();
            colPriority = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colProgress = new DataGridViewTextBoxColumn();
            colDueDate = new DataGridViewTextBoxColumn();

            panelBottom = new Panel();
            lblStatus = new Label();
            flowPaging = new FlowLayoutPanel();
            btnPrev = new Button();
            lblPage = new Label();
            btnNext = new Button();
            lblCount = new Label();

            panelHeader.SuspendLayout();
            panelTop.SuspendLayout();
            flowToolbar.SuspendLayout();
            panelBottom.SuspendLayout();
            flowPaging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
            this.SuspendLayout();

            // ── panelHeader ──────────────────────────────────────
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 56;
            panelHeader.Name = "panelHeader";
            panelHeader.Controls.Add(lblHeader);

            lblHeader.AutoSize = false;
            lblHeader.Location = new Point(20, 13);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(700, 30);
            lblHeader.Text = "📋  Quản lý Công việc";

            // ── panelTop — toolbar ───────────────────────────────
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 50;
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(10, 0, 10, 0);
            panelTop.Controls.Add(flowToolbar);

            // flowToolbar — tất cả controls tự sắp xếp, không bị chồng lên nhau
            flowToolbar.AutoSize = false;
            flowToolbar.Dock = DockStyle.Fill;
            flowToolbar.FlowDirection = FlowDirection.LeftToRight;
            flowToolbar.Name = "flowToolbar";
            flowToolbar.Padding = new Padding(0, 10, 0, 0);
            flowToolbar.WrapContents = false;
            flowToolbar.Controls.AddRange(new Control[]
            {
                txtSearch, cboProjectFilter, cboStatusFilter,
                btnAddNew, btnEdit, btnDelete, btnRefresh
            });

            txtSearch.Margin = new Padding(0, 0, 6, 0);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "🔍  Tìm kiếm...";
            txtSearch.Size = new Size(200, 28);
            txtSearch.TextChanged += txtSearch_TextChanged;

            cboProjectFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProjectFilter.Margin = new Padding(0, 0, 6, 0);
            cboProjectFilter.Name = "cboProjectFilter";
            cboProjectFilter.Size = new Size(170, 28);
            cboProjectFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            cboStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatusFilter.Margin = new Padding(0, 0, 16, 0);
            cboStatusFilter.Name = "cboStatusFilter";
            cboStatusFilter.Size = new Size(155, 28);
            cboStatusFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            btnAddNew.Margin = new Padding(0, 0, 6, 0);
            btnAddNew.Name = "btnAddNew";
            btnAddNew.Size = new Size(108, 28);
            btnAddNew.Text = "➕  Thêm mới";
            btnAddNew.Click += btnAddNew_Click;

            btnEdit.Enabled = false;
            btnEdit.Margin = new Padding(0, 0, 6, 0);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(78, 28);
            btnEdit.Text = "✏️  Sửa";
            btnEdit.Click += btnEdit_Click;

            btnDelete.Enabled = false;
            btnDelete.Margin = new Padding(0, 0, 6, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(78, 28);
            btnDelete.Text = "🗑️  Xóa";
            btnDelete.Click += btnDelete_Click;

            btnRefresh.Margin = new Padding(0, 0, 0, 0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(96, 28);
            btnRefresh.Text = "🔄  Làm mới";
            btnRefresh.Click += btnRefresh_Click;

            // ── dgvTasks ─────────────────────────────────────────
            dgvTasks.Dock = DockStyle.Fill;
            dgvTasks.Name = "dgvTasks";
            dgvTasks.BorderStyle = BorderStyle.None;
            dgvTasks.RowTemplate.Height = 36;
            dgvTasks.SelectionChanged += dgvTasks_SelectionChanged;
            dgvTasks.CellDoubleClick += dgvTasks_CellDoubleClick;

            colId.Name = "colId";
            colId.Visible = false;

            colTitle.Name = "colTitle";
            colTitle.HeaderText = "Tiêu đề công việc";
            colTitle.MinimumWidth = 180;
            colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            colProject.Name = "colProject";
            colProject.HeaderText = "Dự án";
            colProject.Width = 150;

            colAssignee.Name = "colAssignee";
            colAssignee.HeaderText = "Người thực hiện";
            colAssignee.Width = 140;

            colPriority.Name = "colPriority";
            colPriority.HeaderText = "Ưu tiên";
            colPriority.Width = 85;

            colStatus.Name = "colStatus";
            colStatus.HeaderText = "Trạng thái";
            colStatus.Width = 115;

            colProgress.Name = "colProgress";
            colProgress.HeaderText = "%";
            colProgress.Width = 55;

            colDueDate.Name = "colDueDate";
            colDueDate.HeaderText = "Hạn chót";
            colDueDate.Width = 95;

            dgvTasks.Columns.AddRange(new DataGridViewColumn[]
            { colId, colTitle, colProject, colAssignee, colPriority, colStatus, colProgress, colDueDate });

            // ── panelBottom — status bar + phân trang ────────────
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 34;
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(12, 0, 8, 0);
            panelBottom.Controls.Add(flowPaging);
            panelBottom.Controls.Add(lblCount);
            panelBottom.Controls.Add(lblStatus);

            // lblStatus — bên trái
            lblStatus.AutoSize = false;
            lblStatus.Dock = DockStyle.Left;
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(320, 34);
            lblStatus.Text = "Sẵn sàng";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;

            // lblCount — bên trái, sau lblStatus
            lblCount.AutoSize = false;
            lblCount.Dock = DockStyle.Left;
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(150, 34);
            lblCount.Text = "";
            lblCount.TextAlign = ContentAlignment.MiddleLeft;

            // flowPaging — bên phải, không bao giờ bị đè
            flowPaging.AutoSize = false;
            flowPaging.Dock = DockStyle.Right;
            flowPaging.FlowDirection = FlowDirection.LeftToRight;
            flowPaging.Name = "flowPaging";
            flowPaging.Padding = new Padding(0, 4, 0, 4);
            flowPaging.Size = new Size(260, 34);
            flowPaging.WrapContents = false;
            flowPaging.Controls.AddRange(new Control[] { btnPrev, lblPage, btnNext });

            btnPrev.Enabled = false;
            btnPrev.Margin = new Padding(0, 0, 4, 0);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(80, 26);
            btnPrev.Text = "◀  Trước";
            btnPrev.Click += btnPrev_Click;

            lblPage.AutoSize = false;
            lblPage.Margin = new Padding(0, 0, 4, 0);
            lblPage.Name = "lblPage";
            lblPage.Size = new Size(95, 26);
            lblPage.Text = "Trang 1 / 1";
            lblPage.TextAlign = ContentAlignment.MiddleCenter;

            btnNext.Enabled = false;
            btnNext.Margin = new Padding(0);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(72, 26);
            btnNext.Text = "Sau  ▶";
            btnNext.Click += btnNext_Click;

            // ── Form ─────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.MinimumSize = new Size(960, 500);
            this.Name = "frmTaskList";
            this.Size = new Size(1140, 660);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "📋  Quản lý Công việc";

            this.Controls.Add(dgvTasks);
            this.Controls.Add(panelBottom);
            this.Controls.Add(panelTop);
            this.Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            flowToolbar.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            flowPaging.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
            this.ResumeLayout(false);
        }

        // ── Fields ───────────────────────────────────────────────
        private Panel panelHeader;
        private Label lblHeader;
        private Panel panelTop;
        private FlowLayoutPanel flowToolbar;
        private TextBox txtSearch;
        private ComboBox cboProjectFilter;
        private ComboBox cboStatusFilter;
        private Button btnAddNew;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private DataGridView dgvTasks;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colProject;
        private DataGridViewTextBoxColumn colAssignee;
        private DataGridViewTextBoxColumn colPriority;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colProgress;
        private DataGridViewTextBoxColumn colDueDate;
        private Panel panelBottom;
        private Label lblStatus;
        private Label lblCount;
        private FlowLayoutPanel flowPaging;
        private Button btnPrev;
        private Label lblPage;
        private Button btnNext;
    }
}