namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmProjectMembers
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
            lblTitle = new Label();
            panelAccent = new Panel();
            dgvMembers = new DataGridView();
            colMemberId = new DataGridViewTextBoxColumn();
            colMemberName = new DataGridViewTextBoxColumn();
            colMemberEmail = new DataGridViewTextBoxColumn();
            colMemberRole = new DataGridViewTextBoxColumn();
            colJoinedAt = new DataGridViewTextBoxColumn();
            panelAdd = new Panel();
            lblAddTitle = new Label();
            tableAdd = new TableLayoutPanel();
            cboUser = new ComboBox();
            cboProjectRole = new ComboBox();
            btnAddMember = new Button();
            panelBottom = new Panel();
            flowBottomBtns = new FlowLayoutPanel();
            btnClose = new Button();
            btnRemove = new Button();
            lblCount = new Label();
            panelBottomLine = new Panel();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            panelAdd.SuspendLayout();
            tableAdd.SuspendLayout();
            panelBottom.SuspendLayout();
            flowBottomBtns.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(panelAccent);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(663, 77);
            panelHeader.TabIndex = 3;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(21, 0, 0, 0);
            lblTitle.Size = new Size(663, 72);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "👥  Thành viên dự án";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelAccent
            // 
            panelAccent.Dock = DockStyle.Bottom;
            panelAccent.Location = new Point(0, 72);
            panelAccent.Margin = new Padding(3, 4, 3, 4);
            panelAccent.Name = "panelAccent";
            panelAccent.Size = new Size(663, 5);
            panelAccent.TabIndex = 1;
            // 
            // dgvMembers
            // 
            dgvMembers.BorderStyle = BorderStyle.None;
            dgvMembers.ColumnHeadersHeight = 29;
            dgvMembers.Columns.AddRange(new DataGridViewColumn[] { colMemberId, colMemberName, colMemberEmail, colMemberRole, colJoinedAt });
            dgvMembers.Dock = DockStyle.Fill;
            dgvMembers.Location = new Point(0, 77);
            dgvMembers.Margin = new Padding(3, 4, 3, 4);
            dgvMembers.Name = "dgvMembers";
            dgvMembers.RowHeadersWidth = 51;
            dgvMembers.RowTemplate.Height = 36;
            dgvMembers.Size = new Size(663, 432);
            dgvMembers.TabIndex = 0;
            // 
            // colMemberId
            // 
            colMemberId.MinimumWidth = 6;
            colMemberId.Name = "colMemberId";
            colMemberId.Visible = false;
            colMemberId.Width = 125;
            // 
            // colMemberName
            // 
            colMemberName.HeaderText = "Họ tên";
            colMemberName.MinimumWidth = 6;
            colMemberName.Name = "colMemberName";
            colMemberName.Width = 180;
            // 
            // colMemberEmail
            // 
            colMemberEmail.HeaderText = "Email";
            colMemberEmail.MinimumWidth = 6;
            colMemberEmail.Name = "colMemberEmail";
            colMemberEmail.Width = 210;
            // 
            // colMemberRole
            // 
            colMemberRole.HeaderText = "Vai trò DA";
            colMemberRole.MinimumWidth = 6;
            colMemberRole.Name = "colMemberRole";
            colMemberRole.Width = 110;
            // 
            // colJoinedAt
            // 
            colJoinedAt.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colJoinedAt.HeaderText = "Ngày tham gia";
            colJoinedAt.MinimumWidth = 6;
            colJoinedAt.Name = "colJoinedAt";
            // 
            // panelAdd
            // 
            panelAdd.Controls.Add(lblAddTitle);
            panelAdd.Controls.Add(tableAdd);
            panelAdd.Dock = DockStyle.Bottom;
            panelAdd.Location = new Point(0, 509);
            panelAdd.Margin = new Padding(3, 4, 3, 4);
            panelAdd.Name = "panelAdd";
            panelAdd.Padding = new Padding(18, 11, 18, 11);
            panelAdd.Size = new Size(663, 109);
            panelAdd.TabIndex = 1;
            // 
            // lblAddTitle
            // 
            lblAddTitle.AutoSize = true;
            lblAddTitle.Dock = DockStyle.Top;
            lblAddTitle.Location = new Point(18, 11);
            lblAddTitle.Name = "lblAddTitle";
            lblAddTitle.Size = new Size(145, 21);
            lblAddTitle.TabIndex = 0;
            lblAddTitle.Text = "THÊM THÀNH VIÊN";
            // 
            // tableAdd
            // 
            tableAdd.ColumnCount = 3;
            tableAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
            tableAdd.Controls.Add(cboUser, 0, 0);
            tableAdd.Controls.Add(cboProjectRole, 1, 0);
            tableAdd.Controls.Add(btnAddMember, 2, 0);
            tableAdd.Dock = DockStyle.Fill;
            tableAdd.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableAdd.Location = new Point(18, 11);
            tableAdd.Margin = new Padding(3, 4, 3, 4);
            tableAdd.Name = "tableAdd";
            tableAdd.RowCount = 1;
            tableAdd.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableAdd.Size = new Size(627, 87);
            tableAdd.TabIndex = 1;
            // 
            // cboUser
            // 
            cboUser.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboUser.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUser.Location = new Point(0, 29);
            cboUser.Margin = new Padding(0, 0, 9, 0);
            cboUser.Name = "cboUser";
            cboUser.Size = new Size(345, 29);
            cboUser.TabIndex = 0;
            // 
            // cboProjectRole
            // 
            cboProjectRole.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboProjectRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProjectRole.Location = new Point(354, 29);
            cboProjectRole.Margin = new Padding(0, 0, 9, 0);
            cboProjectRole.Name = "cboProjectRole";
            cboProjectRole.Size = new Size(138, 29);
            cboProjectRole.TabIndex = 1;
            // 
            // btnAddMember
            // 
            btnAddMember.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnAddMember.Location = new Point(501, 28);
            btnAddMember.Margin = new Padding(0);
            btnAddMember.Name = "btnAddMember";
            btnAddMember.Size = new Size(126, 31);
            btnAddMember.TabIndex = 2;
            btnAddMember.Text = "➕  Thêm";
            btnAddMember.Click += btnAddMember_Click;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(flowBottomBtns);
            panelBottom.Controls.Add(lblCount);
            panelBottom.Controls.Add(panelBottomLine);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 618);
            panelBottom.Margin = new Padding(3, 4, 3, 4);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(18, 0, 18, 0);
            panelBottom.Size = new Size(663, 75);
            panelBottom.TabIndex = 2;
            // 
            // flowBottomBtns
            // 
            flowBottomBtns.Controls.Add(btnClose);
            flowBottomBtns.Controls.Add(btnRemove);
            flowBottomBtns.Dock = DockStyle.Right;
            flowBottomBtns.FlowDirection = FlowDirection.RightToLeft;
            flowBottomBtns.Location = new Point(318, 1);
            flowBottomBtns.Margin = new Padding(3, 4, 3, 4);
            flowBottomBtns.Name = "flowBottomBtns";
            flowBottomBtns.Padding = new Padding(0, 13, 0, 13);
            flowBottomBtns.Size = new Size(327, 74);
            flowBottomBtns.TabIndex = 0;
            flowBottomBtns.WrapContents = false;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(201, 13);
            btnClose.Margin = new Padding(0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(126, 47);
            btnClose.TabIndex = 0;
            btnClose.Text = "Đóng";
            btnClose.Click += btnClose_Click;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(9, 13);
            btnRemove.Margin = new Padding(0, 0, 9, 0);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(183, 47);
            btnRemove.TabIndex = 1;
            btnRemove.Text = "🗑️  Xóa thành viên";
            btnRemove.Click += btnRemove_Click;
            // 
            // lblCount
            // 
            lblCount.Dock = DockStyle.Left;
            lblCount.Location = new Point(18, 1);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(229, 74);
            lblCount.TabIndex = 1;
            lblCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBottomLine
            // 
            panelBottomLine.Dock = DockStyle.Top;
            panelBottomLine.Location = new Point(18, 0);
            panelBottomLine.Margin = new Padding(3, 4, 3, 4);
            panelBottomLine.Name = "panelBottomLine";
            panelBottomLine.Size = new Size(627, 1);
            panelBottomLine.TabIndex = 2;
            // 
            // frmProjectMembers
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(663, 693);
            Controls.Add(dgvMembers);
            Controls.Add(panelAdd);
            Controls.Add(panelBottom);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 5, 3, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmProjectMembers";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thành viên dự án";
            panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            panelAdd.ResumeLayout(false);
            panelAdd.PerformLayout();
            tableAdd.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            flowBottomBtns.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Fields ───────────────────────────────────────────────
        private Panel panelHeader;
        private Panel panelAccent;
        private Label lblTitle;
        private DataGridView dgvMembers;
        private DataGridViewTextBoxColumn colMemberId;
        private DataGridViewTextBoxColumn colMemberName;
        private DataGridViewTextBoxColumn colMemberEmail;
        private DataGridViewTextBoxColumn colMemberRole;
        private DataGridViewTextBoxColumn colJoinedAt;
        private Panel panelAdd;
        private Label lblAddTitle;
        private TableLayoutPanel tableAdd;
        private ComboBox cboUser;
        private ComboBox cboProjectRole;
        private Button btnAddMember;
        private Panel panelBottom;
        private Panel panelBottomLine;
        private Label lblCount;
        private FlowLayoutPanel flowBottomBtns;
        private Button btnRemove;
        private Button btnClose;
    }
}