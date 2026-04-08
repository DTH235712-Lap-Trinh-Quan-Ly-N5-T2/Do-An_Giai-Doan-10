// ============================================================
//  ucTaskCard.Designer.cs  —  TaskFlowManagement.WinForms.Controls
//
//  LAYOUT mới (140px cao):
//  ┌─[3px priority bar]──────────────────────────────────────┐
//  │  [Avatar 28px]  lblTitle (bold, 2 dòng)                 │
//  │  lblAssignee (muted, 1 dòng)                            │
//  │  [chip Status]  [chip Priority]  [chip DueDate]         │
//  │  [████░░░░░░░░  progress bar 4px]  [lblProgress "60%"]  │
//  └─────────────────────────────────────────────────────────┘
// ============================================================
namespace TaskFlowManagement.WinForms.Controls
{
    partial class ucTaskCard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components       = new System.ComponentModel.Container();

            // ── Controls ──────────────────────────────────────────────────────
            this.pnlContainer     = new System.Windows.Forms.Panel();
            this.pnlPriorityBar   = new System.Windows.Forms.Panel();   // thanh màu 3px bên trái
            this.pnlContent       = new System.Windows.Forms.Panel();   // padding bên phải thanh màu

            this.lblAvatarInitials = new System.Windows.Forms.Label();  // avatar chữ tắt
            this.lblTitle          = new System.Windows.Forms.Label();
            this.lblAssignee       = new System.Windows.Forms.Label();

            // Row chip: Status | Priority | DueDate
            this.pnlChips          = new System.Windows.Forms.Panel();
            this.lblStatus         = new System.Windows.Forms.Label();
            this.lblPriority       = new System.Windows.Forms.Label();
            this.lblDueDate        = new System.Windows.Forms.Label();

            // Progress row
            this.pnlProgressTrack  = new System.Windows.Forms.Panel();
            this.pnlProgressFill   = new System.Windows.Forms.Panel();
            this.lblProgress       = new System.Windows.Forms.Label();

            // Context menu 10 trạng thái
            this.cmsTaskActions    = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCreated         = new System.Windows.Forms.ToolStripMenuItem();
            this.miAssigned        = new System.Windows.Forms.ToolStripMenuItem();
            this.miInProgress      = new System.Windows.Forms.ToolStripMenuItem();
            this.miFailed          = new System.Windows.Forms.ToolStripMenuItem();
            this.miReview1         = new System.Windows.Forms.ToolStripMenuItem();
            this.miReview2         = new System.Windows.Forms.ToolStripMenuItem();
            this.miApproved        = new System.Windows.Forms.ToolStripMenuItem();
            this.miInTest          = new System.Windows.Forms.ToolStripMenuItem();
            this.miResolved        = new System.Windows.Forms.ToolStripMenuItem();
            this.miClosed          = new System.Windows.Forms.ToolStripMenuItem();

            this.pnlContainer.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlChips.SuspendLayout();
            this.pnlProgressTrack.SuspendLayout();
            this.cmsTaskActions.SuspendLayout();
            this.SuspendLayout();

            // ══════════════════════════════════════════════════════════════════
            // pnlContainer — toàn bộ card (trắng, border 1px)
            // ══════════════════════════════════════════════════════════════════
            this.pnlContainer.BackColor   = System.Drawing.Color.White;
            this.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContainer.Dock        = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location    = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name        = "pnlContainer";
            this.pnlContainer.Size        = new System.Drawing.Size(250, 140);
            this.pnlContainer.TabIndex    = 0;
            this.pnlContainer.Controls.AddRange(new System.Windows.Forms.Control[]
                { this.pnlPriorityBar, this.pnlContent });

