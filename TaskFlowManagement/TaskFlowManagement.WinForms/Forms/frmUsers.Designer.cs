namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmUsers
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.tableToolbar = new System.Windows.Forms.TableLayoutPanel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboFilterRole = new System.Windows.Forms.ComboBox();
            this.cboFilterStatus = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelRight = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.btnActivate = new System.Windows.Forms.Button();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();

            this.panelTop.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            this.tableToolbar.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvUsers).BeginInit();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();

            // ── panelTop ──────────────────────────────────────────────────────
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 52;
            this.panelTop.Name = "panelTop";
            this.panelTop.Controls.Add(this.lblHeader);

            this.lblHeader.AutoSize = false;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Text = "  👥  Quản lý Tài khoản";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── panelToolbar ──────────────────────────────────────────────────
            // Chứa TableLayoutPanel chia 2 cột: left (search+filter) | right (actions)
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Height = 56;
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panelToolbar.Controls.Add(this.tableToolbar);

            // ── tableToolbar ──────────────────────────────────────────────────
            this.tableToolbar.ColumnCount = 2;
            this.tableToolbar.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableToolbar.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableToolbar.RowCount = 1;
            this.tableToolbar.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableToolbar.Name = "tableToolbar";
            this.tableToolbar.Controls.Add(this.panelLeft, 0, 0);
            this.tableToolbar.Controls.Add(this.panelRight, 1, 0);

            // ── panelLeft: Ô tìm kiếm + 2 ComboBox filter + Refresh ──────────
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.txtSearch, this.cboFilterRole, this.cboFilterStatus, this.btnRefresh
            });

            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(0, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "🔍  Tìm theo tên, username, email...";
            this.txtSearch.Size = new System.Drawing.Size(230, 30);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.cboFilterRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterRole.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboFilterRole.Location = new System.Drawing.Point(238, 13);
            this.cboFilterRole.Name = "cboFilterRole";
            this.cboFilterRole.Size = new System.Drawing.Size(130, 30);
            this.cboFilterRole.Items.AddRange(new object[] { "Tất cả Role", "Admin", "Manager", "Developer" });
            this.cboFilterRole.SelectedIndex = 0;
            this.cboFilterRole.SelectedIndexChanged += new System.EventHandler(this.cboFilterRole_SelectedIndexChanged);

            this.cboFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboFilterStatus.Location = new System.Drawing.Point(376, 13);
            this.cboFilterStatus.Name = "cboFilterStatus";
            this.cboFilterStatus.Size = new System.Drawing.Size(115, 30);
            this.cboFilterStatus.Items.AddRange(new object[] { "Tất cả", "Active", "Inactive" });
            this.cboFilterStatus.SelectedIndex = 0;
            this.cboFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cboFilterStatus_SelectedIndexChanged);

            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Emoji", 11F);
            this.btnRefresh.Location = new System.Drawing.Point(499, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(36, 30);
            this.btnRefresh.Text = "🔄";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // ── panelRight: Separator + Các nút hành động + lblCount ──────────
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Name = "panelRight";
            this.panelRight.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblSeparator, this.btnAdd, this.btnEdit,
                this.btnDeactivate, this.btnActivate, this.lblCount
            });

            // Đường phân cách dọc giữa 2 nhóm
            this.lblSeparator.AutoSize = false;
            this.lblSeparator.Location = new System.Drawing.Point(0, 10);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(1, 34);
            this.lblSeparator.Text = "";
            this.lblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Location = new System.Drawing.Point(10, 11);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 32);
            this.btnAdd.Text = "➕  Thêm mới";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Enabled = false;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Location = new System.Drawing.Point(128, 11);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 32);
            this.btnEdit.Text = "✏️  Sửa thông tin";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDeactivate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeactivate.Enabled = false;
            this.btnDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeactivate.FlatAppearance.BorderSize = 0;
            this.btnDeactivate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeactivate.Location = new System.Drawing.Point(256, 11);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(120, 32);
            this.btnDeactivate.Text = "🔴  Vô hiệu hóa";
            this.btnDeactivate.Click += new System.EventHandler(this.btnDeactivate_Click);

            this.btnActivate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActivate.Enabled = false;
            this.btnActivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivate.FlatAppearance.BorderSize = 0;
            this.btnActivate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActivate.Location = new System.Drawing.Point(384, 11);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(120, 32);
            this.btnActivate.Text = "✅  Kích hoạt lại";
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);

            this.lblCount.AutoSize = false;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.Location = new System.Drawing.Point(512, 11);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(130, 32);
            this.lblCount.Text = "";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ── DataGridView ──────────────────────────────────────────────────
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUsers.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.ColumnHeadersDefaultCellStyle.Alignment =
                System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvUsers.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvUsers.ColumnHeadersDefaultCellStyle.Padding =
                new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.dgvUsers.DefaultCellStyle.Padding =
                new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.EnableHeadersVisualStyles = false;
            this.dgvUsers.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.RowTemplate.Height = 36;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.SelectionChanged += new System.EventHandler(this.dgvUsers_SelectionChanged);
            this.dgvUsers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellDoubleClick);

            // Cấu hình các cột
            this.colId.Name = "colId";
            this.colId.HeaderText = "ID";
            this.colId.Width = 45;
            this.colId.Visible = false;

            this.colUsername.Name = "colUsername";
            this.colUsername.HeaderText = "Username";
            this.colUsername.Width = 130;

            this.colFullName.Name = "colFullName";
            this.colFullName.HeaderText = "Họ và tên";
            this.colFullName.Width = 190;

            this.colEmail.Name = "colEmail";
            this.colEmail.HeaderText = "Email";
            this.colEmail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            this.colPhone.Name = "colPhone";
            this.colPhone.HeaderText = "Điện thoại";
            this.colPhone.Width = 120;

            this.colRole.Name = "colRole";
            this.colRole.HeaderText = "Vai trò";
            this.colRole.Width = 110;

            this.colStatus.Name = "colStatus";
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.Width = 110;

            this.colLastLogin.Name = "colLastLogin";
            this.colLastLogin.HeaderText = "Đăng nhập cuối";
            this.colLastLogin.Width = 150;

            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                this.colId, this.colUsername, this.colFullName, this.colEmail,
                this.colPhone, this.colRole, this.colStatus, this.colLastLogin
            });

            // ── panelStatus ───────────────────────────────────────────────────
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatus.Height = 28;
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Controls.Add(this.lblStatus);

            this.lblStatus.AutoSize = false;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblStatus.Text = "";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── frmUsers ──────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 640);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelStatus);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "frmUsers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Quản lý Tài khoản";

            this.panelTop.ResumeLayout(false);
            this.panelToolbar.ResumeLayout(false);
            this.tableToolbar.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgvUsers).EndInit();
            this.panelStatus.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Khai báo field ────────────────────────────────────────────────────
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.TableLayoutPanel tableToolbar;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboFilterRole;
        private System.Windows.Forms.ComboBox cboFilterStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRole;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastLogin;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblStatus;
    }
}