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
            // ── Instantiation ─────────────────────────────────────────────────
            panelHeader     = new Panel();
            panelAccentLine = new Panel();
            lblHeader       = new Label();

            panelFilter       = new Panel();
            btnRefresh        = new Button();
            btnFilterAll      = new Button();
            btnFilterMine     = new Button();
            btnFilterOverdue  = new Button();
            lblFilterHint     = new Label();

            tlpBoard = new TableLayoutPanel();

            // 6 cột Kanban
            pnlTodo       = new Panel(); lblTodo       = new Label(); flpTodo       = new DoubleBufferedFlowLayoutPanel(); lblBadgeTodo       = new Label();
            pnlInProgress = new Panel(); lblInProgress = new Label(); flpInProgress = new DoubleBufferedFlowLayoutPanel(); lblBadgeInProgress = new Label();
            pnlReview     = new Panel(); lblReview     = new Label(); flpReview     = new DoubleBufferedFlowLayoutPanel(); lblBadgeReview     = new Label();
            pnlTesting    = new Panel(); lblTesting    = new Label(); flpTesting    = new DoubleBufferedFlowLayoutPanel(); lblBadgeTesting    = new Label();
            pnlFailed     = new Panel(); lblFailed     = new Label(); flpFailed     = new DoubleBufferedFlowLayoutPanel(); lblBadgeFailed     = new Label();
            pnlDone       = new Panel(); lblDone       = new Label(); flpDone       = new DoubleBufferedFlowLayoutPanel(); lblBadgeDone       = new Label();

            // Toast + Status bar
            panelToast  = new Panel(); lblToast  = new Label();
            panelStatus = new Panel(); lblStatus = new Label();

            panelHeader.SuspendLayout();
            panelFilter.SuspendLayout();
            panelToast.SuspendLayout();
            panelStatus.SuspendLayout();
            tlpBoard.SuspendLayout();
            this.SuspendLayout();

            // ══════════════════════════════════════════════════════════════════
            // panelHeader
            // ══════════════════════════════════════════════════════════════════
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelHeader.Dock      = DockStyle.Top;
            panelHeader.Height    = 58;
            panelHeader.Name      = "panelHeader";
            panelHeader.Controls.AddRange(new Control[] { lblHeader, panelAccentLine });

            panelAccentLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            panelAccentLine.Dock      = DockStyle.Bottom;
            panelAccentLine.Height    = 4;
            panelAccentLine.Name      = "panelAccentLine";

            lblHeader.AutoSize  = false;
            lblHeader.Dock      = DockStyle.Fill;
            lblHeader.Font      = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;
            lblHeader.Name      = "lblHeader";
            lblHeader.Padding   = new Padding(18, 0, 0, 4);
            lblHeader.Text      = "🗂️  Kanban Board";
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ══════════════════════════════════════════════════════════════════
            // panelFilter — toolbar với filter buttons
            // ══════════════════════════════════════════════════════════════════
            panelFilter.BackColor = UIHelper.ColorBackground;
            panelFilter.Dock      = DockStyle.Top;
            panelFilter.Height    = 50;
            panelFilter.Name      = "panelFilter";
            panelFilter.Controls.AddRange(new Control[]
                { btnRefresh, btnFilterAll, btnFilterMine, btnFilterOverdue, lblFilterHint });

            // [🔄 Làm mới]
            UIHelper.StyleToolButton(btnRefresh, "🔄  Làm mới",
                UIHelper.ButtonVariant.Secondary, 12, 10, 110, 30);
            btnRefresh.Name   = "btnRefresh";
            btnRefresh.Click += btnRefresh_Click;

            // [Tất cả] — active mặc định (Primary)
            UIHelper.StyleToolButton(btnFilterAll, "Tất cả",
                UIHelper.ButtonVariant.Primary, 132, 10, 80, 30);
            btnFilterAll.Name = "btnFilterAll";

            // [👤 Của tôi]
            UIHelper.StyleToolButton(btnFilterMine, "👤  Của tôi",
                UIHelper.ButtonVariant.Secondary, 222, 10, 100, 30);
            btnFilterMine.Name = "btnFilterMine";

            // [⚠ Quá hạn]
            UIHelper.StyleToolButton(btnFilterOverdue, "⚠  Quá hạn",
                UIHelper.ButtonVariant.Secondary, 332, 10, 100, 30);
            btnFilterOverdue.Name = "btnFilterOverdue";

            // Hint label
            lblFilterHint.AutoSize  = false;
            lblFilterHint.Font      = UIHelper.FontSmall;
            lblFilterHint.ForeColor = UIHelper.ColorMuted;
            lblFilterHint.Location  = new System.Drawing.Point(448, 16);
            lblFilterHint.Name      = "lblFilterHint";
            lblFilterHint.Size      = new System.Drawing.Size(500, 18);
            lblFilterHint.Text      = "Double-click vào card để xem chi tiết · Kéo thả để đổi trạng thái · Double-click tiêu đề để sửa trực tiếp";

            // ══════════════════════════════════════════════════════════════════
            // panelToast — thông báo nổi (ẩn mặc định)
            // ══════════════════════════════════════════════════════════════════
            panelToast.BackColor = UIHelper.ColorSuccess;
            panelToast.Dock      = DockStyle.Bottom;
            panelToast.Height    = 34;
            panelToast.Name      = "panelToast";
            panelToast.Visible   = false;
            panelToast.Controls.Add(lblToast);

            lblToast.AutoSize  = false;
            lblToast.Dock      = DockStyle.Fill;
            lblToast.Font      = UIHelper.FontBold;
            lblToast.ForeColor = System.Drawing.Color.White;
            lblToast.Name      = "lblToast";
            lblToast.Padding   = new Padding(16, 0, 0, 0);
            lblToast.Text      = "";
            lblToast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ══════════════════════════════════════════════════════════════════
            // panelStatus — status bar dưới cùng
            // ══════════════════════════════════════════════════════════════════
            panelStatus.BackColor = UIHelper.ColorHeaderBg;
            panelStatus.Dock      = DockStyle.Bottom;
            panelStatus.Height    = 28;
            panelStatus.Name      = "panelStatus";
            panelStatus.Controls.Add(lblStatus);

            lblStatus.AutoSize  = false;
            lblStatus.Dock      = DockStyle.Fill;
            lblStatus.Font      = UIHelper.FontSmall;
            lblStatus.ForeColor = UIHelper.ColorSubtitle;
            lblStatus.Name      = "lblStatus";
            lblStatus.Padding   = new Padding(12, 0, 0, 0);
            lblStatus.Text      = "Sẵn sàng";
            lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ══════════════════════════════════════════════════════════════════
            // tlpBoard — 6-cột bố cục chính
            // ══════════════════════════════════════════════════════════════════
            tlpBoard.BackColor   = UIHelper.ColorBackground;
            tlpBoard.ColumnCount = 6;
            for (int i = 0; i < 6; i++)
                tlpBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 6));
            tlpBoard.Controls.Add(pnlTodo,       0, 0);
            tlpBoard.Controls.Add(pnlInProgress, 1, 0);
            tlpBoard.Controls.Add(pnlReview,     2, 0);
            tlpBoard.Controls.Add(pnlTesting,    3, 0);
            tlpBoard.Controls.Add(pnlFailed,     4, 0);
            tlpBoard.Controls.Add(pnlDone,       5, 0);
            tlpBoard.Dock        = DockStyle.Fill;
            tlpBoard.Name        = "tlpBoard";
            tlpBoard.Padding     = new Padding(8, 10, 8, 10);
            tlpBoard.RowCount    = 1;
            tlpBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBoard.TabIndex    = 0;

            // ── Cột 1: To Do ─────────────────────────────────────────────────
            BuildColumn(pnlTodo, lblTodo, flpTodo, lblBadgeTodo,
                text: "📋  To Do",
                headerBg:  System.Drawing.Color.FromArgb(226, 232, 240),
                headerFg:  System.Drawing.Color.FromArgb(51, 65, 85),
                bodyBg:    System.Drawing.Color.FromArgb(248, 250, 252));

            // ── Cột 2: In Progress ───────────────────────────────────────────
            BuildColumn(pnlInProgress, lblInProgress, flpInProgress, lblBadgeInProgress,
                text: "🔄  In Progress",
                headerBg: System.Drawing.Color.FromArgb(191, 219, 254),
                headerFg: System.Drawing.Color.FromArgb(30, 58, 138),
                bodyBg:   System.Drawing.Color.FromArgb(239, 246, 255));

            // ── Cột 3: Review ────────────────────────────────────────────────
            BuildColumn(pnlReview, lblReview, flpReview, lblBadgeReview,
                text: "🔍  Review",
                headerBg: System.Drawing.Color.FromArgb(253, 230, 138),
                headerFg: System.Drawing.Color.FromArgb(120, 53, 15),
                bodyBg:   System.Drawing.Color.FromArgb(255, 251, 235));

            // ── Cột 4: Testing ───────────────────────────────────────────────
            BuildColumn(pnlTesting, lblTesting, flpTesting, lblBadgeTesting,
                text: "🧪  Testing",
                headerBg: System.Drawing.Color.FromArgb(221, 214, 254),
                headerFg: System.Drawing.Color.FromArgb(76, 29, 149),
                bodyBg:   System.Drawing.Color.FromArgb(245, 243, 255));

            // ── Cột 5: Failed ────────────────────────────────────────────────
            BuildColumn(pnlFailed, lblFailed, flpFailed, lblBadgeFailed,
                text: "❌  Failed",
                headerBg: System.Drawing.Color.FromArgb(254, 202, 202),
                headerFg: System.Drawing.Color.FromArgb(127, 29, 29),
                bodyBg:   System.Drawing.Color.FromArgb(254, 242, 242));

            // ── Cột 6: Done ──────────────────────────────────────────────────
            BuildColumn(pnlDone, lblDone, flpDone, lblBadgeDone,
                text: "✅  Done",
                headerBg: System.Drawing.Color.FromArgb(167, 243, 208),
                headerFg: System.Drawing.Color.FromArgb(6, 78, 59),
                bodyBg:   System.Drawing.Color.FromArgb(236, 253, 245));

            // ══════════════════════════════════════════════════════════════════
            // frmKanban — form root
            // ══════════════════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = UIHelper.ColorBackground;
            this.ClientSize          = new System.Drawing.Size(1280, 750);
            this.Controls.Add(tlpBoard);
            this.Controls.Add(panelFilter);
            this.Controls.Add(panelHeader);
            this.Controls.Add(panelToast);
            this.Controls.Add(panelStatus);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name        = "frmKanban";
            this.Text        = "🗂️  Kanban Board";

            panelHeader.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            panelToast.ResumeLayout(false);
            panelStatus.ResumeLayout(false);
            tlpBoard.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Column builder helper ─────────────────────────────────────────────
        /// <summary>
        /// Tạo cấu trúc 1 cột Kanban: pnlColumn → pnlColHeader + flp.
        /// pnlColHeader chứa lblColName (trái) + lblBadge (phải, bo tròn).
        /// </summary>
        private static void BuildColumn(
            Panel                        pnlColumn,
            Label                        lblColName,
            DoubleBufferedFlowLayoutPanel flp,
            Label                        lblBadge,
            string                       text,
            System.Drawing.Color         headerBg,
            System.Drawing.Color         headerFg,
            System.Drawing.Color         bodyBg)
        {
            // Column container
            pnlColumn.BackColor = bodyBg;
            pnlColumn.Dock      = DockStyle.Fill;
            pnlColumn.Margin    = new Padding(4);
            pnlColumn.Name      = pnlColumn.Name;
            pnlColumn.Padding   = new Padding(0);

            // Header panel (chứa lblColName + lblBadge)
            var pnlColHeader = new Panel
            {
                BackColor = headerBg,
                Dock      = DockStyle.Top,
                Height    = 44,
                Name      = "pnlColHeader"
            };

            // Column name label
            lblColName.AutoSize   = false;
            lblColName.BackColor  = System.Drawing.Color.Transparent;
            lblColName.Dock       = DockStyle.Fill;
            lblColName.Font       = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblColName.ForeColor  = headerFg;
            lblColName.Name       = lblColName.Name;
            lblColName.Padding    = new Padding(10, 0, 40, 0);   // right padding cho badge
            lblColName.Text       = text;
            lblColName.TextAlign  = System.Drawing.ContentAlignment.MiddleLeft;

            // Badge đếm (badge số thẻ, bo tròn, góc phải)
            lblBadge.AutoSize   = false;
            lblBadge.BackColor  = System.Drawing.Color.FromArgb(80, 0, 0, 0);   // semi-transparent dark
            lblBadge.ForeColor  = System.Drawing.Color.White;
            lblBadge.Font       = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            lblBadge.Location   = new System.Drawing.Point(0, 12);     // sẽ dời bằng Anchor
            lblBadge.Anchor     = AnchorStyles.Top | AnchorStyles.Right;
            lblBadge.Name       = lblBadge.Name;
            lblBadge.Size       = new System.Drawing.Size(30, 20);
            lblBadge.Text       = "0";
            lblBadge.TextAlign  = System.Drawing.ContentAlignment.MiddleCenter;
            lblBadge.Paint     += LblBadge_Paint;    // vẽ hình viên thuốc bo tròn

            // Đặt vị trí badge phải
            pnlColHeader.SizeChanged += (_, _) =>
                lblBadge.Left = pnlColHeader.Width - lblBadge.Width - 8;

            pnlColHeader.Controls.AddRange(new Control[] { lblColName, lblBadge });

            // FlowLayoutPanel chứa các card
            flp.AutoScroll     = true;
            flp.BackColor      = bodyBg;
            flp.Dock           = DockStyle.Fill;
            flp.FlowDirection  = FlowDirection.TopDown;
            flp.Name           = flp.Name;
            flp.Padding        = new Padding(6);
            flp.WrapContents   = false;

            pnlColumn.Controls.AddRange(new Control[] { flp, pnlColHeader });
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
