using System.Drawing.Drawing2D;
using TaskFlowManagement.Core.DTOs;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmDashboard : BaseForm
    {
        private readonly ITaskService _taskService;
        private readonly IProjectService _projectService;
        private readonly IExpenseService _expenseService;

        private EventHandler? _taskDataChangedHandler;
        private DashboardStatsDto? _currentOverview = null;
        private List<ProgressReportDto> _currentProgress = new();
        private List<BudgetReportDto> _currentBudget = new();
        private ProjectBudgetSummaryDto? _currentBudgetSummary = null;

        private int _hoverProgressIndex = -1;
        private EventHandler? _taskDataChangedHandler;

        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmDashboard()
        {
            InitializeComponent();
        }

        public frmDashboard(
            ITaskService taskService,
            IProjectService projectService,
            IExpenseService expenseService)
        {
            _taskService = taskService;
            _projectService = projectService;
            _expenseService = expenseService;

            InitializeComponent();
            ApplyClientStyles();
            SetupUI();
        }

        // ══════════════════════════════════════════════════════════════════
        // STYLES — Toàn bộ màu sắc, font, border lấy từ UIHelper
        // ══════════════════════════════════════════════════════════════════

        private void ApplyClientStyles()
        {
            this.BackColor = UIHelper.ColorBackground;
            pnlHeader.BackColor = UIHelper.ColorBackground;
            pnlToolbar.BackColor = UIHelper.ColorBackground;
            pnlContent.BackColor = UIHelper.ColorBackground;

            lblProjectFilter.Font = UIHelper.FontLabel;
            lblProjectFilter.ForeColor = UIHelper.ColorTextPrimary;
            UIHelper.StyleFilterCombo(cboProject);

            tabControlDashboard.Font = UIHelper.FontBase;
            foreach (TabPage tab in tabControlDashboard.TabPages)
                tab.BackColor = UIHelper.ColorBackground;

            // Vùng nội dung từng tab
            pnlCardsLayout.BackColor = UIHelper.ColorBackground;

            pnlPieChartArea.BackColor = UIHelper.ColorSurface;
            pnlPieChartArea.BorderStyle = BorderStyle.None;
            pnlPieChart.BackColor = UIHelper.ColorSurface;
            pnlLegend.BackColor = UIHelper.ColorSurface;

            // pnlProgressChart: nền trắng, cuộn dọc khi nhiều dự án
            pnlProgressChart.BackColor = UIHelper.ColorSurface;
            pnlProgressChart.BorderStyle = BorderStyle.None;
            pnlProgressChart.AutoScroll = true;

            // pnlBudgetChart: khu vực vẽ thanh ngang — không gộp chung với legend
            pnlBudgetChart.BackColor = UIHelper.ColorSurface;
            pnlBudgetChart.BorderStyle = BorderStyle.None;
            pnlBudgetChart.AutoScroll = true;

            // pnlBudgetLegend: panel legend cố định phía dưới biểu đồ ngân sách
            pnlBudgetLegend.BackColor = UIHelper.ColorSurface;
            pnlBudgetLegend.BorderStyle = BorderStyle.None;
        }

        // ══════════════════════════════════════════════════════════════════
        // SETUP
        // ══════════════════════════════════════════════════════════════════

        private void SetupUI()
        {
            BuildHeader();
            WireEvents();
            EnableDoubleBuffer(pnlPieChart);
            EnableDoubleBuffer(pnlLegend);
            EnableDoubleBuffer(pnlProgressChart);
            EnableDoubleBuffer(pnlBudgetChart);
            EnableDoubleBuffer(pnlBudgetLegend);

            // Developer không được xem tab Ngân sách & Chi phí
            if (!AppSession.IsManager && !AppSession.IsAdmin)
                tabControlDashboard.TabPages.Remove(tabBudget);
        }

        private void BuildHeader()
        {
            pnlHeader.Controls.Clear();

            var header = UIHelper.CreateHeaderPanel(
                "BÁO CÁO TỔNG QUAN",
                "Cập nhật lúc: " + DateTime.Now.ToString("HH:mm dd/MM/yyyy"));

            var pnlAccent = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 3,
                BackColor = UIHelper.ColorPrimary
            };
            header.Controls.Add(pnlAccent);
            pnlHeader.Controls.Add(header);

            if (AppSession.IsAdmin || AppSession.IsManager)
            {
                var btnReport = new Button();
                UIHelper.StyleButton(btnReport, UIHelper.ButtonVariant.Secondary);
                btnReport.Text = "📊 Tải báo cáo tổng kết";
                btnReport.Name = "btnHeaderReport";
                btnReport.Size = new Size(196, 34);
                btnReport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                btnReport.Location = new Point(pnlHeader.Width - 212, 17);
                btnReport.FlatAppearance.BorderSize = 1;
                btnReport.FlatAppearance.BorderColor = UIHelper.ColorBorderLight;
                btnReport.Click += async (s, e) =>
                {
                    btnReport.Enabled = false;
                    try { await OpenDashboardReportAsync(); }
                    finally { if (!btnReport.IsDisposed) btnReport.Enabled = true; }
                };
                pnlHeader.Controls.Add(btnReport);
                btnReport.BringToFront();
            }
        }

        private void WireEvents()
        {
            cboProject.SelectedIndexChanged += OnProjectChanged;

            pnlProgressChart.MouseMove += PnlProgressChart_MouseMove;
            pnlProgressChart.MouseLeave += OnChartMouseLeave;

            _taskDataChangedHandler = async (s, e) =>
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.BeginInvoke(new Action(async () => await LoadDashboardDataAsync()));
                }
            };
            _taskService.TaskDataChanged += _taskDataChangedHandler;
            this.FormClosing += (s, e) => _taskService.TaskDataChanged -= _taskDataChangedHandler;

            pnlBudgetLegend.Paint += PnlBudgetLegend_Paint;
        }

        private async void OnProjectChanged(object? sender, EventArgs e)
            => await LoadDashboardDataAsync();

        private void OnChartMouseLeave(object? sender, EventArgs e)
        {
            _hoverProgressIndex = -1;
            pnlProgressChart.Invalidate();
        }

        private async void OnTaskDataChanged(object? sender, EventArgs e)
        {
            if (this.IsHandleCreated && !this.IsDisposed)
                this.BeginInvoke(new Action(async () => await LoadDashboardDataAsync()));
        }

        private void EnableDoubleBuffer(Control ctrl)
        {
            var method = typeof(Control).GetMethod(
                "SetStyle",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            method?.Invoke(ctrl, new object[]
            {
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint  |
                ControlStyles.UserPaint,
                true
            });
        }

        // ══════════════════════════════════════════════════════════════════
        // EVENTS
        // ══════════════════════════════════════════════════════════════════

        private async void frmDashboard_Load(object sender, EventArgs e)
        {
            await LoadProjectsAsync();
            await LoadDashboardDataAsync();
        }

        private void PnlProgressChart_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_currentProgress == null || !_currentProgress.Any()) return;

            int adjustedY = e.Y - pnlProgressChart.AutoScrollPosition.Y;
            int startY = 80;
            int newHoverIndex = -1;

            for (int i = 0; i < _currentProgress.Count; i++)
            {
                if (adjustedY >= startY && adjustedY < startY + 60)
                {
                    newHoverIndex = i;
                    break;
                }
                startY += 60;
            }

            if (_hoverProgressIndex != newHoverIndex)
            {
                _hoverProgressIndex = newHoverIndex;
                pnlProgressChart.Invalidate();
            }
        }

        public void SelectTab(int tabIndex)
        {
            if (tabIndex >= 0 && tabIndex < tabControlDashboard.TabPages.Count)
            {
                tabControlDashboard.SelectedIndex = tabIndex;
                return;
            }

            if (tabIndex == 2 && tabControlDashboard.TabPages.Count < 3)
            {
                MessageBox.Show(
                    "Bạn không có quyền truy cập tab Ngân sách (chỉ dành cho Admin/Manager).",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // DATA LOADING
        // ══════════════════════════════════════════════════════════════════

        private async Task LoadProjectsAsync()
        {
            var projects = await _projectService.GetProjectsForUserAsync(
                AppSession.UserId, AppSession.IsManager);

            cboProject.Items.Clear();
            cboProject.Items.Add(new { Id = 0, Name = "-- Toàn bộ hệ thống --" });
            foreach (var p in projects)
                cboProject.Items.Add(new { Id = p.Id, Name = p.Name });

            cboProject.DisplayMember = "Name";
            cboProject.ValueMember = "Id";
            cboProject.SelectedIndex = 0;
            cboProject.AdjustDropDownWidth();

            if (!AppSession.IsManager && !AppSession.IsAdmin)
                cboProject.Enabled = false;
        }

        private async Task LoadDashboardDataAsync()
        {
            if (cboProject.SelectedItem == null) return;

            var selectedId = (int)((dynamic)cboProject.SelectedItem).Id;
            int? projectId = selectedId == 0 ? null : selectedId;

            try
            {
                var task1 = _taskService.GetDashboardStatsAsync(projectId);
                var task2 = _taskService.GetProgressReportAsync(projectId);

                Task<List<BudgetReportDto>>? task3 = null;
                Task<ProjectBudgetSummaryDto?>? task4 = null;

                if (AppSession.IsManager || AppSession.IsAdmin)
                {
                    task3 = _taskService.GetBudgetReportAsync(projectId);
                    if (projectId.HasValue)
                        task4 = _expenseService.GetProjectBudgetSummaryAsync(projectId.Value);
                }

                _currentOverview = await task1;
                _currentProgress = await task2;
                if (task3 != null) _currentBudget = await task3;
                if (task4 != null) _currentBudgetSummary = await task4;
                else _currentBudgetSummary = null;

                RenderStatCards(_currentOverview);
                pnlPieChart.Invalidate();
                pnlLegend.Invalidate();
                pnlProgressChart.Invalidate();
                if (task3 != null)
                {
                    pnlBudgetChart.Invalidate();
                    pnlBudgetLegend.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // TAB 1 – STAT CARDS
        // Dùng TableLayoutPanel nội tại mỗi card để căn chỉnh icon/text chính xác
        // ══════════════════════════════════════════════════════════════════

        private void RenderStatCards(DashboardStatsDto stats)
        {
            // Xóa toàn bộ card cũ trước khi render lại
            for (int col = 0; col < pnlCardsLayout.ColumnCount; col++)
            {
                var existing = pnlCardsLayout.GetControlFromPosition(col, 0);
                if (existing == null) continue;
                pnlCardsLayout.Controls.Remove(existing);
                existing.Dispose();
            }

            Panel card3 = _currentBudgetSummary != null
                ? CreateStatCard(
                    "Ngân Sách Còn Lại",
                    _currentBudgetSummary.Remaining.ToString("N0") + " ₫",
                    "💰",
                    _currentBudgetSummary.IsOverBudget ? UIHelper.ColorDanger : UIHelper.ColorSuccess,
                    "BUDGET", isLast: true)
                : CreateStatCard("Tới Hạn (7 ngày)", stats.DueSoonTasks.ToString(),
                    "⚠️", UIHelper.ColorWarning, "DUE_SOON", isLast: true);

            pnlCardsLayout.Controls.Add(
                CreateStatCard("Tổng Công Việc", stats.TotalTasks.ToString(),
                    "📁", UIHelper.ColorPrimary, "ALL", isLast: false), 0, 0);
            pnlCardsLayout.Controls.Add(
                CreateStatCard("Đã Hoàn Thành", stats.CompletedTasks.ToString(),
                    "✅", UIHelper.ColorSuccess, "COMPLETED", isLast: false), 1, 0);
            pnlCardsLayout.Controls.Add(
                CreateStatCard("Sự Cố (Quá Hạn)", stats.OverdueTasks.ToString(),
                    "🚩", UIHelper.ColorDanger, "OVERDUE", isLast: false), 2, 0);
            pnlCardsLayout.Controls.Add(card3, 3, 0);

            AttachCardClickEvents();
        }

        private void AttachCardClickEvents()
        {
            foreach (Control ctrl in pnlCardsLayout.Controls)
            {
                if (ctrl is Panel card)
                {
                    card.Cursor = Cursors.Hand;
                    AttachClickRecursive(card, StatCard_Click);
                }
            }
        }

        private void AttachClickRecursive(Control parent, EventHandler handler)
        {
            parent.Click += handler;
            foreach (Control child in parent.Controls)
                AttachClickRecursive(child, handler);
        }

        private void StatCard_Click(object? sender, EventArgs e)
        {
            string? filterType = null;
            Control? current = sender as Control;

            while (current != null && current != pnlCardsLayout)
            {
                if (current.Tag != null) { filterType = current.Tag.ToString(); break; }
                current = current.Parent;
            }

            if (string.IsNullOrEmpty(filterType) || filterType == "BUDGET") return;

            int? projectId = null;
            if (cboProject.SelectedItem != null)
            {
                var selId = (int)((dynamic)cboProject.SelectedItem).Id;
                if (selId > 0) projectId = selId;
            }

            if (this.MdiParent is frmMain mainForm)
                mainForm.OpenTaskListWithFilter(filterType, projectId);
        }

        /// <summary>
        /// Thẻ thống kê sử dụng TableLayoutPanel nội tại để phân vùng:
        /// cột trái (75%) chứa tiêu đề + số liệu, cột phải (25%) chứa icon emoji.
        /// Thanh accent 5px bên trái thay thế border cứng — nhẹ hơn về thị giác.
        /// </summary>
        private Panel CreateStatCard(string title, string value, string icon,
            Color accentColor, string tag, bool isLast)
        {
            var pnlOuter = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = isLast ? new Padding(0) : new Padding(0, 0, 14, 0),
                BackColor = UIHelper.ColorSurface,
                BorderStyle = BorderStyle.None,
                Tag = tag,
                Padding = new Padding(0)
            };

            // Thanh accent bên trái
            pnlOuter.Controls.Add(new Panel
            {
                BackColor = accentColor,
                Dock = DockStyle.Left,
                Width = 5
            });

            // TableLayoutPanel nội tại: 2 cột (text | icon), fill toàn bộ card
            var tbl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(14, 12, 10, 12),
                BackColor = UIHelper.ColorSurface
            };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Panel trái: tiêu đề + giá trị xếp dọc
            var pnlText = new Panel { Dock = DockStyle.Fill, BackColor = UIHelper.ColorSurface };
            var lblTitle = new Label
            {
                Text = title,
                ForeColor = UIHelper.ColorTextSecondary,
                Font = UIHelper.FontBold,
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 22,
                Padding = new Padding(0),
                TextAlign = ContentAlignment.MiddleLeft
            };
            var lblValue = new Label
            {
                Text = value,
                ForeColor = UIHelper.ColorTextPrimary,
                Font = UIHelper.FontHeaderLarge,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlText.Controls.Add(lblValue);
            pnlText.Controls.Add(lblTitle);

            // Panel phải: icon emoji căn giữa
            var pnlIcon = new Panel { Dock = DockStyle.Fill, BackColor = UIHelper.ColorSurface };
            var lblIcon = new Label
            {
                Text = icon,
                ForeColor = accentColor,
                Font = new Font("Segoe UI Emoji", 22F),
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlIcon.Controls.Add(lblIcon);

            tbl.Controls.Add(pnlText, 0, 0);
            tbl.Controls.Add(pnlIcon, 1, 0);
            pnlOuter.Controls.Add(tbl);

            // Viền bo góc mờ nhẹ thay thế BorderStyle cứng
            pnlOuter.Paint += (s, pe) =>
            {
                pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, pnlOuter.Width - 1, pnlOuter.Height - 1);
                using var path = GetRoundedRect(rect, 10);
                using var pen = new Pen(UIHelper.ColorBorderLight);
                pe.Graphics.DrawPath(pen, path);
            };

            return pnlOuter;
        }

        // ══════════════════════════════════════════════════════════════════
        // TAB 1 – DONUT CHART
        // ══════════════════════════════════════════════════════════════════

        private void PnlPieChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            DrawPanelTitle(g, pnlPieChart.Width,
                "Thống kê trạng thái Công việc (" + (_currentOverview?.TotalTasks ?? 0) + " tasks)");

            if (_currentOverview == null || !_currentOverview.StatusSummaries.Any(x => x.Count > 0))
            {
                DrawNoData(g, pnlPieChart.Width, pnlPieChart.Height);
                return;
            }

            int totalTasks = _currentOverview.StatusSummaries.Sum(s => s.Count);
            if (totalTasks == 0) return;

            int padding = 32;
            int chartSize = Math.Min(pnlPieChart.Width - padding * 2, pnlPieChart.Height - 90);
            if (chartSize < 50) chartSize = 50;

            int originX = (pnlPieChart.Width - chartSize) / 2;
            int originY = 72 + (pnlPieChart.Height - 72 - chartSize) / 2;
            if (originY < 72) originY = 72;

            var pieRect = new Rectangle(originX, originY, chartSize, chartSize);
            float startAngle = -90f;

            foreach (var status in _currentOverview.StatusSummaries)
            {
                if (status.Count == 0) continue;
                float sweep = status.Count / (float)totalTasks * 360f;
                using var brush = new SolidBrush(ColorTranslator.FromHtml(status.ColorHex));
                g.FillPie(brush, pieRect, startAngle, sweep);
                startAngle += sweep;
            }

            // Lỗ 62% tạo hiệu ứng donut cân bằng thẩm mỹ
            int holeSize = (int)(chartSize * 0.62);
            var holeRect = new Rectangle(
                pieRect.X + (chartSize - holeSize) / 2,
                pieRect.Y + (chartSize - holeSize) / 2,
                holeSize, holeSize);

            using (var holeBrush = new SolidBrush(UIHelper.ColorSurface))
                g.FillEllipse(holeBrush, holeRect);

            using var centerFont = new Font("Segoe UI", 18F, FontStyle.Bold);
            using var centerBrush = new SolidBrush(UIHelper.ColorTextPrimary);
            string centerText = totalTasks.ToString();
            SizeF textSz = g.MeasureString(centerText, centerFont);
            g.DrawString(centerText, centerFont, centerBrush,
                pieRect.X + (chartSize - textSz.Width) / 2,
                pieRect.Y + (chartSize - textSz.Height) / 2);
        }

        private void PnlLegend_Paint(object sender, PaintEventArgs e)
        {
            if (_currentOverview == null) return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            var items = _currentOverview.StatusSummaries.Where(s => s.Count > 0).ToList();
            if (!items.Any()) return;

            const int itemHeight = 34;
            int totalH = items.Count * itemHeight;
            int startY = (pnlLegend.Height - totalH) / 2;
            if (startY < 20) startY = 20;
            const int lx = 24;

            foreach (var status in items)
            {
                var swatchRect = new Rectangle(lx, startY + 2, 14, 14);
                using var swatchBrush = new SolidBrush(ColorTranslator.FromHtml(status.ColorHex));
                using var swatchPath = GetRoundedRect(swatchRect, 3);
                g.FillPath(swatchBrush, swatchPath);

                using var textBrush = new SolidBrush(UIHelper.ColorTextPrimary);
                g.DrawString($"{status.StatusName}   ({status.Count})",
                    UIHelper.FontBase, textBrush, lx + 24, startY);

                startY += itemHeight;
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // TAB 2 – PROGRESS BARS
        // labelColWidth tăng lên 260px để tên dự án dài không bị cắt sớm
        // ══════════════════════════════════════════════════════════════════

        private void PnlProgressChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TranslateTransform(
                pnlProgressChart.AutoScrollPosition.X,
                pnlProgressChart.AutoScrollPosition.Y);

            int panelWidth = Math.Max(pnlProgressChart.Width, pnlProgressChart.AutoScrollMinSize.Width);
            DrawPanelTitle(g, panelWidth, "Tiến độ Thực tế Dự án (%)");

            if (_currentProgress == null || !_currentProgress.Any())
            {
                DrawNoData(g, pnlProgressChart.Width, pnlProgressChart.Height);
                return;
            }

            const int marginLeft = 30;
            const int nameWidth = 260;   // mở rộng cột tên để hiển thị đủ ký tự
            const int barHeight = 26;
            int maxBarWidth = pnlProgressChart.ClientSize.Width - marginLeft - nameWidth - 80;
            if (maxBarWidth < 100) maxBarWidth = 100;

            int startY = 80;
            int rowIndex = 0;

            foreach (var proj in _currentProgress)
            {
                if (rowIndex == _hoverProgressIndex)
                {
                    using var hoverBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0));
                    g.FillRectangle(hoverBrush,
                        marginLeft - 12, startY - 6,
                        pnlProgressChart.ClientSize.Width - marginLeft * 2 + 24, 62);
                }

                // Cắt tên dài hơn 34 ký tự để vừa cột 260px
                string displayName = proj.ProjectName.Length > 34
                    ? proj.ProjectName[..31] + "…"
                    : proj.ProjectName;

                using (var nameBrush = new SolidBrush(UIHelper.ColorTextPrimary))
                    g.DrawString(displayName, UIHelper.FontGridHeader, nameBrush,
                        marginLeft + 12, startY);

                using (var muteBrush = new SolidBrush(UIHelper.ColorMuted))
                    g.DrawString($"{proj.CompletedTasks}/{proj.TotalTasks} tasks",
                        UIHelper.FontSmall, muteBrush, marginLeft + 12, startY + 20);

                int barX = marginLeft + nameWidth;
                var bgRect = new Rectangle(barX, startY, maxBarWidth, barHeight);
                using (var bgBrush = new SolidBrush(UIHelper.ColorBorderLight))
                    g.FillRectangle(bgBrush, bgRect);

                Color barColor = UIHelper.ColorPrimary;
                if (proj.AvgProgress >= 100) barColor = UIHelper.ColorSuccess;
                else if (proj.Status is "OnHold" or "Delayed") barColor = UIHelper.ColorWarning;

                int fillWidth = (int)(maxBarWidth * (double)(proj.AvgProgress / 100m));
                if (fillWidth > 0)
                {
                    var fillRect = new Rectangle(barX, startY, fillWidth, barHeight);
                    using var fillBrush = new LinearGradientBrush(
                        fillRect,
                        ControlPaint.Light(barColor, 0.4f),
                        barColor,
                        LinearGradientMode.Horizontal);
                    g.FillRectangle(fillBrush, fillRect);
                }

                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                string pctText = $"{Math.Round(proj.AvgProgress, 1)}%";
                SizeF pctSize = g.MeasureString(pctText, UIHelper.FontBase);
                using (var pctBrush = new SolidBrush(UIHelper.ColorTextPrimary))
                    g.DrawString(pctText, UIHelper.FontBase, pctBrush,
                        barX + fillWidth + 6,
                        startY + (barHeight - pctSize.Height) / 2);

                using (var divPen = new Pen(UIHelper.ColorBorderLight))
                    g.DrawLine(divPen,
                        marginLeft, startY + barHeight + 16,
                        pnlProgressChart.ClientSize.Width - marginLeft, startY + barHeight + 16);

                startY += 60;
                rowIndex++;
            }

            int totalHeight = startY + 20;
            if (pnlProgressChart.AutoScrollMinSize.Height != totalHeight)
                pnlProgressChart.AutoScrollMinSize = new Size(0, totalHeight);
        }

        // ══════════════════════════════════════════════════════════════════
        // TAB 3 – BUDGET HORIZONTAL BAR CHART
        // pnlBudgetChart vẽ biểu đồ cuộn; pnlBudgetLegend cố định dưới cùng.
        // labelColWidth = 260px để tên dự án dài hiển thị đủ chữ.
        // ══════════════════════════════════════════════════════════════════

        private void PnlBudgetChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Bù trừ cuộn dọc để toàn bộ nội dung di chuyển cùng scrollbar
            g.TranslateTransform(0, pnlBudgetChart.AutoScrollPosition.Y);

            DrawPanelTitle(g, pnlBudgetChart.Width,
                "Thống kê Ngân sách Thực tế (Budget vs Expenses)");

            if (_currentBudget == null || !_currentBudget.Any())
            {
                DrawNoData(g, pnlBudgetChart.Width, pnlBudgetChart.Height);
                return;
            }

            const int paddingTop = 72;
            const int paddingRight = 20;
            const int labelColWidth = 260;  // cột tên — mở rộng để đọc được tên dự án dài
            const int barPairGap = 6;
            const int rowGap = 16;
            const int barThickness = 22;
            const int pairHeight = barThickness * 2 + barPairGap;
            int rowStride = pairHeight + rowGap;

            int barMaxWidth = pnlBudgetChart.ClientSize.Width
                              - labelColWidth - paddingRight - 90;
            if (barMaxWidth < 80) barMaxWidth = 80;

            decimal maxVal = _currentBudget.Max(b => Math.Max(b.Budget, b.TotalExpense));
            if (maxVal == 0) maxVal = 1;

            int totalContentHeight = paddingTop + _currentBudget.Count * rowStride + 24;
            if (pnlBudgetChart.AutoScrollMinSize.Height != totalContentHeight)
                pnlBudgetChart.AutoScrollMinSize = new Size(0, totalContentHeight);

            // Trục Y dọc và đường lưới dọc
            int axisX = labelColWidth + 10;
            int axisTop = paddingTop;
            int axisBottom = paddingTop + _currentBudget.Count * rowStride - rowGap;

            using (var gridPen = new Pen(UIHelper.ColorBorderLight))
            using (var lblBrush = new SolidBrush(UIHelper.ColorMuted))
            {
                g.DrawLine(gridPen, axisX, axisTop, axisX, axisBottom);

                var sf = new StringFormat { Alignment = StringAlignment.Center };
                for (int i = 0; i <= 5; i++)
                {
                    int gx = axisX + (int)(barMaxWidth * i / 5.0);
                    decimal val = maxVal * i / 5;
                    string lbl = val >= 1_000_000_000
                        ? (val / 1_000_000_000).ToString("0.#B")
                        : val >= 1_000_000
                            ? (val / 1_000_000).ToString("0.#M")
                            : (val / 1_000).ToString("0.#k");

                    if (i > 0)
                        g.DrawLine(gridPen, gx, axisTop, gx, axisBottom);

                    g.DrawString(lbl, UIHelper.FontSmall, lblBrush,
                        new RectangleF(gx - 24, axisTop - 18, 48, 16), sf);
                }
            }

            int currentY = paddingTop;

            foreach (var item in _currentBudget)
            {
                // Cắt tên dài hơn 32 ký tự để vừa cột 260px
                string displayName = item.ProjectName.Length > 32
                    ? item.ProjectName[..29] + "…"
                    : item.ProjectName;

                var nameRect = new RectangleF(4, currentY, labelColWidth - 8, pairHeight);
                var nameSf = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };
                using (var nameBrush = new SolidBrush(UIHelper.ColorTextPrimary))
                    g.DrawString(displayName, UIHelper.FontGridHeader, nameBrush, nameRect, nameSf);

                // Thanh Budget
                int wBudget = (int)(barMaxWidth * (double)(item.Budget / maxVal));
                if (wBudget > 0)
                {
                    var rBudget = new Rectangle(axisX + 1, currentY, wBudget, barThickness);
                    using var bBrush = new LinearGradientBrush(
                        rBudget,
                        ControlPaint.Light(UIHelper.ColorAccent, 0.4f),
                        UIHelper.ColorAccent,
                        LinearGradientMode.Horizontal);
                    g.FillRectangle(bBrush, rBudget);
                }

                string budgetLbl = item.Budget >= 1_000_000
                    ? (item.Budget / 1_000_000).ToString("0.#M ₫")
                    : item.Budget.ToString("N0") + " ₫";
                using (var valBrush = new SolidBrush(UIHelper.ColorMuted))
                    g.DrawString(budgetLbl, UIHelper.FontSmall, valBrush,
                        axisX + wBudget + 6,
                        currentY + (barThickness - UIHelper.FontSmall.Height) / 2);

                // Thanh Expense — màu đỏ khi vượt quỹ, xanh khi an toàn
                int yExpense = currentY + barThickness + barPairGap;
                int wExpense = (int)(barMaxWidth * (double)(item.TotalExpense / maxVal));
                Color expColor = item.TotalExpense > item.Budget
                    ? UIHelper.ColorDanger
                    : UIHelper.ColorSuccess;

                if (wExpense > 0)
                {
                    var rExpense = new Rectangle(axisX + 1, yExpense, wExpense, barThickness);
                    using var eBrush = new LinearGradientBrush(
                        rExpense,
                        ControlPaint.Light(expColor, 0.4f),
                        expColor,
                        LinearGradientMode.Horizontal);
                    g.FillRectangle(eBrush, rExpense);
                }

                string expLbl = item.TotalExpense >= 1_000_000
                    ? (item.TotalExpense / 1_000_000).ToString("0.#M ₫")
                    : item.TotalExpense.ToString("N0") + " ₫";
                string pctLbl = $"({item.UsagePercentage}%)";

                using (var expBrush = new SolidBrush(UIHelper.ColorMuted))
                {
                    g.DrawString(expLbl, UIHelper.FontSmall, expBrush,
                        axisX + wExpense + 6,
                        yExpense + (barThickness - UIHelper.FontSmall.Height) / 2);

                    SizeF expSz = g.MeasureString(expLbl, UIHelper.FontSmall);
                    using var pctBrush = new SolidBrush(expColor);
                    g.DrawString(pctLbl, UIHelper.FontSmall, pctBrush,
                        axisX + wExpense + 6 + expSz.Width,
                        yExpense + (barThickness - UIHelper.FontSmall.Height) / 2);
                }

                using (var divPen = new Pen(UIHelper.ColorBorderLight))
                    g.DrawLine(divPen,
                        4, currentY + pairHeight + rowGap / 2,
                        pnlBudgetChart.ClientSize.Width - paddingRight,
                        currentY + pairHeight + rowGap / 2);

                currentY += rowStride;
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // TAB 3 – BUDGET LEGEND (panel riêng biệt, Dock = Bottom)
        // Tách khỏi khu vực vẽ biểu đồ để không bị đè lên thanh ngang
        // ══════════════════════════════════════════════════════════════════

        private void PnlBudgetLegend_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            const int swatchSize = 12;
            const int itemGap = 28;
            int lx = 20;
            int ly = (pnlBudgetLegend.Height - UIHelper.FontBase.Height) / 2;
            if (ly < 4) ly = 4;

            void DrawItem(Color color, string label)
            {
                var swatchRect = new Rectangle(lx, ly + 2, swatchSize, swatchSize);
                using var brush = new SolidBrush(color);
                using var path = GetRoundedRect(swatchRect, 3);
                g.FillPath(brush, path);

                using var textBrush = new SolidBrush(UIHelper.ColorTextPrimary);
                SizeF sz = g.MeasureString(label, UIHelper.FontBase);
                g.DrawString(label, UIHelper.FontBase, textBrush, lx + swatchSize + 6, ly);
                lx += (int)sz.Width + swatchSize + itemGap;
            }

            // Đường kẻ ngăn cách legend với biểu đồ phía trên
            using (var divPen = new Pen(UIHelper.ColorBorderLight))
                g.DrawLine(divPen, 0, 0, pnlBudgetLegend.Width, 0);

            DrawItem(UIHelper.ColorAccent, "Ngân sách (Budget)");
            DrawItem(UIHelper.ColorSuccess, "Chi phí thực tế — An toàn");
            DrawItem(UIHelper.ColorDanger, "Chi phí thực tế — Vượt quỹ");
        }

        // ══════════════════════════════════════════════════════════════════
        // BÁO CÁO TỔNG KẾT
        // ══════════════════════════════════════════════════════════════════

        /// <summary>
        /// Mở frmReportViewer với projectId đang chọn (null = toàn hệ thống).
        /// Chỉ gọi khi AppSession.IsAdmin || AppSession.IsManager.
        /// </summary>
        private async Task OpenDashboardReportAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                int? selectedProjectId = null;
                if (cboProject.SelectedItem != null)
                {
                    var selId = (int)((dynamic)cboProject.SelectedItem).Id;
                    if (selId > 0) selectedProjectId = selId;
                }

                using var reportForm = new frmReportViewer(_expenseService, selectedProjectId);
                this.Cursor = Cursors.Default;
                reportForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể mở cửa sổ báo cáo:\n\n{ex.Message}",
                    "Lỗi Báo cáo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // GDI+ HELPERS
        // ══════════════════════════════════════════════════════════════════

        private void DrawPanelTitle(Graphics g, int panelWidth, string title)
        {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using var brush = new SolidBrush(UIHelper.ColorTextPrimary);
            using var divPen = new Pen(UIHelper.ColorBorderLight);
            g.DrawString(title, UIHelper.FontHeaderLarge, brush, 20, 20);
            g.DrawLine(divPen, 0, 54, panelWidth, 54);
        }

        private void DrawNoData(Graphics g, int panelWidth, int panelHeight)
        {
            const string msg = "Chưa có dữ liệu thống kê";
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using var brush = new SolidBrush(UIHelper.ColorMuted);
            SizeF sz = g.MeasureString(msg, UIHelper.FontBase);
            g.DrawString(msg, UIHelper.FontBase, brush,
                (panelWidth - sz.Width) / 2,
                (panelHeight - sz.Height) / 2);
        }

        /// <summary>Tạo GraphicsPath hình chữ nhật bo góc, dùng chung cho card border và swatch.</summary>
        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
            var path = new GraphicsPath();

            if (radius == 0) { path.AddRectangle(bounds); return path; }

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_taskDataChangedHandler != null)
                _taskService.TaskDataChanged -= _taskDataChangedHandler;

            base.OnFormClosed(e);
        }
    }
}