            // ══════════════════════════════════════════════════════════════════
            // pnlPriorityBar — thanh màu 3px bên trái
            // BackColor được set động trong Bind() theo priority
            // ══════════════════════════════════════════════════════════════════
            this.pnlPriorityBar.BackColor = System.Drawing.Color.FromArgb(226, 232, 240); // mặc định slate-200
            this.pnlPriorityBar.Dock      = System.Windows.Forms.DockStyle.Left;
            this.pnlPriorityBar.Name      = "pnlPriorityBar";
            this.pnlPriorityBar.Width     = 4;
            this.pnlPriorityBar.TabIndex  = 0;

            // ══════════════════════════════════════════════════════════════════
            // pnlContent — khu vực nội dung (padding 8px)
            // ══════════════════════════════════════════════════════════════════
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Name      = "pnlContent";
            this.pnlContent.Padding   = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.pnlContent.TabIndex  = 1;
            this.pnlContent.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblAvatarInitials,
                this.lblTitle,
                this.lblAssignee,
                this.pnlChips,
                this.pnlProgressTrack,
                this.lblProgress
            });

            // ══════════════════════════════════════════════════════════════════
            // lblAvatarInitials — hình tròn chữ tắt assignee (28x28)
            // ══════════════════════════════════════════════════════════════════
            this.lblAvatarInitials.BackColor  = System.Drawing.Color.FromArgb(219, 234, 254); // blue-100
            this.lblAvatarInitials.ForeColor  = System.Drawing.Color.FromArgb(29, 78, 216);   // blue-700
            this.lblAvatarInitials.Font       = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblAvatarInitials.Location   = new System.Drawing.Point(8, 7);
            this.lblAvatarInitials.Name       = "lblAvatarInitials";
            this.lblAvatarInitials.Size       = new System.Drawing.Size(28, 28);
            this.lblAvatarInitials.TabIndex   = 0;
            this.lblAvatarInitials.Text       = "??";
            this.lblAvatarInitials.TextAlign  = System.Drawing.ContentAlignment.MiddleCenter;
            // Bo tròn kiểu avatar bằng Paint event
            this.lblAvatarInitials.Paint     += lblAvatarInitials_Paint;

            // ══════════════════════════════════════════════════════════════════
            // lblTitle — tiêu đề task (bold, tối đa 2 dòng)
            // Double-click → inline edit (xử lý trong ucTaskCard.cs)
            // ══════════════════════════════════════════════════════════════════
            this.lblTitle.AutoSize   = false;
            this.lblTitle.Cursor     = System.Windows.Forms.Cursors.IBeam; // gợi ý có thể edit
            this.lblTitle.Font       = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor  = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTitle.Location   = new System.Drawing.Point(44, 5);
            this.lblTitle.Name       = "lblTitle";
            this.lblTitle.Size       = new System.Drawing.Size(190, 36);
            this.lblTitle.TabIndex   = 1;
            this.lblTitle.Text       = "Task Title";
            this.lblTitle.TextAlign  = System.Drawing.ContentAlignment.TopLeft;
            // Double-click gắn trong ucTaskCard.cs (WireDoubleClickRecursive)

            // ══════════════════════════════════════════════════════════════════
            // lblAssignee — tên người thực hiện (muted)
            // ══════════════════════════════════════════════════════════════════
            this.lblAssignee.AutoSize  = false;
            this.lblAssignee.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAssignee.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblAssignee.Location  = new System.Drawing.Point(8, 44);
            this.lblAssignee.Name      = "lblAssignee";
            this.lblAssignee.Size      = new System.Drawing.Size(226, 18);
            this.lblAssignee.TabIndex  = 2;
            this.lblAssignee.Text      = "Chưa phân công";
            this.lblAssignee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ══════════════════════════════════════════════════════════════════
            // pnlChips — hàng chip: Status | Priority | DueDate
            // ══════════════════════════════════════════════════════════════════
            this.pnlChips.BackColor = System.Drawing.Color.Transparent;
            this.pnlChips.Location  = new System.Drawing.Point(6, 66);
            this.pnlChips.Name      = "pnlChips";
            this.pnlChips.Size      = new System.Drawing.Size(232, 22);
            this.pnlChips.TabIndex  = 3;
            this.pnlChips.Controls.AddRange(new System.Windows.Forms.Control[]
                { this.lblStatus, this.lblPriority, this.lblDueDate });

            // lblStatus chip
            this.lblStatus.AutoSize   = true;
            this.lblStatus.BackColor  = System.Drawing.Color.FromArgb(241, 245, 249);
            this.lblStatus.Font       = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblStatus.ForeColor  = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblStatus.Location   = new System.Drawing.Point(0, 2);
            this.lblStatus.Name       = "lblStatus";
            this.lblStatus.Padding    = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.lblStatus.TabIndex   = 0;
            this.lblStatus.Text       = "CREATED";
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // lblPriority chip
            this.lblPriority.AutoSize   = true;
            this.lblPriority.BackColor  = System.Drawing.Color.Transparent;
            this.lblPriority.Font       = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblPriority.ForeColor  = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblPriority.Location   = new System.Drawing.Point(80, 2);
            this.lblPriority.Name       = "lblPriority";
            this.lblPriority.TabIndex   = 1;
            this.lblPriority.Text       = "Medium";

            // lblDueDate chip (ẩn mặc định, Bind() sẽ set Visible)
            this.lblDueDate.AutoSize   = true;
            this.lblDueDate.BackColor  = System.Drawing.Color.FromArgb(241, 245, 249);
            this.lblDueDate.Font       = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblDueDate.ForeColor  = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblDueDate.Location   = new System.Drawing.Point(148, 2);
            this.lblDueDate.Name       = "lblDueDate";
            this.lblDueDate.Padding    = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.lblDueDate.TabIndex   = 2;
            this.lblDueDate.Text       = "⏰ --/--";
            this.lblDueDate.Visible    = false;
            this.lblDueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ══════════════════════════════════════════════════════════════════
            // pnlProgressTrack — thanh progress 4px (nền xám)
            // ══════════════════════════════════════════════════════════════════
            this.pnlProgressTrack.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlProgressTrack.Location  = new System.Drawing.Point(6, 98);
            this.pnlProgressTrack.Name      = "pnlProgressTrack";
            this.pnlProgressTrack.Size      = new System.Drawing.Size(200, 5);
            this.pnlProgressTrack.TabIndex  = 4;
            this.pnlProgressTrack.Controls.Add(this.pnlProgressFill);

            // pnlProgressFill — phần tô màu xanh bên trong track
            this.pnlProgressFill.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.pnlProgressFill.Dock      = System.Windows.Forms.DockStyle.Left;
            this.pnlProgressFill.Location  = new System.Drawing.Point(0, 0);
            this.pnlProgressFill.Name      = "pnlProgressFill";
            this.pnlProgressFill.Size      = new System.Drawing.Size(0, 5);   // Width set trong Bind()
            this.pnlProgressFill.TabIndex  = 0;

            // lblProgress — "60%"
            this.lblProgress.AutoSize  = false;
            this.lblProgress.Font      = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblProgress.Location  = new System.Drawing.Point(210, 93);
            this.lblProgress.Name      = "lblProgress";
            this.lblProgress.Size      = new System.Drawing.Size(30, 14);
            this.lblProgress.TabIndex  = 5;
            this.lblProgress.Text      = "0%";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ══════════════════════════════════════════════════════════════════
            // Context menu — 10 trạng thái workflow
            // ══════════════════════════════════════════════════════════════════
            this.cmsTaskActions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsTaskActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                miCreated, miAssigned, miInProgress, miFailed,
                miReview1, miReview2, miApproved, miInTest,
                miResolved, miClosed
            });
            this.cmsTaskActions.Name = "cmsTaskActions";
            this.cmsTaskActions.Size = new System.Drawing.Size(251, 264);

            SetMenuItem(miCreated,    "Chuyển sang Created");
            SetMenuItem(miAssigned,   "Chuyển sang Assigned");
            SetMenuItem(miInProgress, "Chuyển sang In-Progress");
            SetMenuItem(miFailed,     "Chuyển sang Failed");
            SetMenuItem(miReview1,    "Chuyển sang Review-1");
            SetMenuItem(miReview2,    "Chuyển sang Review-2");
            SetMenuItem(miApproved,   "Chuyển sang Approved");
            SetMenuItem(miInTest,     "Chuyển sang In-Test");
            SetMenuItem(miResolved,   "Chuyển sang Resolved");
            SetMenuItem(miClosed,     "Chuyển sang Closed");

            // ══════════════════════════════════════════════════════════════════
            // ucTaskCard — UserControl root
            // ══════════════════════════════════════════════════════════════════
            this.AutoScaleMode     = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor         = System.Drawing.Color.White;
            this.ContextMenuStrip  = this.cmsTaskActions;
            this.Controls.Add(this.pnlContainer);
            this.Name = "ucTaskCard";
            this.Size = new System.Drawing.Size(250, 140);

            this.pnlContainer.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlChips.ResumeLayout(false);
            this.pnlChips.PerformLayout();
            this.pnlProgressTrack.ResumeLayout(false);
            this.cmsTaskActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Paint: avatar tròn ─────────────────────────────────────────────
        private void lblAvatarInitials_Paint(object? sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (sender is not System.Windows.Forms.Label lbl) return;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var rect = new System.Drawing.Rectangle(0, 0, lbl.Width - 1, lbl.Height - 1);
            using var brush = new System.Drawing.SolidBrush(lbl.BackColor);
            e.Graphics.FillEllipse(brush, rect);

            using var sf = new System.Drawing.StringFormat
            {
                Alignment     = System.Drawing.StringAlignment.Center,
                LineAlignment = System.Drawing.StringAlignment.Center
            };
            e.Graphics.DrawString(lbl.Text, lbl.Font,
                new System.Drawing.SolidBrush(lbl.ForeColor), rect, sf);
        }

        // ── Helper ────────────────────────────────────────────────────────────
        private static void SetMenuItem(System.Windows.Forms.ToolStripMenuItem item, string text)
        {
            item.Name = item.Name;
            item.Size = new System.Drawing.Size(250, 26);
            item.Text = text;
        }

        #endregion

        // ── Control declarations ───────────────────────────────────────────────
        private System.Windows.Forms.Panel    pnlContainer;
        private System.Windows.Forms.Panel    pnlPriorityBar;
        private System.Windows.Forms.Panel    pnlContent;
        private System.Windows.Forms.Label    lblAvatarInitials;
        private System.Windows.Forms.Label    lblTitle;
        private System.Windows.Forms.Label    lblAssignee;
        private System.Windows.Forms.Panel    pnlChips;
        private System.Windows.Forms.Label    lblStatus;
        private System.Windows.Forms.Label    lblPriority;
        private System.Windows.Forms.Label    lblDueDate;
        private System.Windows.Forms.Panel    pnlProgressTrack;
        private System.Windows.Forms.Panel    pnlProgressFill;
        private System.Windows.Forms.Label    lblProgress;
        private System.Windows.Forms.ContextMenuStrip     cmsTaskActions;
        private System.Windows.Forms.ToolStripMenuItem    miCreated;
        private System.Windows.Forms.ToolStripMenuItem    miAssigned;
        private System.Windows.Forms.ToolStripMenuItem    miInProgress;
        private System.Windows.Forms.ToolStripMenuItem    miFailed;
        private System.Windows.Forms.ToolStripMenuItem    miReview1;
        private System.Windows.Forms.ToolStripMenuItem    miReview2;
        private System.Windows.Forms.ToolStripMenuItem    miApproved;
        private System.Windows.Forms.ToolStripMenuItem    miInTest;
        private System.Windows.Forms.ToolStripMenuItem    miResolved;
        private System.Windows.Forms.ToolStripMenuItem    miClosed;
    }
}
