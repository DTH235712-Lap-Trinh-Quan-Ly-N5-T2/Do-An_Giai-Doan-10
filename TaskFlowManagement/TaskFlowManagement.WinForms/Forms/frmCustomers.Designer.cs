using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmCustomers   // BaseForm declared in frmCustomers.cs
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
            panelFilter = new Panel();
            txtSearch = new TextBox();
            btnRefresh = new Button();
            panelToolbar = new Panel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnDetail = new Button();
            lblCount = new Label();
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
            panelFilter.SuspendLayout();
            panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            panelStatus.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(lblHeader);
            panelTop.Controls.Add(panelAccentLine);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(4, 4, 4, 4);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1286, 81);
            panelTop.TabIndex = 4;
            // 
            // lblHeader
            // 
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Location = new Point(0, 0);
            lblHeader.Margin = new Padding(4, 0, 4, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new Padding(21, 0, 0, 6);
            lblHeader.Size = new Size(1286, 75);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "🏢  Quản lý Khách hàng";
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelAccentLine
            // 
            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Location = new Point(0, 75);
            panelAccentLine.Margin = new Padding(4, 4, 4, 4);
            panelAccentLine.Name = "panelAccentLine";
            panelAccentLine.Size = new Size(1286, 6);
            panelAccentLine.TabIndex = 1;
            // 
            // panelFilter
            // 
            panelFilter.Controls.Add(txtSearch);
            panelFilter.Controls.Add(btnRefresh);
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Location = new Point(0, 81);
            panelFilter.Margin = new Padding(4, 4, 4, 4);
            panelFilter.Name = "panelFilter";
            panelFilter.Size = new Size(1286, 64);
            panelFilter.TabIndex = 3;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(15, 14);
            txtSearch.Margin = new Padding(4, 4, 4, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "🔍  Tìm theo tên công ty, liên hệ, email...";
            txtSearch.Size = new Size(436, 29);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(465, 14);
            btnRefresh.Margin = new Padding(4, 4, 4, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(129, 36);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "🔄  Làm mới";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // panelToolbar
            // 
            panelToolbar.Controls.Add(btnAdd);
            panelToolbar.Controls.Add(btnEdit);
            panelToolbar.Controls.Add(btnDelete);
            panelToolbar.Controls.Add(btnDetail);
            panelToolbar.Controls.Add(lblCount);
            panelToolbar.Dock = DockStyle.Top;
            panelToolbar.Location = new Point(0, 145);
            panelToolbar.Margin = new Padding(4, 4, 4, 4);
            panelToolbar.Name = "panelToolbar";
            panelToolbar.Size = new Size(1286, 73);
            panelToolbar.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(15, 13);
            btnAdd.Margin = new Padding(4, 4, 4, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(154, 48);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "➕  Thêm mới";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Enabled = false;
            btnEdit.Location = new Point(177, 13);
            btnEdit.Margin = new Padding(4, 4, 4, 4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(116, 48);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "✏️  Sửa";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Enabled = false;
            btnDelete.Location = new Point(301, 13);
            btnDelete.Margin = new Padding(4, 4, 4, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(103, 48);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "🗑️  Xóa";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnDetail
            // 
            btnDetail.Enabled = false;
            btnDetail.Location = new Point(411, 13);
            btnDetail.Margin = new Padding(4, 4, 4, 4);
            btnDetail.Name = "btnDetail";
            btnDetail.Size = new Size(235, 48);
            btnDetail.TabIndex = 3;
            btnDetail.Text = "📋  Xem dự án";
            btnDetail.Click += btnDetail_Click;
            // 
            // lblCount
            // 
            lblCount.Location = new Point(573, 13);
            lblCount.Margin = new Padding(4, 0, 4, 0);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(231, 48);
            lblCount.TabIndex = 4;
            lblCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvCustomers
            // 
            dgvCustomers.ColumnHeadersHeight = 29;
            dgvCustomers.Columns.AddRange(new DataGridViewColumn[] { colCustId, colCompany, colContact, colCustEmail, colCustPhone, colAddress, colCreatedAt });
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Location = new Point(0, 218);
            dgvCustomers.Margin = new Padding(4, 4, 4, 4);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.RowTemplate.Height = 34;
            dgvCustomers.Size = new Size(1286, 611);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellDoubleClick += dgvCustomers_CellDoubleClick;
            dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
            // 
            // colCustId
            // 
            colCustId.HeaderText = "ID";
            colCustId.MinimumWidth = 6;
            colCustId.Name = "colCustId";
            colCustId.Visible = false;
            colCustId.Width = 45;
            // 
            // colCompany
            // 
            colCompany.HeaderText = "Tên công ty";
            colCompany.MinimumWidth = 6;
            colCompany.Name = "colCompany";
            colCompany.Width = 220;
            // 
            // colContact
            // 
            colContact.HeaderText = "Người liên hệ";
            colContact.MinimumWidth = 6;
            colContact.Name = "colContact";
            colContact.Width = 160;
            // 
            // colCustEmail
            // 
            colCustEmail.HeaderText = "Email";
            colCustEmail.MinimumWidth = 6;
            colCustEmail.Name = "colCustEmail";
            colCustEmail.Width = 190;
            // 
            // colCustPhone
            // 
            colCustPhone.HeaderText = "Điện thoại";
            colCustPhone.MinimumWidth = 6;
            colCustPhone.Name = "colCustPhone";
            colCustPhone.Width = 120;
            // 
            // colAddress
            // 
            colAddress.HeaderText = "Địa chỉ";
            colAddress.MinimumWidth = 6;
            colAddress.Name = "colAddress";
            colAddress.Width = 200;
            // 
            // colCreatedAt
            // 
            colCreatedAt.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCreatedAt.HeaderText = "Ngày tạo";
            colCreatedAt.MinimumWidth = 6;
            colCreatedAt.Name = "colCreatedAt";
            // 
            // panelStatus
            // 
            panelStatus.Controls.Add(lblStatus);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Location = new Point(0, 829);
            panelStatus.Margin = new Padding(4, 4, 4, 4);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(1286, 39);
            panelStatus.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(0, 0);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(15, 0, 0, 0);
            lblStatus.Size = new Size(1286, 39);
            lblStatus.TabIndex = 0;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmCustomers
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1286, 868);
            Controls.Add(dgvCustomers);
            Controls.Add(panelStatus);
            Controls.Add(panelToolbar);
            Controls.Add(panelFilter);
            Controls.Add(panelTop);
            Margin = new Padding(5, 6, 5, 6);
            Name = "frmCustomers";
            StartPosition = FormStartPosition.Manual;
            Text = "🏢  Quản lý Khách hàng";
            panelTop.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            panelStatus.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Field declarations ────────────────────────────────────────────────
        private Panel panelTop, panelAccentLine, panelFilter, panelToolbar, panelStatus;
        private Label lblHeader, lblCount, lblStatus;
        private TextBox txtSearch;
        private Button btnRefresh, btnAdd, btnEdit, btnDelete, btnDetail;
        private DataGridView dgvCustomers;
        private DataGridViewTextBoxColumn colCustId, colCompany, colContact;
        private DataGridViewTextBoxColumn colCustEmail, colCustPhone, colAddress, colCreatedAt;
    }
}