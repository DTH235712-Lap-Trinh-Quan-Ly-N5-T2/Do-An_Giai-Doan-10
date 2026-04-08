using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmHome   // BaseForm declared in frmHome.cs
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Instantiation ──────────────────────────────────────────────────────
            panelHeader = new Panel();
            panelAccentLine = new Panel();
            lblHeader = new Label();

            panelBody = new Panel();
            panelWelcome = new Panel();
            lblGreeting = new Label();
            lblRole = new Label();
            lblLastLogin = new Label();

            panelStats = new Panel();
            flowCards = new FlowLayoutPanel();
            lblNote = new Label();

            panelCard1 = new Panel();
            panelCard1Top = new Panel();
            lblCard1Icon = new Label();
            lblCard1Title = new Label();
            lblStatProjects = new Label();
            lblCard1Sub = new Label();

            panelCard2 = new Panel();
            panelCard2Top = new Panel();
            lblCard2Icon = new Label();
            lblCard2Title = new Label();
            lblStatTasks = new Label();
            lblCard2Sub = new Label();

            panelCard3 = new Panel();
            panelCard3Top = new Panel();
            lblCard3Icon = new Label();
            lblCard3Title = new Label();
            lblStatOverdue = new Label();
            lblCard3Sub = new Label();

            panelCard4 = new Panel();
            panelCard4Top = new Panel();
            lblCard4Icon = new Label();
            lblCard4Title = new Label();
            lblStatDone = new Label();
            lblCard4Sub = new Label();

            panelHeader.SuspendLayout();
            panelBody.SuspendLayout();
            panelWelcome.SuspendLayout();
            panelStats.SuspendLayout();
            this.SuspendLayout();

            // ── panelHeader ────────────────────────────────────────────────────────
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

            // ── panelBody ──────────────────────────────────────────────────────────
            panelBody.Dock = DockStyle.Fill;
            panelBody.Name = "panelBody";
            panelBody.Controls.Add(panelStats);
            panelBody.Controls.Add(panelWelcome);

            // ── panelWelcome ───────────────────────────────────────────────────────
            panelWelcome.Dock = DockStyle.Top;
            panelWelcome.Height = 98;
            panelWelcome.Name = "panelWelcome";
            panelWelcome.Padding = new Padding(24, 12, 24, 0);
            panelWelcome.Controls.AddRange(new Control[] { lblGreeting, lblRole, lblLastLogin });

            lblGreeting.AutoSize = false;
            lblGreeting.Location = new Point(24, 12);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new System.Drawing.Size(900, 38);
            lblGreeting.Text = "Chào buổi sáng, ...! 👋";

            lblRole.AutoSize = false;
            lblRole.Location = new Point(26, 54);
            lblRole.Name = "lblRole";
            lblRole.Size = new System.Drawing.Size(500, 20);
            lblRole.Text = "Vai trò: ...";

            lblLastLogin.AutoSize = false;
            lblLastLogin.Location = new Point(26, 76);
            lblLastLogin.Name = "lblLastLogin";
            lblLastLogin.Size = new System.Drawing.Size(500, 18);
            lblLastLogin.Text = "";

            // ── panelStats ─────────────────────────────────────────────────────────
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

            // ── Card 1 — Dự án đang chạy ──────────────────────────────────────────
            panelCard1.BackColor = System.Drawing.Color.White;
            panelCard1.BorderStyle = BorderStyle.FixedSingle;
            panelCard1.Cursor = Cursors.Hand;
            panelCard1.Name = "panelCard1";
            // Size & Margin set trong ApplyClientStyles()
            panelCard1.Controls.AddRange(new Control[]
            { panelCard1Top, lblCard1Icon, lblCard1Title, lblStatProjects, lblCard1Sub });

            panelCard1Top.Dock = DockStyle.Top;
            panelCard1Top.Height = 5;
            panelCard1Top.Name = "panelCard1Top";

            lblCard1Icon.Location = new Point(14, 14);
            lblCard1Icon.Name = "lblCard1Icon";
            lblCard1Icon.Size = new System.Drawing.Size(44, 40);
            lblCard1Icon.Text = "📁";
            lblCard1Icon.TextAlign = ContentAlignment.MiddleCenter;

            lblCard1Title.Location = new Point(64, 22);
            lblCard1Title.Name = "lblCard1Title";
            lblCard1Title.Size = new System.Drawing.Size(162, 16);
            lblCard1Title.Text = "DỰ ÁN ĐANG CHẠY";

            lblStatProjects.Location = new Point(14, 58);
            lblStatProjects.Name = "lblStatProjects";
            lblStatProjects.Size = new System.Drawing.Size(200, 60);
            lblStatProjects.Text = "...";

            lblCard1Sub.Location = new Point(14, 122);
            lblCard1Sub.Name = "lblCard1Sub";
            lblCard1Sub.Size = new System.Drawing.Size(200, 16);
            lblCard1Sub.Text = "dự án InProgress";

            // ── Card 2 — Công việc của tôi ────────────────────────────────────────
            panelCard2.BackColor = System.Drawing.Color.White;
            panelCard2.BorderStyle = BorderStyle.FixedSingle;
            panelCard2.Cursor = Cursors.Hand;
            panelCard2.Name = "panelCard2";
            panelCard2.Controls.AddRange(new Control[]
            { panelCard2Top, lblCard2Icon, lblCard2Title, lblStatTasks, lblCard2Sub });

            panelCard2Top.Dock = DockStyle.Top;
            panelCard2Top.Height = 5;
            panelCard2Top.Name = "panelCard2Top";

            lblCard2Icon.Location = new Point(14, 14);
            lblCard2Icon.Name = "lblCard2Icon";
            lblCard2Icon.Size = new System.Drawing.Size(44, 40);
            lblCard2Icon.Text = "✅";
            lblCard2Icon.TextAlign = ContentAlignment.MiddleCenter;

            lblCard2Title.Location = new Point(64, 22);
            lblCard2Title.Name = "lblCard2Title";
            lblCard2Title.Size = new System.Drawing.Size(162, 16);
            lblCard2Title.Text = "CÔNG VIỆC CỦA TÔI";

            lblStatTasks.Location = new Point(14, 58);
            lblStatTasks.Name = "lblStatTasks";
            lblStatTasks.Size = new System.Drawing.Size(200, 60);
            lblStatTasks.Text = "...";

            lblCard2Sub.Location = new Point(14, 122);
            lblCard2Sub.Name = "lblCard2Sub";
            lblCard2Sub.Size = new System.Drawing.Size(200, 16);
            lblCard2Sub.Text = "task được giao";

            // ── Card 3 — Quá hạn ─────────────────────────────────────────────────
            panelCard3.BackColor = System.Drawing.Color.White;
            panelCard3.BorderStyle = BorderStyle.FixedSingle;
            panelCard3.Cursor = Cursors.Hand;
            panelCard3.Name = "panelCard3";
            panelCard3.Controls.AddRange(new Control[]
            { panelCard3Top, lblCard3Icon, lblCard3Title, lblStatOverdue, lblCard3Sub });

            panelCard3Top.Dock = DockStyle.Top;
            panelCard3Top.Height = 5;
            panelCard3Top.Name = "panelCard3Top";

            lblCard3Icon.Location = new Point(14, 14);
            lblCard3Icon.Name = "lblCard3Icon";
            lblCard3Icon.Size = new System.Drawing.Size(44, 40);
            lblCard3Icon.Text = "⚠️";
            lblCard3Icon.TextAlign = ContentAlignment.MiddleCenter;

            lblCard3Title.Location = new Point(64, 22);
            lblCard3Title.Name = "lblCard3Title";
            lblCard3Title.Size = new System.Drawing.Size(162, 16);
            lblCard3Title.Text = "QUÁ HẠN";

            lblStatOverdue.Location = new Point(14, 58);
            lblStatOverdue.Name = "lblStatOverdue";
            lblStatOverdue.Size = new System.Drawing.Size(200, 60);
            lblStatOverdue.Text = "...";

            lblCard3Sub.Location = new Point(14, 122);
            lblCard3Sub.Name = "lblCard3Sub";
            lblCard3Sub.Size = new System.Drawing.Size(200, 16);
            lblCard3Sub.Text = "task đã qua deadline";

            // ── Card 4 — Hoàn thành tháng này ────────────────────────────────────
            panelCard4.BackColor = System.Drawing.Color.White;
            panelCard4.BorderStyle = BorderStyle.FixedSingle;
            panelCard4.Cursor = Cursors.Hand;
            panelCard4.Name = "panelCard4";
            panelCard4.Controls.AddRange(new Control[]
            { panelCard4Top, lblCard4Icon, lblCard4Title, lblStatDone, lblCard4Sub });

            panelCard4Top.Dock = DockStyle.Top;
            panelCard4Top.Height = 5;
            panelCard4Top.Name = "panelCard4Top";

            lblCard4Icon.Location = new Point(14, 14);
            lblCard4Icon.Name = "lblCard4Icon";
            lblCard4Icon.Size = new System.Drawing.Size(44, 40);
            lblCard4Icon.Text = "🎯";
            lblCard4Icon.TextAlign = ContentAlignment.MiddleCenter;

            lblCard4Title.Location = new Point(64, 22);
            lblCard4Title.Name = "lblCard4Title";
            lblCard4Title.Size = new System.Drawing.Size(162, 16);
            lblCard4Title.Text = "XONG THÁNG NÀY";

            lblStatDone.Location = new Point(14, 58);
            lblStatDone.Name = "lblStatDone";
            lblStatDone.Size = new System.Drawing.Size(200, 60);
            lblStatDone.Text = "...";

            lblCard4Sub.Location = new Point(14, 122);
            lblCard4Sub.Name = "lblCard4Sub";
            lblCard4Sub.Size = new System.Drawing.Size(200, 16);
            lblCard4Sub.Text = "task hoàn thành tháng này";
            // Text thực có tháng cụ thể được gán trong ApplyClientStyles()

            // ── Form ───────────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 580);
            this.Name = "frmHome";
            this.Text = "🏠  Trang chủ";
            this.StartPosition = FormStartPosition.Manual;

            // Thứ tự Add: Fill trước → Top
            this.Controls.Add(panelBody);
            this.Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelBody.ResumeLayout(false);
            panelWelcome.ResumeLayout(false);
            panelStats.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Field declarations ────────────────────────────────────────────────
        private Panel panelHeader;
        private Panel panelAccentLine;
        private Label lblHeader;
        private Panel panelBody;
        private Panel panelWelcome;
        private Label lblGreeting;
        private Label lblRole;
        private Label lblLastLogin;
        private Panel panelStats;
        private FlowLayoutPanel flowCards;
        private Label lblNote;

        private Panel panelCard1;
        private Panel panelCard1Top;
        private Label lblCard1Icon;
        private Label lblCard1Title;
        private Label lblStatProjects;
        private Label lblCard1Sub;

        private Panel panelCard2;
        private Panel panelCard2Top;
        private Label lblCard2Icon;
        private Label lblCard2Title;
        private Label lblStatTasks;
        private Label lblCard2Sub;

        private Panel panelCard3;
        private Panel panelCard3Top;
        private Label lblCard3Icon;
        private Label lblCard3Title;
        private Label lblStatOverdue;
        private Label lblCard3Sub;

        private Panel panelCard4;
        private Panel panelCard4Top;
        private Label lblCard4Icon;
        private Label lblCard4Title;
        private Label lblStatDone;
        private Label lblCard4Sub;
    }
}