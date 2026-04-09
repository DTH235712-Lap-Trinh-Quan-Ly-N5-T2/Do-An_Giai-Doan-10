namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Khai báo tất cả controls ──────────────────────────────────────
            pnlHeader = new Panel();
            pnlToolbar = new Panel();
            lblProjectFilter = new Label();
            cboProject = new ComboBox();
            pnlContent = new Panel();
            tabControlDashboard = new TabControl();

            // Tab 1 – Tổng Quan
            tabOverview = new TabPage();
            pnlOverviewBody = new TableLayoutPanel();
            pnlCardsLayout = new TableLayoutPanel();
            pnlPieChartArea = new Panel();
            pnlPieChartSplit = new TableLayoutPanel();
            pnlPieChart = new Panel();
            pnlLegend = new Panel();

            // Tab 2 – Tiến độ
            tabProgress = new TabPage();
            pnlProgressChart = new Panel();

            // Tab 3 – Ngân sách
            tabBudget = new TabPage();
            pnlBudgetWrapper = new TableLayoutPanel();
            pnlBudgetChart = new Panel();
            pnlBudgetLegend = new Panel();

            pnlToolbar.SuspendLayout();
            pnlContent.SuspendLayout();
            tabControlDashboard.SuspendLayout();
            tabOverview.SuspendLayout();
            pnlOverviewBody.SuspendLayout();
            pnlCardsLayout.SuspendLayout();
            pnlPieChartArea.SuspendLayout();
            pnlPieChartSplit.SuspendLayout();
            tabProgress.SuspendLayout();
            tabBudget.SuspendLayout();
            pnlBudgetWrapper.SuspendLayout();
            this.SuspendLayout();

            // ── pnlHeader ─────────────────────────────────────────────────────
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 68;
            pnlHeader.Name = "pnlHeader";

            // ── pnlToolbar ────────────────────────────────────────────────────
            pnlToolbar.Controls.Add(cboProject);
            pnlToolbar.Controls.Add(lblProjectFilter);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Height = 52;
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Padding = new Padding(16, 0, 16, 0);

            lblProjectFilter.AutoSize = true;
            lblProjectFilter.Location = new Point(20, 16);
            lblProjectFilter.Name = "lblProjectFilter";
            lblProjectFilter.Text = "Lọc dự án:";

            cboProject.FormattingEnabled = true;
            cboProject.Location = new Point(100, 12);
            cboProject.Name = "cboProject";
            cboProject.Size = new Size(320, 23);
            cboProject.TabIndex = 0;
            cboProject.DropDownStyle = ComboBoxStyle.DropDownList;

            // ── pnlContent ────────────────────────────────────────────────────
            pnlContent.Controls.Add(tabControlDashboard);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(16, 8, 16, 12);

            // ── tabControlDashboard ───────────────────────────────────────────
            tabControlDashboard.Controls.Add(tabOverview);
            tabControlDashboard.Controls.Add(tabProgress);
            tabControlDashboard.Controls.Add(tabBudget);
            tabControlDashboard.Dock = DockStyle.Fill;
            tabControlDashboard.Name = "tabControlDashboard";
            tabControlDashboard.SelectedIndex = 0;
            tabControlDashboard.Padding = new Point(16, 8);

            // ── tabOverview ───────────────────────────────────────────────────
            tabOverview.Controls.Add(pnlOverviewBody);
            tabOverview.Name = "tabOverview";
            tabOverview.Padding = new Padding(14);
            tabOverview.Text = "Tổng Quan";
            tabOverview.UseVisualStyleBackColor = true;

            // pnlOverviewBody: hàng 0 = stat cards (140px cố định), hàng 1 = chart (fill)
            pnlOverviewBody.ColumnCount = 1;
            pnlOverviewBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlOverviewBody.RowCount = 2;
            pnlOverviewBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            pnlOverviewBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlOverviewBody.Controls.Add(pnlCardsLayout, 0, 0);
            pnlOverviewBody.Controls.Add(pnlPieChartArea, 0, 1);
            pnlOverviewBody.Dock = DockStyle.Fill;
            pnlOverviewBody.Name = "pnlOverviewBody";
            pnlOverviewBody.Padding = new Padding(0);
            pnlOverviewBody.Margin = new Padding(0);

            // pnlCardsLayout: 4 cột đều nhau
            pnlCardsLayout.ColumnCount = 4;
            pnlCardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCardsLayout.RowCount = 1;
            pnlCardsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlCardsLayout.Dock = DockStyle.Fill;
            pnlCardsLayout.Margin = new Padding(0, 0, 0, 12);
            pnlCardsLayout.Name = "pnlCardsLayout";

            // pnlPieChartArea: card bao ngoài khu vực donut + legend
            pnlPieChartArea.Controls.Add(pnlPieChartSplit);
            pnlPieChartArea.Dock = DockStyle.Fill;
            pnlPieChartArea.Margin = new Padding(0);
            pnlPieChartArea.Name = "pnlPieChartArea";
            pnlPieChartArea.Padding = new Padding(20);
            pnlPieChartArea.BorderStyle = BorderStyle.None;

            // pnlPieChartSplit: 2 cột 50/50 — donut trái, legend phải
            pnlPieChartSplit.ColumnCount = 2;
            pnlPieChartSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlPieChartSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlPieChartSplit.RowCount = 1;
            pnlPieChartSplit.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlPieChartSplit.Controls.Add(pnlPieChart, 0, 0);
            pnlPieChartSplit.Controls.Add(pnlLegend, 1, 0);
            pnlPieChartSplit.Dock = DockStyle.Fill;
            pnlPieChartSplit.Margin = new Padding(0);
            pnlPieChartSplit.Name = "pnlPieChartSplit";
            pnlPieChartSplit.Padding = new Padding(0);

            // pnlPieChart: cột trái — vẽ donut qua sự kiện Paint
            pnlPieChart.Dock = DockStyle.Fill;
            pnlPieChart.Margin = new Padding(0);
            pnlPieChart.Name = "pnlPieChart";
            pnlPieChart.Paint += new PaintEventHandler(PnlPieChart_Paint);

            // pnlLegend: cột phải — vẽ chú giải qua sự kiện Paint
            pnlLegend.Dock = DockStyle.Fill;
            pnlLegend.Margin = new Padding(0);
            pnlLegend.Name = "pnlLegend";
            pnlLegend.Paint += new PaintEventHandler(PnlLegend_Paint);

            // ── tabProgress ───────────────────────────────────────────────────
            tabProgress.Controls.Add(pnlProgressChart);
            tabProgress.Name = "tabProgress";
            tabProgress.Padding = new Padding(14);
            tabProgress.Text = "Báo cáo tiến độ";
            tabProgress.UseVisualStyleBackColor = true;

            pnlProgressChart.Dock = DockStyle.Fill;
            pnlProgressChart.Margin = new Padding(0);
            pnlProgressChart.Name = "pnlProgressChart";
            pnlProgressChart.AutoScroll = true;
            pnlProgressChart.BorderStyle = BorderStyle.None;
            pnlProgressChart.Paint += new PaintEventHandler(PnlProgressChart_Paint);

            // ── tabBudget ─────────────────────────────────────────────────────
            // Cấu trúc: tabBudget → pnlBudgetWrapper (TableLayoutPanel 2 hàng)
            //   Hàng 0 (Fill)    : pnlBudgetChart  — biểu đồ cuộn dọc
            //   Hàng 1 (40px)    : pnlBudgetLegend — chú giải cố định phía dưới
            tabBudget.Controls.Add(pnlBudgetWrapper);
            tabBudget.Name = "tabBudget";
            tabBudget.Padding = new Padding(14);
            tabBudget.Text = "Ngân sách & Chi phí";
            tabBudget.UseVisualStyleBackColor = true;

            pnlBudgetWrapper.ColumnCount = 1;
            pnlBudgetWrapper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlBudgetWrapper.RowCount = 2;
            pnlBudgetWrapper.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlBudgetWrapper.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            pnlBudgetWrapper.Controls.Add(pnlBudgetChart, 0, 0);
            pnlBudgetWrapper.Controls.Add(pnlBudgetLegend, 0, 1);
            pnlBudgetWrapper.Dock = DockStyle.Fill;
            pnlBudgetWrapper.Margin = new Padding(0);
            pnlBudgetWrapper.Padding = new Padding(0);
            pnlBudgetWrapper.Name = "pnlBudgetWrapper";

            // Khu vực vẽ thanh ngang ngân sách — cuộn dọc khi nhiều dự án
            pnlBudgetChart.Dock = DockStyle.Fill;
            pnlBudgetChart.Margin = new Padding(0);
            pnlBudgetChart.Name = "pnlBudgetChart";
            pnlBudgetChart.AutoScroll = true;
            pnlBudgetChart.BorderStyle = BorderStyle.None;
            pnlBudgetChart.Paint += new PaintEventHandler(PnlBudgetChart_Paint);

            // Khu vực legend cố định, tách biệt hoàn toàn khỏi vùng vẽ biểu đồ
            pnlBudgetLegend.Dock = DockStyle.Fill;
            pnlBudgetLegend.Margin = new Padding(0);
            pnlBudgetLegend.Name = "pnlBudgetLegend";
            pnlBudgetLegend.BorderStyle = BorderStyle.None;

            // ── Form ──────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 800);
            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlToolbar);
            this.Controls.Add(pnlHeader);
            this.Name = "frmDashboard";
            this.Text = "Dashboard Thống Kê";
            this.Load += new EventHandler(frmDashboard_Load);

            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            pnlContent.ResumeLayout(false);
            tabControlDashboard.ResumeLayout(false);
            tabOverview.ResumeLayout(false);
            pnlOverviewBody.ResumeLayout(false);
            pnlCardsLayout.ResumeLayout(false);
            pnlPieChartArea.ResumeLayout(false);
            pnlPieChartSplit.ResumeLayout(false);
            tabProgress.ResumeLayout(false);
            tabBudget.ResumeLayout(false);
            pnlBudgetWrapper.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Khai báo fields ───────────────────────────────────────────────────
        private Panel pnlHeader;
        private Panel pnlToolbar;
        private Label lblProjectFilter;
        private ComboBox cboProject;
        private Panel pnlContent;
        private TabControl tabControlDashboard;
        private TabPage tabOverview;
        private TableLayoutPanel pnlOverviewBody;
        private TableLayoutPanel pnlCardsLayout;
        private Panel pnlPieChartArea;
        private TableLayoutPanel pnlPieChartSplit;
        private Panel pnlPieChart;
        private Panel pnlLegend;
        private TabPage tabProgress;
        private Panel pnlProgressChart;
        private TabPage tabBudget;
        private TableLayoutPanel pnlBudgetWrapper;
        private Panel pnlBudgetChart;
        private Panel pnlBudgetLegend;
    }
}