// ============================================================
//  frmKanban.Designer.cs  —  TaskFlowManagement.WinForms.Forms
//
//  LAYOUT mới:
//  ┌─ panelHeader (slate-900, 58px) ─────────────────────────┐
//  │  lblHeader + panelAccentLine (blue, 4px bottom)         │
//  ├─ panelFilter (slate-50, 50px) ──────────────────────────┤
//  │  [🔄 Làm mới]  [Tất cả ▼]  [👤 Của tôi]  [⚠ Quá hạn] │
//  │  hint label (muted)                                     │
//  ├─ tlpBoard (6 cột, Fill) ────────────────────────────────┤
//  │  Mỗi cột: pnlColumn → [pnlColHeader: lblColName + lblBadge]│
//  │                     → flpXxx (AutoScroll)               │
//  ├─ panelToast (success/danger, 32px, ẩn mặc định) ────────┤
//  └─ panelStatus (slate-900, 28px) ─────────────────────────┘
// ============================================================
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmKanban
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

            panelFilter = new Panel();
            btnRefresh = new Button();
            btnFilterAll = new Button();
            btnFilterMine = new Button();
            btnFilterOverdue = new Button();
            lblFilterHint = new Label();

            tlpBoard = new TableLayoutPanel();

            pnlTodo = new Panel(); lblTodo = new Label(); flpTodo = new DoubleBufferedFlowLayoutPanel(); lblBadgeTodo = new Label();
            pnlInProgress = new Panel(); lblInProgress = new Label(); flpInProgress = new DoubleBufferedFlowLayoutPanel(); lblBadgeInProgress = new Label();
            pnlReview = new Panel(); lblReview = new Label(); flpReview = new DoubleBufferedFlowLayoutPanel(); lblBadgeReview = new Label();
            pnlTesting = new Panel(); lblTesting = new Label(); flpTesting = new DoubleBufferedFlowLayoutPanel(); lblBadgeTesting = new Label();
            pnlFailed = new Panel(); lblFailed = new Label(); flpFailed = new DoubleBufferedFlowLayoutPanel(); lblBadgeFailed = new Label();
            pnlDone = new Panel(); lblDone = new Label(); flpDone = new DoubleBufferedFlowLayoutPanel(); lblBadgeDone = new Label();

            panelToast = new Panel(); lblToast = new Label();
            panelStatus = new Panel(); lblStatus = new Label();

            panelHeader.SuspendLayout();
            panelFilter.SuspendLayout();
            panelToast.SuspendLayout();
            panelStatus.SuspendLayout();
            tlpBoard.SuspendLayout();
            this.SuspendLayout();

            // ── panelHeader ────────────────────────────────────────────────────────
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 58;
            panelHeader.Name = "panelHeader";
            panelHeader.Controls.AddRange(new Control[] { lblHeader, panelAccentLine });

            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Height = 4;
            panelAccentLine.Name = "panelAccentLine";

            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new Padding(18, 0, 0, 4);
            lblHeader.Text = "🗂️  Kanban Board";
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── panelFilter ────────────────────────────────────────────────────────
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Height = 50;
            panelFilter.Name = "panelFilter";
            panelFilter.Controls.AddRange(new Control[]
            { btnRefresh, btnFilterAll, btnFilterMine, btnFilterOverdue, lblFilterHint });

            btnRefresh.Name = "btnRefresh";
            btnRefresh.Text = "🔄  Làm mới";
            btnRefresh.Size = new System.Drawing.Size(110, 30);
            btnRefresh.Location = new System.Drawing.Point(12, 10);
            btnRefresh.Click += btnRefresh_Click;

            btnFilterAll.Name = "btnFilterAll";
            btnFilterAll.Text = "Tất cả";
            btnFilterAll.Size = new System.Drawing.Size(80, 30);
            btnFilterAll.Location = new System.Drawing.Point(132, 10);

            btnFilterMine.Name = "btnFilterMine";
            btnFilterMine.Text = "👤  Của tôi";
            btnFilterMine.Size = new System.Drawing.Size(100, 30);
            btnFilterMine.Location = new System.Drawing.Point(222, 10);

            btnFilterOverdue.Name = "btnFilterOverdue";
            btnFilterOverdue.Text = "⚠  Quá hạn";
            btnFilterOverdue.Size = new System.Drawing.Size(100, 30);
            btnFilterOverdue.Location = new System.Drawing.Point(332, 10);

            lblFilterHint.AutoSize = false;
            lblFilterHint.Location = new System.Drawing.Point(448, 16);
            lblFilterHint.Name = "lblFilterHint";
            lblFilterHint.Size = new System.Drawing.Size(500, 18);
            lblFilterHint.Text = "Double-click vào card để xem chi tiết · Kéo thả để đổi trạng thái · Double-click tiêu đề để sửa trực tiếp";

            // ── panelToast ─────────────────────────────────────────────────────────
            panelToast.Dock = DockStyle.Bottom;
            panelToast.Height = 34;
            panelToast.Name = "panelToast";
            panelToast.Visible = false;
            panelToast.Controls.Add(lblToast);

            lblToast.AutoSize = false;
            lblToast.Dock = DockStyle.Fill;
            lblToast.ForeColor = System.Drawing.Color.White;
            lblToast.Name = "lblToast";
            lblToast.Padding = new Padding(16, 0, 0, 0);
            lblToast.Text = "";
            lblToast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── panelStatus ────────────────────────────────────────────────────────
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Height = 28;
            panelStatus.Name = "panelStatus";
            panelStatus.Controls.Add(lblStatus);

            lblStatus.AutoSize = false;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(12, 0, 0, 0);
            lblStatus.Text = "Sẵn sàng";
            lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── tlpBoard — chỉ khai báo cơ bản, ColumnStyles được set trong ApplyClientStyles() ──
            tlpBoard.ColumnCount = 6;
            tlpBoard.Controls.Add(pnlTodo, 0, 0);
            tlpBoard.Controls.Add(pnlInProgress, 1, 0);
            tlpBoard.Controls.Add(pnlReview, 2, 0);
            tlpBoard.Controls.Add(pnlTesting, 3, 0);
            tlpBoard.Controls.Add(pnlFailed, 4, 0);
            tlpBoard.Controls.Add(pnlDone, 5, 0);
            tlpBoard.Dock = DockStyle.Fill;
            tlpBoard.Name = "tlpBoard";
            tlpBoard.Padding = new Padding(8, 10, 8, 10);
            tlpBoard.RowCount = 1;
            tlpBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBoard.TabIndex = 0;

            // ── Các panel cột — chỉ khai báo tên; layout được set trong BuildColumn() ──
            pnlTodo.Dock = DockStyle.Fill; pnlTodo.Name = "pnlTodo";
            pnlInProgress.Dock = DockStyle.Fill; pnlInProgress.Name = "pnlInProgress";
            pnlReview.Dock = DockStyle.Fill; pnlReview.Name = "pnlReview";
            pnlTesting.Dock = DockStyle.Fill; pnlTesting.Name = "pnlTesting";
            pnlFailed.Dock = DockStyle.Fill; pnlFailed.Name = "pnlFailed";
            pnlDone.Dock = DockStyle.Fill; pnlDone.Name = "pnlDone";

            lblTodo.Name = "lblTodo"; flpTodo.Name = "flpTodo"; lblBadgeTodo.Name = "lblBadgeTodo";
            lblInProgress.Name = "lblInProgress"; flpInProgress.Name = "flpInProgress"; lblBadgeInProgress.Name = "lblBadgeInProgress";
            lblReview.Name = "lblReview"; flpReview.Name = "flpReview"; lblBadgeReview.Name = "lblBadgeReview";
            lblTesting.Name = "lblTesting"; flpTesting.Name = "flpTesting"; lblBadgeTesting.Name = "lblBadgeTesting";
            lblFailed.Name = "lblFailed"; flpFailed.Name = "flpFailed"; lblBadgeFailed.Name = "lblBadgeFailed";
            lblDone.Name = "lblDone"; flpDone.Name = "flpDone"; lblBadgeDone.Name = "lblBadgeDone";

            // ── Form ───────────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 750);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "frmKanban";
            this.Text = "🗂️  Kanban Board";

            // Thứ tự Add: Fill → Bottom → Top
            this.Controls.Add(tlpBoard);
            this.Controls.Add(panelFilter);
            this.Controls.Add(panelHeader);
            this.Controls.Add(panelToast);
            this.Controls.Add(panelStatus);

            panelHeader.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            panelToast.ResumeLayout(false);
            panelStatus.ResumeLayout(false);
            tlpBoard.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Badge Paint: pill bo tròn ─────────────────────────────────────────
        private static void LblBadge_Paint(object? sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (sender is not Label lbl) return;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new System.Drawing.Rectangle(0, 0, lbl.Width - 1, lbl.Height - 1);
            using var brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(100, 0, 0, 0));
            e.Graphics.FillEllipse(brush, rect);
            using var sf = new System.Drawing.StringFormat
            {
                Alignment     = System.Drawing.StringAlignment.Center,
                LineAlignment = System.Drawing.StringAlignment.Center
            };
            e.Graphics.DrawString(lbl.Text, lbl.Font,
                System.Drawing.Brushes.White, rect, sf);
        }

        #region Control declarations

        private Panel panelHeader;
        private Panel panelAccentLine;
        private Label lblHeader;

        private Panel  panelFilter;
        private Button btnRefresh;
        private Button btnFilterAll;
        private Button btnFilterMine;
        private Button btnFilterOverdue;
        private Label  lblFilterHint;

        private TableLayoutPanel tlpBoard;

        private Panel pnlTodo;       private Label lblTodo;       private DoubleBufferedFlowLayoutPanel flpTodo;       private Label lblBadgeTodo;
        private Panel pnlInProgress; private Label lblInProgress; private DoubleBufferedFlowLayoutPanel flpInProgress; private Label lblBadgeInProgress;
        private Panel pnlReview;     private Label lblReview;     private DoubleBufferedFlowLayoutPanel flpReview;     private Label lblBadgeReview;
        private Panel pnlTesting;    private Label lblTesting;    private DoubleBufferedFlowLayoutPanel flpTesting;    private Label lblBadgeTesting;
        private Panel pnlFailed;     private Label lblFailed;     private DoubleBufferedFlowLayoutPanel flpFailed;     private Label lblBadgeFailed;
        private Panel pnlDone;       private Label lblDone;       private DoubleBufferedFlowLayoutPanel flpDone;       private Label lblBadgeDone;

        private Panel panelToast;  private Label lblToast;
        private Panel panelStatus; private Label lblStatus;

        #endregion
    }
}
