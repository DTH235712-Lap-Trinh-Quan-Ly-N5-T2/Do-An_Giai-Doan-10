// ============================================================
//  frmMyTasks.Designer.cs
//  TaskFlowManagement.WinForms.Forms
// ============================================================
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmMyTasks
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Khởi tạo toàn bộ control ──────────────────────────────────────
            panelHeader = new Panel();
            panelAccentLine = new Panel();
            lblHeader = new Label();

            panelFilter = new Panel();
            lblUser = new Label();
            btnRefresh = new Button();

            tabControl = new TabControl();
            tabMyTasks = new TabPage();
            tabReview = new TabPage();
            tabTesting = new TabPage();

            dgvMyTasks = new DataGridView();
            dgvReview = new DataGridView();
            dgvTesting = new DataGridView();

            panelStatus = new Panel();
            lblStatus = new Label();

            panelHeader.SuspendLayout();
            panelFilter.SuspendLayout();
            panelStatus.SuspendLayout();
            tabControl.SuspendLayout();
            tabMyTasks.SuspendLayout();
            tabReview.SuspendLayout();
            tabTesting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMyTasks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTesting).BeginInit();
            this.SuspendLayout();

            // ── panelHeader ────────────────────────────────────────────────────
            panelHeader.Controls.AddRange(new Control[] { lblHeader, panelAccentLine });
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 58;
            panelHeader.Name = "panelHeader";

            panelAccentLine.Dock = DockStyle.Bottom;
            panelAccentLine.Height = 4;
            panelAccentLine.Name = "panelAccentLine";

            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new Padding(18, 0, 0, 4);
            lblHeader.Text = "📋  Công việc của tôi";
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── panelFilter — dùng TableLayoutPanel bên trong để chống đè ────
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Height = 46;
            panelFilter.Name = "panelFilter";

            var tlpFilter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(10, 8, 10, 8),
                Name = "tlpFilter",
            };
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlpFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            lblUser.AutoSize = false;
            lblUser.Dock = DockStyle.Fill;
            lblUser.Name = "lblUser";
            lblUser.Text = "Đang tải...";
            lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            btnRefresh.Dock = DockStyle.Fill;
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Text = "🔄  Làm mới";
            btnRefresh.Click += btnRefresh_Click;

            tlpFilter.Controls.Add(lblUser, 0, 0);
            tlpFilter.Controls.Add(btnRefresh, 1, 0);
            panelFilter.Controls.Add(tlpFilter);

            // ── panelStatus ────────────────────────────────────────────────────
            panelStatus.Controls.Add(lblStatus);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Height = 28;
            panelStatus.Name = "panelStatus";

            lblStatus.AutoSize = false;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(12, 0, 0, 0);
            lblStatus.Text = "Sẵn sàng";
            lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── DataGridViews — chỉ khai báo tên; style áp dụng trong ApplyClientStyles() ──
            dgvMyTasks.Name = "dgvMyTasks";
            dgvMyTasks.Dock = DockStyle.Fill;
            dgvMyTasks.CellDoubleClick += dgv_CellDoubleClick;

            dgvReview.Name = "dgvReview";
            dgvReview.Dock = DockStyle.Fill;
            dgvReview.CellDoubleClick += dgv_CellDoubleClick;

            dgvTesting.Name = "dgvTesting";
            dgvTesting.Dock = DockStyle.Fill;
            dgvTesting.CellDoubleClick += dgv_CellDoubleClick;

            // ── Tab Pages ──────────────────────────────────────────────────────
            tabMyTasks.Controls.Add(dgvMyTasks);
            tabMyTasks.Name = "tabMyTasks";
            tabMyTasks.Padding = new Padding(3);
            tabMyTasks.Text = "📋  Được giao (0)";

            tabReview.Controls.Add(dgvReview);
            tabReview.Name = "tabReview";
            tabReview.Padding = new Padding(3);
            tabReview.Text = "🔍  Review (0)";

            tabTesting.Controls.Add(dgvTesting);
            tabTesting.Name = "tabTesting";
            tabTesting.Padding = new Padding(3);
            tabTesting.Text = "🧪  Testing (0)";

            // ── TabControl ─────────────────────────────────────────────────────
            tabControl.Dock = DockStyle.Fill;
            tabControl.ItemSize = new System.Drawing.Size(175, 32);
            tabControl.Name = "tabControl";
            tabControl.TabPages.AddRange(new TabPage[] { tabMyTasks, tabReview, tabTesting });

            // ── Form ───────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 660);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "frmMyTasks";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "📋  Công việc của tôi";

            // Thứ tự Add: Fill → Bottom → Top
            this.Controls.Add(tabControl);
            this.Controls.Add(panelStatus);
            this.Controls.Add(panelFilter);
            this.Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            panelStatus.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabMyTasks.ResumeLayout(false);
            tabReview.ResumeLayout(false);
            tabTesting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMyTasks).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReview).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTesting).EndInit();
            this.ResumeLayout(false);
        }

        #region Control declarations

        private Panel panelHeader;
        private Panel panelAccentLine;
        private Label lblHeader;
        private Panel panelFilter;
        private Label lblUser;
        private Button btnRefresh;
        private TabControl tabControl;
        private TabPage tabMyTasks;
        private TabPage tabReview;
        private TabPage tabTesting;
        private DataGridView dgvMyTasks;
        private DataGridView dgvReview;
        private DataGridView dgvTesting;
        private Panel panelStatus;
        private Label lblStatus;

        #endregion
    }
}