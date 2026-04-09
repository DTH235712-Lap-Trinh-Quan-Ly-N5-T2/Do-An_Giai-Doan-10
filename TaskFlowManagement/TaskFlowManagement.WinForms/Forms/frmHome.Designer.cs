using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmHome   // BaseForm khai báo trong frmHome.cs
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Khởi tạo controls ────────────────────────────────────────────────────
            panelHeader = new Panel();
            panelAccentLine = new Panel();
            lblHeader = new Label();

            panelBody = new Panel();

            panelWelcome = new Panel();
            tblWelcome = new TableLayoutPanel();
            lblGreeting = new Label();
            lblRole = new Label();
            lblLastLogin = new Label();

            panelStats = new Panel();
            flowCards = new FlowLayoutPanel();
            lblNote = new Label();

            // Card 1 — Dự án đang chạy
            panelCard1 = new Panel();
            panelCard1Top = new Panel();
            tblCard1 = new TableLayoutPanel();
            lblCard1Icon = new Label();
            lblCard1Title = new Label();
            lblStatProjects = new Label();
            lblCard1Sub = new Label();

            // Card 2 — Công việc của tôi
            panelCard2 = new Panel();
            panelCard2Top = new Panel();
            tblCard2 = new TableLayoutPanel();
            lblCard2Icon = new Label();
            lblCard2Title = new Label();
            lblStatTasks = new Label();
            lblCard2Sub = new Label();

            // Card 3 — Quá hạn
            panelCard3 = new Panel();
            panelCard3Top = new Panel();
            tblCard3 = new TableLayoutPanel();
            lblCard3Icon = new Label();
            lblCard3Title = new Label();
            lblStatOverdue = new Label();
            lblCard3Sub = new Label();

            // Card 4 — Hoàn thành tháng này
            panelCard4 = new Panel();
            panelCard4Top = new Panel();
            tblCard4 = new TableLayoutPanel();
            lblCard4Icon = new Label();
            lblCard4Title = new Label();
            lblStatDone = new Label();
            lblCard4Sub = new Label();

            panelHeader.SuspendLayout();
            panelBody.SuspendLayout();
            panelWelcome.SuspendLayout();
            tblWelcome.SuspendLayout();
            panelStats.SuspendLayout();
            flowCards.SuspendLayout();
            panelCard1.SuspendLayout();
            tblCard1.SuspendLayout();
            panelCard2.SuspendLayout();
            tblCard2.SuspendLayout();
            panelCard3.SuspendLayout();
            tblCard3.SuspendLayout();
            panelCard4.SuspendLayout();
            tblCard4.SuspendLayout();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════════════════════════
            // panelHeader
            // ════════════════════════════════════════════════════════════════════════
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 58;
            panelHeader.Name = "panelHeader";
            panelHeader.Controls.Add(lblHeader);
            panelHeader.Controls.Add(panelAccentLine);

            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Height = 4;
            panelAccentLine.Name = "panelAccentLine";

            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new Padding(18, 0, 0, 4);
            lblHeader.Text = "🏠  Trang chủ";
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;

            // ════════════════════════════════════════════════════════════════════════
            // panelBody
            // ════════════════════════════════════════════════════════════════════════
            panelBody.Dock = DockStyle.Fill;
            panelBody.Name = "panelBody";
            panelBody.Controls.Add(panelStats);
            panelBody.Controls.Add(panelWelcome);

            // ════════════════════════════════════════════════════════════════════════
            // panelWelcome  →  chứa TableLayoutPanel 3 hàng
            // ════════════════════════════════════════════════════════════════════════
            panelWelcome.Dock = DockStyle.Top;
            panelWelcome.Height = 96;
            panelWelcome.Name = "panelWelcome";
            panelWelcome.Padding = new Padding(24, 8, 24, 8);
            panelWelcome.Controls.Add(tblWelcome);

            tblWelcome.Dock = DockStyle.Fill;
            tblWelcome.Name = "tblWelcome";
            tblWelcome.ColumnCount = 1;
            tblWelcome.RowCount = 3;
            tblWelcome.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblWelcome.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));   // lblGreeting
            tblWelcome.RowStyles.Add(new RowStyle(SizeType.Percent, 28F));   // lblRole
            tblWelcome.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));   // lblLastLogin
            tblWelcome.Controls.Add(lblGreeting, 0, 0);
            tblWelcome.Controls.Add(lblRole, 0, 1);
            tblWelcome.Controls.Add(lblLastLogin, 0, 2);

            lblGreeting.AutoSize = false;
            lblGreeting.Dock = DockStyle.Fill;
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Text = "Chào buổi sáng, ...! 👋";
            lblGreeting.TextAlign = ContentAlignment.MiddleLeft;

            lblRole.AutoSize = false;
            lblRole.Dock = DockStyle.Fill;
            lblRole.Name = "lblRole";
            lblRole.Text = "Vai trò: ...";
            lblRole.TextAlign = ContentAlignment.MiddleLeft;

            lblLastLogin.AutoSize = false;
            lblLastLogin.Dock = DockStyle.Fill;
            lblLastLogin.Name = "lblLastLogin";
            lblLastLogin.Text = "";
            lblLastLogin.TextAlign = ContentAlignment.MiddleLeft;

            // ════════════════════════════════════════════════════════════════════════
            // panelStats
            // ════════════════════════════════════════════════════════════════════════
            panelStats.Dock = DockStyle.Fill;
            panelStats.Name = "panelStats";
            panelStats.Padding = new Padding(20, 16, 20, 8);
            panelStats.Controls.Add(lblNote);
            panelStats.Controls.Add(flowCards);

            flowCards.AutoSize = true;
            flowCards.Dock = DockStyle.Top;
            flowCards.Name = "flowCards";
            flowCards.WrapContents = true;
            flowCards.Controls.AddRange(new Control[]
                { panelCard1, panelCard2, panelCard3, panelCard4 });

            lblNote.AutoSize = false;
            lblNote.Dock = DockStyle.Bottom;
            lblNote.Name = "lblNote";
            lblNote.Padding = new Padding(4, 0, 0, 8);
            lblNote.Size = new System.Drawing.Size(0, 28);
            lblNote.Text = "ℹ️  Đang tải số liệu...";
            lblNote.TextAlign = ContentAlignment.MiddleLeft;

            // ════════════════════════════════════════════════════════════════════════
            // Helper cục bộ: cấu hình TableLayoutPanel bên trong card
            //   Row 0 (Auto)    : panelCardXTop (accent bar 5px)
            //   Row 1 (Auto)    : header row  — col 0: icon, col 1: tiêu đề
            //   Row 2 (Percent) : số liệu lớn (span 2 cột)
            //   Row 3 (Auto)    : sub-text     (span 2 cột)
            // ════════════════════════════════════════════════════════════════════════

            // ── Card 1 ───────────────────────────────────────────────────────────────
            panelCard1.BorderStyle = BorderStyle.FixedSingle;
            panelCard1.Cursor = Cursors.Hand;
            panelCard1.Name = "panelCard1";
            // Size & Margin gán trong ApplyClientStyles()
            panelCard1.Controls.Add(tblCard1);

            tblCard1.Dock = DockStyle.Fill;
            tblCard1.Name = "tblCard1";
            tblCard1.ColumnCount = 2;
            tblCard1.RowCount = 4;
            tblCard1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52F));   // icon
            tblCard1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // text
            tblCard1.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));          // accent bar
            tblCard1.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));         // icon + title
            tblCard1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));        // số lớn
            tblCard1.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));         // sub-text

            // Row 0: accent bar (span 2 cột)
            panelCard1Top.Name = "panelCard1Top";
            panelCard1Top.Dock = DockStyle.Fill;
            panelCard1Top.Height = 5;
            tblCard1.Controls.Add(panelCard1Top, 0, 0);
            tblCard1.SetColumnSpan(panelCard1Top, 2);

            // Row 1 col 0: icon
            lblCard1Icon.Name = "lblCard1Icon";
            lblCard1Icon.Dock = DockStyle.Fill;
            lblCard1Icon.Text = "📁";
            lblCard1Icon.TextAlign = ContentAlignment.MiddleCenter;
            tblCard1.Controls.Add(lblCard1Icon, 0, 1);

            // Row 1 col 1: tiêu đề
            lblCard1Title.Name = "lblCard1Title";
            lblCard1Title.Dock = DockStyle.Fill;
            lblCard1Title.Text = "DỰ ÁN ĐANG CHẠY";
            lblCard1Title.TextAlign = ContentAlignment.MiddleLeft;
            tblCard1.Controls.Add(lblCard1Title, 1, 1);

            // Row 2: số lớn (span 2 cột)
            lblStatProjects.Name = "lblStatProjects";
            lblStatProjects.Dock = DockStyle.Fill;
            lblStatProjects.Text = "...";
            lblStatProjects.TextAlign = ContentAlignment.MiddleCenter;
            tblCard1.Controls.Add(lblStatProjects, 0, 2);
            tblCard1.SetColumnSpan(lblStatProjects, 2);

            // Row 3: sub-text (span 2 cột)
            lblCard1Sub.Name = "lblCard1Sub";
            lblCard1Sub.Dock = DockStyle.Fill;
            lblCard1Sub.Text = "dự án InProgress";
            lblCard1Sub.TextAlign = ContentAlignment.MiddleCenter;
            tblCard1.Controls.Add(lblCard1Sub, 0, 3);
            tblCard1.SetColumnSpan(lblCard1Sub, 2);

            // ── Card 2 ───────────────────────────────────────────────────────────────
            panelCard2.BorderStyle = BorderStyle.FixedSingle;
            panelCard2.Cursor = Cursors.Hand;
            panelCard2.Name = "panelCard2";
            panelCard2.Controls.Add(tblCard2);

            tblCard2.Dock = DockStyle.Fill;
            tblCard2.Name = "tblCard2";
            tblCard2.ColumnCount = 2;
            tblCard2.RowCount = 4;
            tblCard2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52F));
            tblCard2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCard2.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tblCard2.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tblCard2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblCard2.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));

            panelCard2Top.Name = "panelCard2Top";
            panelCard2Top.Dock = DockStyle.Fill;
            panelCard2Top.Height = 5;
            tblCard2.Controls.Add(panelCard2Top, 0, 0);
            tblCard2.SetColumnSpan(panelCard2Top, 2);

            lblCard2Icon.Name = "lblCard2Icon";
            lblCard2Icon.Dock = DockStyle.Fill;
            lblCard2Icon.Text = "✅";
            lblCard2Icon.TextAlign = ContentAlignment.MiddleCenter;
            tblCard2.Controls.Add(lblCard2Icon, 0, 1);

            lblCard2Title.Name = "lblCard2Title";
            lblCard2Title.Dock = DockStyle.Fill;
            lblCard2Title.Text = "CÔNG VIỆC CỦA TÔI";
            lblCard2Title.TextAlign = ContentAlignment.MiddleLeft;
            tblCard2.Controls.Add(lblCard2Title, 1, 1);

            lblStatTasks.Name = "lblStatTasks";
            lblStatTasks.Dock = DockStyle.Fill;
            lblStatTasks.Text = "...";
            lblStatTasks.TextAlign = ContentAlignment.MiddleCenter;
            tblCard2.Controls.Add(lblStatTasks, 0, 2);
            tblCard2.SetColumnSpan(lblStatTasks, 2);

            lblCard2Sub.Name = "lblCard2Sub";
            lblCard2Sub.Dock = DockStyle.Fill;
            lblCard2Sub.Text = "task được giao";
            lblCard2Sub.TextAlign = ContentAlignment.MiddleCenter;
            tblCard2.Controls.Add(lblCard2Sub, 0, 3);
            tblCard2.SetColumnSpan(lblCard2Sub, 2);

            // ── Card 3 ───────────────────────────────────────────────────────────────
            panelCard3.BorderStyle = BorderStyle.FixedSingle;
            panelCard3.Cursor = Cursors.Hand;
            panelCard3.Name = "panelCard3";
            panelCard3.Controls.Add(tblCard3);

            tblCard3.Dock = DockStyle.Fill;
            tblCard3.Name = "tblCard3";
            tblCard3.ColumnCount = 2;
            tblCard3.RowCount = 4;
            tblCard3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52F));
            tblCard3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCard3.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tblCard3.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tblCard3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblCard3.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));

            panelCard3Top.Name = "panelCard3Top";
            panelCard3Top.Dock = DockStyle.Fill;
            panelCard3Top.Height = 5;
            tblCard3.Controls.Add(panelCard3Top, 0, 0);
            tblCard3.SetColumnSpan(panelCard3Top, 2);

            lblCard3Icon.Name = "lblCard3Icon";
            lblCard3Icon.Dock = DockStyle.Fill;
            lblCard3Icon.Text = "⚠️";
            lblCard3Icon.TextAlign = ContentAlignment.MiddleCenter;
            tblCard3.Controls.Add(lblCard3Icon, 0, 1);

            lblCard3Title.Name = "lblCard3Title";
            lblCard3Title.Dock = DockStyle.Fill;
            lblCard3Title.Text = "QUÁ HẠN";
            lblCard3Title.TextAlign = ContentAlignment.MiddleLeft;
            tblCard3.Controls.Add(lblCard3Title, 1, 1);

            lblStatOverdue.Name = "lblStatOverdue";
            lblStatOverdue.Dock = DockStyle.Fill;
            lblStatOverdue.Text = "...";
            lblStatOverdue.TextAlign = ContentAlignment.MiddleCenter;
            tblCard3.Controls.Add(lblStatOverdue, 0, 2);
            tblCard3.SetColumnSpan(lblStatOverdue, 2);

            lblCard3Sub.Name = "lblCard3Sub";
            lblCard3Sub.Dock = DockStyle.Fill;
            lblCard3Sub.Text = "task đã qua deadline";
            lblCard3Sub.TextAlign = ContentAlignment.MiddleCenter;
            tblCard3.Controls.Add(lblCard3Sub, 0, 3);
            tblCard3.SetColumnSpan(lblCard3Sub, 2);

            // ── Card 4 ───────────────────────────────────────────────────────────────
            panelCard4.BorderStyle = BorderStyle.FixedSingle;
            panelCard4.Cursor = Cursors.Hand;
            panelCard4.Name = "panelCard4";
            panelCard4.Controls.Add(tblCard4);

            tblCard4.Dock = DockStyle.Fill;
            tblCard4.Name = "tblCard4";
            tblCard4.ColumnCount = 2;
            tblCard4.RowCount = 4;
            tblCard4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52F));
            tblCard4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblCard4.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tblCard4.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tblCard4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblCard4.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));

            panelCard4Top.Name = "panelCard4Top";
            panelCard4Top.Dock = DockStyle.Fill;
            panelCard4Top.Height = 5;
            tblCard4.Controls.Add(panelCard4Top, 0, 0);
            tblCard4.SetColumnSpan(panelCard4Top, 2);

            lblCard4Icon.Name = "lblCard4Icon";
            lblCard4Icon.Dock = DockStyle.Fill;
            lblCard4Icon.Text = "🎯";
            lblCard4Icon.TextAlign = ContentAlignment.MiddleCenter;
            tblCard4.Controls.Add(lblCard4Icon, 0, 1);

            lblCard4Title.Name = "lblCard4Title";
            lblCard4Title.Dock = DockStyle.Fill;
            lblCard4Title.Text = "XONG THÁNG NÀY";
            lblCard4Title.TextAlign = ContentAlignment.MiddleLeft;
            tblCard4.Controls.Add(lblCard4Title, 1, 1);

            lblStatDone.Name = "lblStatDone";
            lblStatDone.Dock = DockStyle.Fill;
            lblStatDone.Text = "...";
            lblStatDone.TextAlign = ContentAlignment.MiddleCenter;
            tblCard4.Controls.Add(lblStatDone, 0, 2);
            tblCard4.SetColumnSpan(lblStatDone, 2);

            lblCard4Sub.Name = "lblCard4Sub";
            lblCard4Sub.Dock = DockStyle.Fill;
            lblCard4Sub.Text = "task hoàn thành tháng này";
            lblCard4Sub.TextAlign = ContentAlignment.MiddleCenter;
            tblCard4.Controls.Add(lblCard4Sub, 0, 3);
            tblCard4.SetColumnSpan(lblCard4Sub, 2);

            // ════════════════════════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 580);
            this.Name = "frmHome";
            this.Text = "🏠  Trang chủ";
            this.StartPosition = FormStartPosition.Manual;

            // Thứ tự Add quan trọng: Fill trước → Top sau
            this.Controls.Add(panelBody);
            this.Controls.Add(panelHeader);

            tblCard4.ResumeLayout(false);
            panelCard4.ResumeLayout(false);
            tblCard3.ResumeLayout(false);
            panelCard3.ResumeLayout(false);
            tblCard2.ResumeLayout(false);
            panelCard2.ResumeLayout(false);
            tblCard1.ResumeLayout(false);
            panelCard1.ResumeLayout(false);
            flowCards.ResumeLayout(false);
            panelStats.ResumeLayout(false);
            tblWelcome.ResumeLayout(false);
            panelWelcome.ResumeLayout(false);
            panelBody.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ════════════════════════════════════════════════════════════════════════
        // Field declarations
        // ════════════════════════════════════════════════════════════════════════
        private Panel panelHeader;
        private Panel panelAccentLine;
        private Label lblHeader;

        private Panel panelBody;

        private Panel panelWelcome;
        private TableLayoutPanel tblWelcome;
        private Label lblGreeting;
        private Label lblRole;
        private Label lblLastLogin;

        private Panel panelStats;
        private FlowLayoutPanel flowCards;
        private Label lblNote;

        private Panel panelCard1;
        private Panel panelCard1Top;
        private TableLayoutPanel tblCard1;
        private Label lblCard1Icon;
        private Label lblCard1Title;
        private Label lblStatProjects;
        private Label lblCard1Sub;

        private Panel panelCard2;
        private Panel panelCard2Top;
        private TableLayoutPanel tblCard2;
        private Label lblCard2Icon;
        private Label lblCard2Title;
        private Label lblStatTasks;
        private Label lblCard2Sub;

        private Panel panelCard3;
        private Panel panelCard3Top;
        private TableLayoutPanel tblCard3;
        private Label lblCard3Icon;
        private Label lblCard3Title;
        private Label lblStatOverdue;
        private Label lblCard3Sub;

        private Panel panelCard4;
        private Panel panelCard4Top;
        private TableLayoutPanel tblCard4;
        private Label lblCard4Icon;
        private Label lblCard4Title;
        private Label lblStatDone;
        private Label lblCard4Sub;
    }
}