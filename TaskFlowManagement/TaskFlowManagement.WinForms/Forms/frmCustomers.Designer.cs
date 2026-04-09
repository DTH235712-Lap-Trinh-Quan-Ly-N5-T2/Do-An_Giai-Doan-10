namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmCustomers
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
            panelAccentLine = new Panel();
            panelTopbar = new Panel();
            flpSearch = new FlowLayoutPanel();
            txtSearch = new TextBox();
            btnRefresh = new Button();
            flpActions = new FlowLayoutPanel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnDetail = new Button();
            dgvCustomers = new DataGridView();
            colCustId = new DataGridViewTextBoxColumn();
            colCompany = new DataGridViewTextBoxColumn();
            colContact = new DataGridViewTextBoxColumn();
            colCustEmail = new DataGridViewTextBoxColumn();
            colCustPhone = new DataGridViewTextBoxColumn();
            colAddress = new DataGridViewTextBoxColumn();
            colCreatedAt = new DataGridViewTextBoxColumn();
            panelStatus = new Panel();
            lblStatus = new Label();

            panelTop.SuspendLayout();
            panelTopbar.SuspendLayout();
            flpSearch.SuspendLayout();
            flpActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            panelStatus.SuspendLayout();
            SuspendLayout();

            // ── panelTop ──────────────────────────────────────────────────────
            panelTop.Controls.Add(lblHeader);
            panelTop.Controls.Add(panelAccentLine);
            panelTop.Dock = DockStyle.Top;
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1286, 72);
            panelTop.TabIndex = 0;

            // ── lblHeader ─────────────────────────────────────────────────────
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new Padding(20, 0, 0, 6);
            lblHeader.Size = new Size(1286, 66);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "🏢  Quản lý Khách hàng";
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;

            // ── panelAccentLine ───────────────────────────────────────────────
            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Name = "panelAccentLine";
            panelAccentLine.Size = new Size(1286, 5);
            panelAccentLine.TabIndex = 1;

            // ── panelTopbar: chứa cả search + actions ─────────────────────────
            panelTopbar.Controls.Add(flpSearch);
            panelTopbar.Controls.Add(flpActions);
            panelTopbar.Dock = DockStyle.Top;
            panelTopbar.Name = "panelTopbar";
            panelTopbar.Size = new Size(1286, 100);
            panelTopbar.TabIndex = 1;

            // ── flpSearch: ô tìm kiếm + nút Làm mới ──────────────────────────
            flpSearch.Controls.Add(txtSearch);
            flpSearch.Controls.Add(btnRefresh);
            flpSearch.AutoSize = true;
            flpSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpSearch.FlowDirection = FlowDirection.LeftToRight;
            flpSearch.WrapContents = false;
            flpSearch.Location = new Point(14, 12);
            flpSearch.Margin = new Padding(0);
            flpSearch.Name = "flpSearch";
            flpSearch.TabIndex = 0;

            // ── txtSearch ─────────────────────────────────────────────────────
            txtSearch.Location = new Point(0, 0);
            txtSearch.Margin = new Padding(0, 0, 10, 0);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(340, 32);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;

            // ── btnRefresh: Margin.Left tạo khoảng cách với TextBox ───────────
            btnRefresh.Location = new Point(0, 0);
            btnRefresh.Margin = new Padding(10, 0, 0, 0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(110, 32);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "🔄  Làm mới";
            btnRefresh.Click += btnRefresh_Click;

            // ── flpActions: nhóm nút CRUD ─────────────────────────────────────
            flpActions.Controls.Add(btnAdd);
            flpActions.Controls.Add(btnEdit);
            flpActions.Controls.Add(btnDelete);
            flpActions.Controls.Add(btnDetail);
            flpActions.AutoSize = true;
            flpActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpActions.FlowDirection = FlowDirection.LeftToRight;
            flpActions.Location = new Point(14, 54);
            flpActions.Margin = new Padding(0);
            flpActions.Name = "flpActions";
            flpActions.TabIndex = 1;
            flpActions.WrapContents = false;

            // ── btnAdd ────────────────────────────────────────────────────────
            btnAdd.Margin = new Padding(0, 0, 8, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.TabIndex = 0;
            btnAdd.Text = "➕  Thêm mới";
            btnAdd.Click += btnAdd_Click;

            // ── btnEdit ───────────────────────────────────────────────────────
            btnEdit.Enabled = false;
            btnEdit.Margin = new Padding(0, 0, 8, 0);
            btnEdit.Name = "btnEdit";
            btnEdit.TabIndex = 1;
            btnEdit.Text = "✏️  Sửa";
            btnEdit.Click += btnEdit_Click;

            // ── btnDelete ─────────────────────────────────────────────────────
            btnDelete.Enabled = false;
            btnDelete.Margin = new Padding(0, 0, 8, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.TabIndex = 2;
            btnDelete.Text = "🗑️  Xóa";
            btnDelete.Click += btnDelete_Click;

            // ── btnDetail ─────────────────────────────────────────────────────
            btnDetail.Enabled = false;
            btnDetail.Margin = new Padding(0, 0, 0, 0);
            btnDetail.Name = "btnDetail";
            btnDetail.TabIndex = 3;
            btnDetail.Text = "📋  Xem dự án";
            btnDetail.Click += btnDetail_Click;

            // ── dgvCustomers ──────────────────────────────────────────────────
            dgvCustomers.ColumnHeadersHeight = 30;
            dgvCustomers.Columns.AddRange(new DataGridViewColumn[]
            {
                colCustId, colCompany, colContact,
                colCustEmail, colCustPhone, colAddress, colCreatedAt
            });
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellDoubleClick += dgvCustomers_CellDoubleClick;
            dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;

            // ── Cột ID (ẩn) ───────────────────────────────────────────────────
            colCustId.HeaderText = "ID";
            colCustId.Name = "colCustId";
            colCustId.Visible = false;
            colCustId.Width = 45;

            // ── Các cột dữ liệu ───────────────────────────────────────────────
            colCompany.HeaderText = "Tên công ty";
            colCompany.Name = "colCompany";

            colContact.HeaderText = "Người liên hệ";
            colContact.Name = "colContact";
            colContact.Width = 150;

            colCustEmail.HeaderText = "Email";
            colCustEmail.Name = "colCustEmail";

            colCustPhone.HeaderText = "Điện thoại";
            colCustPhone.Name = "colCustPhone";
            colCustPhone.Width = 120;

            colAddress.HeaderText = "Địa chỉ";
            colAddress.Name = "colAddress";

            colCreatedAt.HeaderText = "Ngày tạo";
            colCreatedAt.Name = "colCreatedAt";
            colCreatedAt.Width = 110;

            // ── panelStatus ───────────────────────────────────────────────────
            panelStatus.Controls.Add(lblStatus);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(1286, 32);
            panelStatus.TabIndex = 2;

            // ── lblStatus ─────────────────────────────────────────────────────
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(14, 0, 0, 0);
            lblStatus.TabIndex = 0;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;

            // ── frmCustomers ──────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1286, 868);
            Controls.Add(dgvCustomers);
            Controls.Add(panelStatus);
            Controls.Add(panelTopbar);
            Controls.Add(panelTop);
            Name = "frmCustomers";
            StartPosition = FormStartPosition.Manual;
            Text = "🏢  Quản lý Khách hàng";

            panelTop.ResumeLayout(false);
            panelTopbar.ResumeLayout(false);
            panelTopbar.PerformLayout();
            flpSearch.ResumeLayout(false);
            flpSearch.PerformLayout();
            flpActions.ResumeLayout(false);
            flpActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            panelStatus.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Field declarations ────────────────────────────────────────────────
        private Panel panelTop, panelAccentLine, panelTopbar, panelStatus;
        private FlowLayoutPanel flpSearch, flpActions;
        private Label lblHeader, lblStatus;
        private TextBox txtSearch;
        private Button btnRefresh, btnAdd, btnEdit, btnDelete, btnDetail;
        private DataGridView dgvCustomers;
        private DataGridViewTextBoxColumn colCustId, colCompany, colContact;
        private DataGridViewTextBoxColumn colCustEmail, colCustPhone, colAddress, colCreatedAt;
    }
}