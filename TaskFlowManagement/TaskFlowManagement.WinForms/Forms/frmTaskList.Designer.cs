// ============================================================
//  frmTaskList.Designer.cs  (REFACTORED)
//  TaskFlowManagement.WinForms.Forms
//
//  THAY ĐỔI SO VỚI PHIÊN BẢN CŨ:
//  ─────────────────────────────────────────────────────────
//  [Bug Fix - UI Inconsistency]
//   • panelTop KHÔNG có dark header → ĐÃ THÊM panelHeader riêng
//   • Tất cả button (btnAddNew, btnEdit, btnDelete, btnRefresh)
//     KHÔNG có FlatStyle/BackColor → ĐÃ ÁP DỤNG UIHelper.StyleButton()
//   • Status bar (panelBottom) KHÔNG có dark bg → ĐÃ SỬA
//
//  [Styling]
//   • DataGridView → UIHelper.StyleDataGridView()
//   • Status bar  → UIHelper.CreateStatusBar()
// ============================================================
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmTaskList  // BaseForm declared in frmTaskList.cs
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
            panelBottom = new Panel();
            panelPaging = new Panel();
            dgvTasks = new DataGridView();
            txtSearch = new TextBox();
            cboStatusFilter = new ComboBox();
            cboProjectFilter = new ComboBox();
            btnAddNew = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            lblCount = new Label();
            lblStatus = new Label();
            btnPrev = new Button();
            lblPage = new Label();
            btnNext = new Button();

            colId = new DataGridViewTextBoxColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colProject = new DataGridViewTextBoxColumn();
            colAssignee = new DataGridViewTextBoxColumn();
            colPriority = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colProgress = new DataGridViewTextBoxColumn();
            colDueDate = new DataGridViewTextBoxColumn();

            panelHeader.SuspendLayout();
            panelTop.SuspendLayout();
            panelBottom.SuspendLayout();
            panelPaging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
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
            lblHeader.Text = "📋  Quản lý Công việc";

            // ── panelTop ───────────────────────────────────────────────────────────
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 52;
            panelTop.Name = "panelTop";
            panelTop.Controls.AddRange(new Control[]
            {
        txtSearch, cboProjectFilter, cboStatusFilter,
        btnAddNew, btnEdit, btnDelete, btnRefresh, lblCount
            });

            txtSearch.Location = new Point(14, 13);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "🔍  Tìm kiếm...";
            txtSearch.Size = new Size(200, 27);
            txtSearch.TextChanged += txtSearch_TextChanged;

            cboProjectFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProjectFilter.Location = new Point(224, 13);
            cboProjectFilter.Name = "cboProjectFilter";
            cboProjectFilter.Size = new Size(175, 27);
            cboProjectFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            cboStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatusFilter.Location = new Point(409, 13);
            cboStatusFilter.Name = "cboStatusFilter";
            cboStatusFilter.Size = new Size(165, 27);
            cboStatusFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            // Tọa độ tuyệt đối khớp với bx tích lũy trong ApplyClientStyles()
            btnAddNew.Location = new Point(584, 11); btnAddNew.Name = "btnAddNew"; btnAddNew.Size = new Size(100, 30); btnAddNew.Text = "➕ Thêm mới";
            btnEdit.Location = new Point(690, 11); btnEdit.Name = "btnEdit"; btnEdit.Size = new Size(80, 30); btnEdit.Text = "✏️  Sửa"; btnEdit.Enabled = false;
            btnDelete.Location = new Point(776, 11); btnDelete.Name = "btnDelete"; btnDelete.Size = new Size(80, 30); btnDelete.Text = "🗑️  Xóa"; btnDelete.Enabled = false;
            btnRefresh.Location = new Point(862, 11); btnRefresh.Name = "btnRefresh"; btnRefresh.Size = new Size(90, 30); btnRefresh.Text = "🔄 Làm mới";

            btnAddNew.Click += btnAddNew_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnRefresh.Click += btnRefresh_Click;

            lblCount.AutoSize = false;
            lblCount.Location = new Point(958, 15);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(130, 22);
            lblCount.Text = "0 công việc";
            lblCount.TextAlign = ContentAlignment.MiddleRight;

            // ── dgvTasks ───────────────────────────────────────────────────────────
            dgvTasks.Dock = DockStyle.Fill;
            dgvTasks.Name = "dgvTasks";
            dgvTasks.RowTemplate.Height = 30;
            dgvTasks.SelectionChanged += dgvTasks_SelectionChanged;
            dgvTasks.CellDoubleClick += dgvTasks_CellDoubleClick;

            colId.Name = "colId"; colId.HeaderText = "ID"; colId.Width = 50; colId.Visible = false;
            colTitle.Name = "colTitle"; colTitle.HeaderText = "Tiêu đề công việc"; colTitle.MinimumWidth = 200;
            colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colProject.Name = "colProject"; colProject.HeaderText = "Dự án"; colProject.Width = 150;
            colAssignee.Name = "colAssignee"; colAssignee.HeaderText = "Người thực hiện"; colAssignee.Width = 140;
            colPriority.Name = "colPriority"; colPriority.HeaderText = "Ưu tiên"; colPriority.Width = 85;
            colStatus.Name = "colStatus"; colStatus.HeaderText = "Trạng thái"; colStatus.Width = 110;
            colProgress.Name = "colProgress"; colProgress.HeaderText = "%"; colProgress.Width = 55;
            colDueDate.Name = "colDueDate"; colDueDate.HeaderText = "Hạn chót"; colDueDate.Width = 95;
            // colProgress.DefaultCellStyle.Alignment set trong ApplyClientStyles()

            dgvTasks.Columns.AddRange(new DataGridViewColumn[]
            { colId, colTitle, colProject, colAssignee, colPriority, colStatus, colProgress, colDueDate });

            // ── panelBottom ────────────────────────────────────────────────────────
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 32;
            panelBottom.Name = "panelBottom";
            panelBottom.Controls.AddRange(new Control[] { lblStatus, panelPaging });

            lblStatus.AutoSize = false;
            lblStatus.Location = new Point(12, 7);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(550, 18);
            lblStatus.Text = "Sẵn sàng";

            panelPaging.Dock = DockStyle.Right;
            panelPaging.Name = "panelPaging";
            panelPaging.Width = 225;
            panelPaging.Controls.AddRange(new Control[] { btnPrev, lblPage, btnNext });

            btnPrev.Location = new Point(5, 5);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(65, 24);
            btnPrev.Text = "◀ Trước";
            btnPrev.Enabled = false;
            btnPrev.Click += btnPrev_Click;

            lblPage.Location = new Point(75, 8);
            lblPage.Name = "lblPage";
            lblPage.Size = new Size(90, 18);
            lblPage.Text = "Trang 1 / 1";
            lblPage.TextAlign = ContentAlignment.MiddleCenter;

            btnNext.Location = new Point(170, 5);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(50, 24);
            btnNext.Text = "Sau ▶";
            btnNext.Enabled = false;
            btnNext.Click += btnNext_Click;

            // ── Form ───────────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.MinimumSize = new Size(900, 500);
            this.Name = "frmTaskList";
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "📋  Quản lý Công việc";

            // Thứ tự Add: Fill → Bottom → Top
            this.Controls.Add(dgvTasks);
            this.Controls.Add(panelBottom);
            this.Controls.Add(panelTop);
            this.Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            panelPaging.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
            this.ResumeLayout(false);
        }

        // ── Field declarations ───────────────────────────────────────────────
        private Panel                      panelHeader;
        private Label                      lblHeader;
        private Panel                      panelTop;
        private Panel                      panelBottom;
        private Panel                      panelPaging;
        private DataGridView               dgvTasks;
        private TextBox                    txtSearch;
        private ComboBox                   cboStatusFilter;
        private ComboBox                   cboProjectFilter;
        private Button                     btnAddNew;
        private Button                     btnEdit;
        private Button                     btnDelete;
        private Button                     btnRefresh;
        private Label                      lblCount;
        private Label                      lblStatus;
        private Button                     btnPrev;
        private Label                      lblPage;
        private Button                     btnNext;
        private DataGridViewTextBoxColumn  colId;
        private DataGridViewTextBoxColumn  colTitle;
        private DataGridViewTextBoxColumn  colProject;
        private DataGridViewTextBoxColumn  colAssignee;
        private DataGridViewTextBoxColumn  colPriority;
        private DataGridViewTextBoxColumn  colStatus;
        private DataGridViewTextBoxColumn  colProgress;
        private DataGridViewTextBoxColumn  colDueDate;
    }
}
