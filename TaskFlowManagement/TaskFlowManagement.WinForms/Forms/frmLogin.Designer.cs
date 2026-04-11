// frmLogin.Designer.cs
//
// Quy tắc Designer (BẮT BUỘC):
//   ✅ Chỉ chứa property assignment, AddRange, += tên_method khai báo riêng
//   ✅ TableLayoutPanel chính: 1 hàng, 2 cột — 40% trái / 60% phải
//   ✅ TableLayoutPanel phụ (tableFeatures): căn thẳng hàng icon và text tính năng
//   ❌ Không lambda, không using var, không gọi UIHelper

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Khai báo tất cả controls ──────────────────────────────────
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();

            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelRing1 = new System.Windows.Forms.Panel();
            this.panelRing2 = new System.Windows.Forms.Panel();
            this.panelLogoBox = new System.Windows.Forms.Panel();
            this.lblLogoIcon = new System.Windows.Forms.Label();
            this.lblAppName = new System.Windows.Forms.Label();
            this.panelBadge = new System.Windows.Forms.Panel();
            this.lblBadgeDot = new System.Windows.Forms.Label();
            this.lblBadgeText = new System.Windows.Forms.Label();
            this.lblTagline = new System.Windows.Forms.Label();
            this.lblTaglineSub = new System.Windows.Forms.Label();
            this.panelAccent = new System.Windows.Forms.Panel();

            // TableLayoutPanel riêng để căn thẳng hàng icon và text tính năng
            this.tableFeatures = new System.Windows.Forms.TableLayoutPanel();
            this.lblFeatIcon1 = new System.Windows.Forms.Label();
            this.lblFeat1 = new System.Windows.Forms.Label();
            this.lblFeatIcon2 = new System.Windows.Forms.Label();
            this.lblFeat2 = new System.Windows.Forms.Label();
            this.lblFeatIcon3 = new System.Windows.Forms.Label();
            this.lblFeat3 = new System.Windows.Forms.Label();

            this.panelRight = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelTitleLine = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblLabelUser = new System.Windows.Forms.Label();
            this.panelInputUser = new System.Windows.Forms.Panel();
            this.lblIconUser = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblLabelPass = new System.Windows.Forms.Label();
            this.panelInputPass = new System.Windows.Forms.Panel();
            this.lblIconPass = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnEye = new System.Windows.Forms.Button();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.lblError = new System.Windows.Forms.Label();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblFooter = new System.Windows.Forms.Label();

            this.tableMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelLogoBox.SuspendLayout();
            this.panelBadge.SuspendLayout();
            this.tableFeatures.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelInputUser.SuspendLayout();
            this.panelInputPass.SuspendLayout();
            this.SuspendLayout();

            // ══════════════════════════════════════════════════════════════
            // TABLE LAYOUT CHÍNH — 1 hàng, 2 cột: 40% trái / 60% phải
            // Form 900×580 → panelLeft ≈ 360px, panelRight ≈ 540px
            // ══════════════════════════════════════════════════════════════
            this.tableMain.ColumnCount = 2;
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 40F));
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 60F));
            this.tableMain.RowCount = 1;
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.Controls.Add(this.panelLeft, 0, 0);
            this.tableMain.Controls.Add(this.panelRight, 1, 0);
            this.tableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableMain.Margin = new System.Windows.Forms.Padding(0);
            this.tableMain.Name = "tableMain";
            this.tableMain.TabIndex = 0;

            // ══════════════════════════════════════════════════════════════
            // PANEL TRÁI (360px) — màu nền áp qua ApplyClientStyles()
            // Lề nội dung: X = 40, chiều rộng nội dung = 280px
            // ══════════════════════════════════════════════════════════════
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.TabIndex = 0;
            this.panelLeft.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.panelRing1,
                this.panelRing2,
                this.panelLogoBox,
                this.lblAppName,
                this.panelBadge,
                this.lblTagline,
                this.lblTaglineSub,
                this.panelAccent,
                this.tableFeatures });

            // panelRing1 — vòng trang trí lớn, neo góc trên-phải
            this.panelRing1.Anchor = System.Windows.Forms.AnchorStyles.Top
                                   | System.Windows.Forms.AnchorStyles.Right;
            this.panelRing1.BackColor = System.Drawing.Color.Transparent;
            this.panelRing1.Location = new System.Drawing.Point(130, -90);
            this.panelRing1.Name = "panelRing1";
            this.panelRing1.Size = new System.Drawing.Size(280, 280);
            this.panelRing1.TabIndex = 0;
            this.panelRing1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRing1_Paint);

            // panelRing2 — vòng trang trí nhỏ lồng bên trong ring1
            this.panelRing2.Anchor = System.Windows.Forms.AnchorStyles.Top
                                   | System.Windows.Forms.AnchorStyles.Right;
            this.panelRing2.BackColor = System.Drawing.Color.Transparent;
            this.panelRing2.Location = new System.Drawing.Point(170, -52);
            this.panelRing2.Name = "panelRing2";
            this.panelRing2.Size = new System.Drawing.Size(180, 180);
            this.panelRing2.TabIndex = 1;
            this.panelRing2.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRing2_Paint);

            // panelLogoBox — hộp gradient chứa icon ứng dụng
            this.panelLogoBox.Location = new System.Drawing.Point(40, 60);
            this.panelLogoBox.Name = "panelLogoBox";
            this.panelLogoBox.Size = new System.Drawing.Size(52, 52);
            this.panelLogoBox.TabIndex = 2;
            this.panelLogoBox.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLogoBox_Paint);
            this.panelLogoBox.Controls.Add(this.lblLogoIcon);

            // lblLogoIcon — emoji icon bên trong logo box
            this.lblLogoIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblLogoIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblLogoIcon.Location = new System.Drawing.Point(0, 0);
            this.lblLogoIcon.Name = "lblLogoIcon";
            this.lblLogoIcon.Size = new System.Drawing.Size(52, 52);
            this.lblLogoIcon.TabIndex = 0;
            this.lblLogoIcon.Text = "📋";
            this.lblLogoIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblAppName — tên ứng dụng, đặt ngang hàng với logo
            this.lblAppName.AutoSize = false;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(104, 68);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(220, 38);
            this.lblAppName.TabIndex = 3;
            this.lblAppName.Text = "TaskFlow";

            // panelBadge — pill phiên bản, Paint vẽ viền bo góc mờ
            this.panelBadge.BackColor = System.Drawing.Color.Transparent;
            this.panelBadge.Location = new System.Drawing.Point(40, 128);
            this.panelBadge.Name = "panelBadge";
            this.panelBadge.Size = new System.Drawing.Size(210, 28);
            this.panelBadge.TabIndex = 4;
            this.panelBadge.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBadge_Paint);
            this.panelBadge.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBadgeDot, this.lblBadgeText });

            // lblBadgeDot — chấm màu teal
            this.lblBadgeDot.BackColor = System.Drawing.Color.Transparent;
            this.lblBadgeDot.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblBadgeDot.Location = new System.Drawing.Point(8, -2);
            this.lblBadgeDot.Name = "lblBadgeDot";
            this.lblBadgeDot.Size = new System.Drawing.Size(18, 28);
            this.lblBadgeDot.TabIndex = 0;
            this.lblBadgeDot.Text = "•";
            this.lblBadgeDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblBadgeText — nhãn phiên bản
            this.lblBadgeText.BackColor = System.Drawing.Color.Transparent;
            this.lblBadgeText.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblBadgeText.Location = new System.Drawing.Point(28, 0);
            this.lblBadgeText.Name = "lblBadgeText";
            this.lblBadgeText.Size = new System.Drawing.Size(178, 28);
            this.lblBadgeText.TabIndex = 1;
            this.lblBadgeText.Text = "v1.0 – Bản Chính thức";
            this.lblBadgeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblTagline — dòng tiêu đề lớn 1 (Y = 128 + 28 + 20 = 176)
            this.lblTagline.AutoSize = false;
            this.lblTagline.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTagline.ForeColor = System.Drawing.Color.White;
            this.lblTagline.Location = new System.Drawing.Point(40, 176);
            this.lblTagline.Name = "lblTagline";
            this.lblTagline.Size = new System.Drawing.Size(290, 48);
            this.lblTagline.TabIndex = 5;
            this.lblTagline.Text = "Quản lý Dự án";

            // lblTaglineSub — dòng tiêu đề lớn 2 (Y = 176 + 48 = 224)
            this.lblTaglineSub.AutoSize = false;
            this.lblTaglineSub.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTaglineSub.ForeColor = System.Drawing.Color.White;
            this.lblTaglineSub.Location = new System.Drawing.Point(40, 224);
            this.lblTaglineSub.Name = "lblTaglineSub";
            this.lblTaglineSub.Size = new System.Drawing.Size(290, 48);
            this.lblTaglineSub.TabIndex = 6;
            this.lblTaglineSub.Text = "Phần mềm";

            // panelAccent — gạch ngang trang trí (Y = 224 + 48 = 272)
            this.panelAccent.Location = new System.Drawing.Point(40, 280);
            this.panelAccent.Name = "panelAccent";
            this.panelAccent.Size = new System.Drawing.Size(48, 3);
            this.panelAccent.TabIndex = 7;

            // ── tableFeatures: 3 hàng × 2 cột ────────────────────────────
            // Cột 0 (32px tuyệt đối): chứa icon emoji căn giữa ô
            // Cột 1 (fill): chứa text tính năng, căn trái-giữa dọc
            // Mỗi hàng cao 42px — đủ để emoji không bị xén viền
            // ──────────────────────────────────────────────────────────────
            this.tableFeatures.ColumnCount = 2;
            this.tableFeatures.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableFeatures.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 100F));
            this.tableFeatures.RowCount = 3;
            this.tableFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableFeatures.Controls.Add(this.lblFeatIcon1, 0, 0);
            this.tableFeatures.Controls.Add(this.lblFeat1, 1, 0);
            this.tableFeatures.Controls.Add(this.lblFeatIcon2, 0, 1);
            this.tableFeatures.Controls.Add(this.lblFeat2, 1, 1);
            this.tableFeatures.Controls.Add(this.lblFeatIcon3, 0, 2);
            this.tableFeatures.Controls.Add(this.lblFeat3, 1, 2);
            this.tableFeatures.BackColor = System.Drawing.Color.Transparent;
            this.tableFeatures.Location = new System.Drawing.Point(40, 296);
            this.tableFeatures.Margin = new System.Windows.Forms.Padding(0);
            this.tableFeatures.Name = "tableFeatures";
            this.tableFeatures.Size = new System.Drawing.Size(290, 126);
            this.tableFeatures.TabIndex = 8;

            // Hàng 1 — Quản lý dự án & khách hàng
            this.lblFeatIcon1.BackColor = System.Drawing.Color.Transparent;
            this.lblFeatIcon1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFeatIcon1.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblFeatIcon1.Name = "lblFeatIcon1";
            this.lblFeatIcon1.TabIndex = 0;
            this.lblFeatIcon1.Text = "📁";
            this.lblFeatIcon1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblFeat1.BackColor = System.Drawing.Color.Transparent;
            this.lblFeat1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFeat1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFeat1.Name = "lblFeat1";
            this.lblFeat1.TabIndex = 1;
            this.lblFeat1.Text = "Quản lý dự án & khách hàng";
            this.lblFeat1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // Hàng 2 — Giao việc & theo dõi tiến độ
            this.lblFeatIcon2.BackColor = System.Drawing.Color.Transparent;
            this.lblFeatIcon2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFeatIcon2.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblFeatIcon2.Name = "lblFeatIcon2";
            this.lblFeatIcon2.TabIndex = 2;
            this.lblFeatIcon2.Text = "✅";
            this.lblFeatIcon2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblFeat2.BackColor = System.Drawing.Color.Transparent;
            this.lblFeat2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFeat2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFeat2.Name = "lblFeat2";
            this.lblFeat2.TabIndex = 3;
            this.lblFeat2.Text = "Giao việc & theo dõi tiến độ";
            this.lblFeat2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // Hàng 3 — Phân quyền linh hoạt
            this.lblFeatIcon3.BackColor = System.Drawing.Color.Transparent;
            this.lblFeatIcon3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFeatIcon3.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblFeatIcon3.Name = "lblFeatIcon3";
            this.lblFeatIcon3.TabIndex = 4;
            this.lblFeatIcon3.Text = "🔒";
            this.lblFeatIcon3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblFeat3.BackColor = System.Drawing.Color.Transparent;
            this.lblFeat3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFeat3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFeat3.Name = "lblFeat3";
            this.lblFeat3.TabIndex = 5;
            this.lblFeat3.Text = "Phân quyền linh hoạt 3 vai trò";
            this.lblFeat3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ══════════════════════════════════════════════════════════════
            // PANEL PHẢI (540px) — vùng form đăng nhập
            // Lề trái: X = 60, chiều rộng nội dung = 420px
            // ══════════════════════════════════════════════════════════════
            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Margin = new System.Windows.Forms.Padding(0);
            this.panelRight.Name = "panelRight";
            this.panelRight.TabIndex = 1;
            this.panelRight.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle,
                this.panelTitleLine,
                this.lblSubtitle,
                this.lblLabelUser,
                this.panelInputUser,
                this.lblLabelPass,
                this.panelInputPass,
                this.chkRemember,
                this.lblError,
                this.panelProgress,
                this.btnLogin,
                this.lblFooter });

            // lblTitle — tiêu đề form, font 24pt Bold
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(60, 80);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(420, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Đăng nhập";

            // panelTitleLine — gạch ngang trang trí 3px dưới tiêu đề (Y = 80 + 48 = 128)
            this.panelTitleLine.Location = new System.Drawing.Point(60, 132);
            this.panelTitleLine.Name = "panelTitleLine";
            this.panelTitleLine.Size = new System.Drawing.Size(48, 3);
            this.panelTitleLine.TabIndex = 1;

            // lblSubtitle — mô tả phụ (Y = 132 + 3 + 8 = 143)
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.Location = new System.Drawing.Point(60, 143);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(420, 24);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Nhập thông tin tài khoản để tiếp tục";

            // lblLabelUser — nhãn ô tài khoản (Y = 143 + 24 + 20 = 187)
            this.lblLabelUser.AutoSize = true;
            this.lblLabelUser.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLabelUser.Location = new System.Drawing.Point(60, 187);
            this.lblLabelUser.Name = "lblLabelUser";
            this.lblLabelUser.TabIndex = 3;
            this.lblLabelUser.Text = "TÊN ĐĂNG NHẬP";

            // ── panelInputUser ─────────────────────────────────────────────
            // Caret fix: TextBox dùng BorderStyle.None, panel vẽ viền qua Paint.
            // Location Y = 187 + 20 + 6 = 213; Height = 50px cân đối (15px trên/dưới).
            // ──────────────────────────────────────────────────────────────
            this.panelInputUser.Location = new System.Drawing.Point(60, 213);
            this.panelInputUser.Name = "panelInputUser";
            this.panelInputUser.Size = new System.Drawing.Size(420, 50);
            this.panelInputUser.TabIndex = 4;
            this.panelInputUser.Paint += new System.Windows.Forms.PaintEventHandler(this.panelInput_Paint);
            this.panelInputUser.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblIconUser, this.txtUsername });

            // lblIconUser — icon người dùng, căn dọc giữa trong panel 50px
            this.lblIconUser.BackColor = System.Drawing.Color.Transparent;
            this.lblIconUser.Font = new System.Drawing.Font("Segoe UI Emoji", 14F);
            this.lblIconUser.Location = new System.Drawing.Point(10, 10);
            this.lblIconUser.Name = "lblIconUser";
            this.lblIconUser.Size = new System.Drawing.Size(30, 30);
            this.lblIconUser.TabIndex = 0;
            this.lblIconUser.Text = "👤";
            this.lblIconUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // txtUsername — BorderStyle.None để caret hiển thị chuẩn, không bị dính mép
            // X = 48 (sau icon 30px + gap 8px + 10px padding trái); Y = 15 → tâm dọc của panel 50px
            // Width = 420 - 48 (start) - 10 (padding phải) = 362px
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUsername.Location = new System.Drawing.Point(48, 15);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PlaceholderText = "Nhập tên đăng nhập...";
            this.txtUsername.Size = new System.Drawing.Size(362, 22);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Enter += new System.EventHandler(this.txtUsername_EnterLeave);
            this.txtUsername.Leave += new System.EventHandler(this.txtUsername_EnterLeave);

            // lblLabelPass — nhãn ô mật khẩu (Y = 213 + 50 + 16 = 279)
            this.lblLabelPass.AutoSize = true;
            this.lblLabelPass.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLabelPass.Location = new System.Drawing.Point(60, 279);
            this.lblLabelPass.Name = "lblLabelPass";
            this.lblLabelPass.TabIndex = 5;
            this.lblLabelPass.Text = "MẬT KHẨU";

            // ── panelInputPass ─────────────────────────────────────────────
            // Y = 279 + 20 + 6 = 305; layout tương tự panelInputUser.
            // Width của txtPassword nhường 40px bên phải cho btnEye.
            // ──────────────────────────────────────────────────────────────
            this.panelInputPass.Location = new System.Drawing.Point(60, 305);
            this.panelInputPass.Name = "panelInputPass";
            this.panelInputPass.Size = new System.Drawing.Size(420, 50);
            this.panelInputPass.TabIndex = 6;
            this.panelInputPass.Paint += new System.Windows.Forms.PaintEventHandler(this.panelInput_Paint);
            this.panelInputPass.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblIconPass, this.txtPassword, this.btnEye });

            // lblIconPass — icon khóa, căn dọc giữa
            this.lblIconPass.BackColor = System.Drawing.Color.Transparent;
            this.lblIconPass.Font = new System.Drawing.Font("Segoe UI Emoji", 14F);
            this.lblIconPass.Location = new System.Drawing.Point(10, 10);
            this.lblIconPass.Name = "lblIconPass";
            this.lblIconPass.Size = new System.Drawing.Size(30, 30);
            this.lblIconPass.TabIndex = 0;
            this.lblIconPass.Text = "🔑";
            this.lblIconPass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // txtPassword — BorderStyle.None, caret fix giống txtUsername
            // Width = 420 - 48 (start) - 6 (gap) - 32 (btnEye) - 10 (padding) = 324px
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.Location = new System.Drawing.Point(48, 15);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PlaceholderText = "Nhập mật khẩu...";
            this.txtPassword.Size = new System.Drawing.Size(324, 22);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_EnterLeave);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_EnterLeave);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);

            // btnEye — toggle hiện/ẩn mật khẩu, sát cạnh phải (X = 420 - 10 - 32 = 378)
            this.btnEye.BackColor = System.Drawing.Color.Transparent;
            this.btnEye.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEye.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEye.FlatAppearance.BorderSize = 0;
            this.btnEye.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEye.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEye.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.btnEye.Location = new System.Drawing.Point(378, 10);
            this.btnEye.Name = "btnEye";
            this.btnEye.Size = new System.Drawing.Size(32, 30);
            this.btnEye.TabIndex = 2;
            this.btnEye.TabStop = false;
            this.btnEye.Text = "👁";
            this.btnEye.UseVisualStyleBackColor = false;

            // chkRemember — ghi nhớ tài khoản (Y = 305 + 50 + 16 = 371)
            this.chkRemember.AutoSize = true;
            this.chkRemember.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkRemember.Location = new System.Drawing.Point(60, 371);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.TabIndex = 7;
            this.chkRemember.Text = "Nhớ tên đăng nhập";

            // lblError — thông báo lỗi xác thực, ẩn khi không có lỗi (Y = 371 + 24 + 8 = 403)
            this.lblError.AutoSize = false;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.Location = new System.Drawing.Point(60, 403);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(420, 22);
            this.lblError.TabIndex = 8;
            this.lblError.Text = "";

            // panelProgress — thanh tiến trình loading, ẩn mặc định, Width = 0 (Y = 403 + 22 + 6 = 431)
            this.panelProgress.Location = new System.Drawing.Point(60, 431);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(0, 3);
            this.panelProgress.TabIndex = 9;
            this.panelProgress.Visible = false;
            this.panelProgress.Paint += new System.Windows.Forms.PaintEventHandler(this.panelProgress_Paint);

            // btnLogin — nút xác nhận; Paint vẽ gradient bo góc tùy chỉnh (Y = 431 + 3 + 12 = 446)
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(60, 446);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(420, 52);
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Text = "ĐĂNG NHẬP  →";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.Paint += new System.Windows.Forms.PaintEventHandler(this.btnLogin_Paint);

            // lblFooter — dòng thông tin hệ thống, Dock=Bottom căn giữa toàn chiều ngang
            this.lblFooter.AutoSize = false;
            this.lblFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(540, 28);
            this.lblFooter.TabIndex = 11;
            this.lblFooter.Text = "Hệ thống Quản lý Dự án Phần mềm  ·  v1.0 – Bản Chính thức";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ══════════════════════════════════════════════════════════════
            // FORM
            // ══════════════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 580);
            this.Controls.Add(this.tableMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaskFlow – Quản lý Dự án Phần mềm";

            this.panelInputUser.ResumeLayout(false);
            this.panelInputPass.ResumeLayout(false);
            this.panelLogoBox.ResumeLayout(false);
            this.panelBadge.ResumeLayout(false);
            this.tableFeatures.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.tableMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Khai báo biến thành viên ──────────────────────────────────────
        private System.Windows.Forms.TableLayoutPanel tableMain;

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRing1;
        private System.Windows.Forms.Panel panelRing2;
        private System.Windows.Forms.Panel panelLogoBox;
        private System.Windows.Forms.Label lblLogoIcon;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Panel panelBadge;
        private System.Windows.Forms.Label lblBadgeDot;
        private System.Windows.Forms.Label lblBadgeText;
        private System.Windows.Forms.Label lblTagline;
        private System.Windows.Forms.Label lblTaglineSub;
        private System.Windows.Forms.Panel panelAccent;
        private System.Windows.Forms.TableLayoutPanel tableFeatures;
        private System.Windows.Forms.Label lblFeatIcon1;
        private System.Windows.Forms.Label lblFeat1;
        private System.Windows.Forms.Label lblFeatIcon2;
        private System.Windows.Forms.Label lblFeat2;
        private System.Windows.Forms.Label lblFeatIcon3;
        private System.Windows.Forms.Label lblFeat3;

        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelTitleLine;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblLabelUser;
        private System.Windows.Forms.Panel panelInputUser;
        private System.Windows.Forms.Label lblIconUser;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblLabelPass;
        private System.Windows.Forms.Panel panelInputPass;
        private System.Windows.Forms.Label lblIconPass;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnEye;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblFooter;
    }
}