// ============================================================
//  frmExpenses.cs
//  TaskFlowManagement.WinForms.Forms
// ============================================================
using TaskFlowManagement.Core.DTOs;
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmExpenses : BaseForm
    {
        // ── Dependencies ──────────────────────────────────────────────────────
        private readonly IExpenseService _expenseService = null!;
        private readonly IProjectService _projectService = null!;

        // ── State ─────────────────────────────────────────────────────────────
        private List<Expense> _allExpenses = new();
        private Expense? _selectedExpense = null;
        private bool _isLoadingProjects = false;

        // ── Constructors ──────────────────────────────────────────────────────

        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmExpenses()
        {
            InitializeComponent();
        }

        public frmExpenses(IExpenseService expenseService, IProjectService projectService)
#pragma warning disable CS0618
            : this()
#pragma warning restore CS0618
        {
            _expenseService = expenseService;
            _projectService = projectService;

            ApplyClientStyles();
            SetupGrid();
            SetupPermissions();
            WireEvents();
        }

        // ── Áp dụng toàn bộ style, font, màu sắc ────────────────────────────
        private void ApplyClientStyles()
        {
            this.BackColor = UIHelper.ColorBackground;
            this.Font = UIHelper.FontBase;

            // Header
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;

            // Filter bar
            panelFilter.BackColor = UIHelper.ColorBackground;
            lblProjectFilter.Font = UIHelper.FontLabel;
            lblProjectFilter.ForeColor = UIHelper.ColorTextPrimary;
            lblTypeFilter.Font = UIHelper.FontLabel;
            lblTypeFilter.ForeColor = UIHelper.ColorTextPrimary;

            UIHelper.StyleFilterCombo(cboProject);
            UIHelper.StyleFilterCombo(cboExpenseType);
            cboExpenseType.Items.Clear();
            cboExpenseType.Items.AddRange(new object[]
                { "— Tất cả —", "Nhân công", "Phần mềm", "Hạ tầng", "Khác" });
            cboExpenseType.SelectedIndex = 0;

            UIHelper.StyleButton(btnRefresh, UIHelper.ButtonVariant.Secondary);

            // Toolbar
            panelToolbar.BackColor = UIHelper.ColorSurface;
            UIHelper.StyleButton(btnAdd, UIHelper.ButtonVariant.Primary);
            UIHelper.StyleButton(btnEdit, UIHelper.ButtonVariant.Success);
            UIHelper.StyleButton(btnDelete, UIHelper.ButtonVariant.Danger);
            UIHelper.StyleButton(btnDetail, UIHelper.ButtonVariant.Slate);
            UIHelper.StyleButton(btnExportReport, UIHelper.ButtonVariant.Purple);

            lblCount.Font = UIHelper.FontSmall;
            lblCount.ForeColor = UIHelper.ColorMuted;

            // Căn lblCount về phía phải toolbar
            panelToolbar.SizeChanged += (_, _) =>
                lblCount.Location = new System.Drawing.Point(
                    panelToolbar.Width - lblCount.Width - 14,
                    (panelToolbar.Height - lblCount.Height) / 2);

            // Khởi tạo layout vùng thống kê
            BuildSummaryPanel();

            // DataGridView
            UIHelper.StyleDataGridView(dgvExpenses);
            UIHelper.ApplyAlternateRowColors(dgvExpenses);
            dgvExpenses.RowTemplate.Height = 38;

            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Status bar
            panelStatus.BackColor = UIHelper.ColorHeaderBg;
            lblStatus.Font = UIHelper.FontSmall;
            lblStatus.ForeColor = UIHelper.ColorSubtitle;
        }

        // ── Xây dựng vùng thống kê 4 cột ─────────────────────────────────────
        private void BuildSummaryPanel()
        {
            panelSummary.BackColor = UIHelper.ColorBackground;

            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                BackColor = System.Drawing.Color.Transparent,
            };
            for (int i = 0; i < 4; i++)
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            tlp.Controls.Add(BuildStatCard(lblBudgetTitle, lblBudgetVal), 0, 0);
            tlp.Controls.Add(BuildStatCard(lblTotalExpenseTitle, lblTotalExpenseVal), 1, 0);
            tlp.Controls.Add(BuildStatCard(lblRemainingTitle, lblRemainingVal), 2, 0);
            tlp.Controls.Add(BuildUsageCard(lblUsagePct), 3, 0);

            panelSummary.Controls.Clear();
            panelSummary.Controls.Add(tlp);
        }

        private Panel BuildStatCard(Label title, Label val)
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UIHelper.ColorSurface,
                Margin = new Padding(4),
                BorderStyle = BorderStyle.None,
            };

            title.Dock = DockStyle.Top;
            title.Height = 26;
            title.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            title.Font = UIHelper.FontLabel;
            title.ForeColor = UIHelper.ColorMuted;
            title.Padding = new Padding(10, 0, 0, 0);

            val.Dock = DockStyle.Fill;
            val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            val.Font = UIHelper.FontHeaderLarge;
            val.ForeColor = UIHelper.ColorTextPrimary;
            val.Padding = new Padding(10, 0, 0, 0);

            pnl.Controls.Add(val);
            pnl.Controls.Add(title);
            return pnl;
        }

        private Panel BuildUsageCard(Label pct)
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UIHelper.ColorSurface,
                Margin = new Padding(4),
                BorderStyle = BorderStyle.None,
            };

            var title = new Label
            {
                Text = "TỶ LỆ SỬ DỤNG",
                Dock = DockStyle.Top,
                Height = 26,
                TextAlign = System.Drawing.ContentAlignment.BottomLeft,
                Font = UIHelper.FontLabel,
                ForeColor = UIHelper.ColorMuted,
                Padding = new Padding(10, 0, 0, 0),
            };

            pct.Dock = DockStyle.Fill;
            pct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            pct.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            pct.Padding = new Padding(0, 0, 16, 0);

            pnl.Controls.Add(pct);
            pnl.Controls.Add(title);
            return pnl;
        }

        // ── Cấu hình DataPropertyName cho grid ───────────────────────────────
        private void SetupGrid()
        {
            dgvExpenses.AutoGenerateColumns = false;
            colId.DataPropertyName = "Id";
            colProject.DataPropertyName = "ProjectName";
            colType.DataPropertyName = "ExpenseType";
            colAmount.DataPropertyName = "Amount";
            colDate.DataPropertyName = "ExpenseDateDisplay";
            colNote.DataPropertyName = "Note";
            colCreatedBy.DataPropertyName = "CreatorName";

            // Giãn cột Tên Dự Án
            colProject.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        // ── Phân quyền hiển thị nút ──────────────────────────────────────────
        private void SetupPermissions()
        {
            bool canManage = AppSession.IsManager || AppSession.IsAdmin;
            btnAdd.Visible = canManage;
            btnEdit.Visible = canManage;
            btnDelete.Visible = canManage;
            btnExportReport.Visible = canManage;
        }

        // ── Đăng ký sự kiện ──────────────────────────────────────────────────
        private void WireEvents()
        {
            this.Load += async (_, _) =>
            {
                await LoadProjectsAsync();
                await LoadExpensesAsync();
            };

            cboProject.SelectedIndexChanged += async (_, _) => { if (!_isLoadingProjects) await LoadExpensesAsync(); };
            cboExpenseType.SelectedIndexChanged += (_, _) => ApplyFilter();
            btnRefresh.Click += async (_, _) =>
            {
                btnRefresh.Enabled = false;
                try { await LoadExpensesAsync(); }
                finally { if (!this.IsDisposed) btnRefresh.Enabled = true; }
            };

            dgvExpenses.SelectionChanged += (_, _) =>
            {
                _selectedExpense = null;
                if (dgvExpenses.CurrentRow?.Selected == true
                    && dgvExpenses.CurrentRow.Cells["colId"].Value is int id)
                    _selectedExpense = _allExpenses.FirstOrDefault(x => x.Id == id);
                UpdateButtons();
            };

            btnAdd.Click += async (_, _) =>
            {
                btnAdd.Enabled = false;
                try { await OpenEditFormAsync(null); }
                finally { if (!this.IsDisposed) btnAdd.Enabled = true; }
            };
            btnEdit.Click += async (_, _) =>
            {
                btnEdit.Enabled = false;
                try { await OpenEditFormAsync(_selectedExpense); }
                finally { if (!this.IsDisposed) btnEdit.Enabled = true; }
            };
            btnDelete.Click += async (_, _) => await DeleteExpenseAsync();
            btnDetail.Click += (_, _) => ShowDetail();
            btnExportReport.Click += async (_, _) =>
            {
                btnExportReport.Enabled = false;
                try { await OpenReportAsync(); }
                finally { if (!this.IsDisposed) btnExportReport.Enabled = true; }
            };

            dgvExpenses.CellFormatting += DgvExpenses_CellFormatting;
        }

        private void UpdateButtons()
        {
            bool has = _selectedExpense != null;
            btnEdit.Enabled = has;
            btnDelete.Enabled = has;
            btnDetail.Enabled = has;
        }

        // ── Tải danh sách dự án ───────────────────────────────────────────────
        private async Task LoadProjectsAsync()
        {
            _isLoadingProjects = true;
            try
            {
                var projects = await _projectService.GetProjectsForUserAsync(
                    AppSession.UserId, AppSession.IsManager || AppSession.IsAdmin);

                cboProject.Items.Clear();
                cboProject.Items.Add(new ComboItem(0, "-- Tất cả dự án --"));
                foreach (var p in projects)
                    cboProject.Items.Add(new ComboItem(p.Id, p.Name));

                cboProject.SelectedIndex = 0;
                cboProject.AdjustDropDownWidth();
                cboExpenseType.AdjustDropDownWidth();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dự án: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingProjects = false;
            }
        }

        // ── Tải dữ liệu chi phí ──────────────────────────────────────────────
        private async Task LoadExpensesAsync()
        {
            SetStatus("⏳ Đang tải dữ liệu...");
            try
            {
                int projectId = (cboProject.SelectedItem as ComboItem)?.Id ?? 0;

                if (projectId > 0)
                {
                    _allExpenses = await _expenseService.GetByProjectAsync(projectId);
                    var summary = await _expenseService.GetProjectBudgetSummaryAsync(projectId);
                    UpdateSummaryCard(summary);
                }
                else
                {
                    var projects = await _projectService.GetProjectsForUserAsync(
                        AppSession.UserId, AppSession.IsManager || AppSession.IsAdmin);

                    _allExpenses = new List<Expense>();
                    foreach (var p in projects)
                        _allExpenses.AddRange(await _expenseService.GetByProjectAsync(p.Id));

                    _allExpenses = _allExpenses.OrderByDescending(x => x.ExpenseDate).ToList();
                    UpdateSummaryCard(null);
                }

                ApplyFilter();
                SetStatus($"✅ Đã tải {_allExpenses.Count} bản ghi.");
            }
            catch (Exception ex)
            {
                SetStatus("❌ Lỗi tải dữ liệu.");
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Cập nhật vùng thống kê ────────────────────────────────────────────
        private void UpdateSummaryCard(ProjectBudgetSummaryDto? summary)
        {
            if (summary == null)
            {
                lblBudgetVal.Text = "—";
                lblTotalExpenseVal.Text = "—";
                lblRemainingVal.Text = "—";
                lblUsagePct.Text = "0%";
                lblBudgetVal.ForeColor = UIHelper.ColorTextPrimary;
                lblTotalExpenseVal.ForeColor = UIHelper.ColorTextPrimary;
                lblRemainingVal.ForeColor = UIHelper.ColorTextPrimary;
                lblUsagePct.ForeColor = UIHelper.ColorMuted;
                return;
            }

            lblBudgetVal.Text = summary.Budget.ToString("N0") + " ₫";
            lblTotalExpenseVal.Text = summary.TotalExpense.ToString("N0") + " ₫";
            lblRemainingVal.Text = summary.Remaining.ToString("N0") + " ₫";
            lblUsagePct.Text = summary.UsagePercent.ToString("N0") + "%";

            lblBudgetVal.ForeColor = UIHelper.ColorTextPrimary;

            if (summary.IsOverBudget)
            {
                lblTotalExpenseVal.ForeColor = UIHelper.ColorDanger;
                lblRemainingVal.ForeColor = UIHelper.ColorDanger;
                lblUsagePct.ForeColor = UIHelper.ColorDanger;
            }
            else if (summary.UsagePercent > 80)
            {
                lblTotalExpenseVal.ForeColor = UIHelper.ColorWarning;
                lblRemainingVal.ForeColor = UIHelper.ColorWarning;
                lblUsagePct.ForeColor = UIHelper.ColorWarning;
            }
            else
            {
                lblTotalExpenseVal.ForeColor = UIHelper.ColorTextPrimary;
                lblRemainingVal.ForeColor = UIHelper.ColorSuccess;
                lblUsagePct.ForeColor = UIHelper.ColorMuted;
            }
        }

        // ── Lọc và bind dữ liệu ──────────────────────────────────────────────
        private void ApplyFilter()
        {
            string typeFilter = cboExpenseType.SelectedItem?.ToString() ?? "— Tất cả —";
            var filtered = _allExpenses
                .Where(x => typeFilter == "— Tất cả —" || x.ExpenseType == typeFilter)
                .ToList();
            BindGrid(filtered);
        }

        private void BindGrid(List<Expense> list)
        {
            var source = list.Select(e => new
            {
                e.Id,
                ProjectName = e.Project?.Name ?? "—",
                e.ExpenseType,
                e.Amount,
                ExpenseDateDisplay = e.ExpenseDate.ToString("dd/MM/yyyy"),
                Note = e.Note ?? "—",
                CreatorName = e.CreatedBy?.FullName ?? "Hệ thống",
            }).ToList();

            dgvExpenses.DataSource = source;
            lblCount.Text = $"Tổng số: {list.Count} khoản chi.";
        }

        // ── Mở form chỉnh sửa ────────────────────────────────────────────────
        private async Task OpenEditFormAsync(Expense? expense)
        {
            using var dlg = new frmExpenseEdit(_expenseService, _projectService, expense);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                await LoadExpensesAsync();
        }

        // ── Xóa chi phí ──────────────────────────────────────────────────────
        private async Task DeleteExpenseAsync()
        {
            if (_selectedExpense == null) return;

            var confirm = MessageBox.Show(
                $"Xác nhận xóa khoản chi {_selectedExpense.Amount:N0} ₫ " +
                $"cho dự án {_selectedExpense.Project?.Name}?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            btnDelete.Enabled = false;
            try
            {
                var (ok, msg) = await _expenseService.DeleteExpenseAsync(_selectedExpense.Id);
                if (ok) await LoadExpensesAsync();
                else MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!this.IsDisposed) btnDelete.Enabled = _selectedExpense != null;
            }
        }

        // ── Hiển thị chi tiết ────────────────────────────────────────────────
        private void ShowDetail()
        {
            if (_selectedExpense == null) return;

            MessageBox.Show(
                $"Dự án   : {_selectedExpense.Project?.Name}\n" +
                $"Loại    : {_selectedExpense.ExpenseType}\n" +
                $"Số tiền : {_selectedExpense.Amount:N0} ₫\n" +
                $"Ngày    : {_selectedExpense.ExpenseDate:dd/MM/yyyy}\n" +
                $"Người tạo: {_selectedExpense.CreatedBy?.FullName}\n" +
                $"Ghi chú : {_selectedExpense.Note ?? "—"}",
                "Chi tiết chi phí", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ── Format cột số tiền ────────────────────────────────────────────────
        private void DgvExpenses_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;
            if (dgvExpenses.Columns[e.ColumnIndex].Name != "colAmount") return;

            if (e.Value is decimal amount)
            {
                e.Value = amount.ToString("N0") + " ₫";
                e.FormattingApplied = true;
            }
        }

        // ── Mở báo cáo ───────────────────────────────────────────────────────
        private async Task OpenReportAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                int? selectedProjectId = null;
                if (cboProject.SelectedItem is ComboItem item && item.Id > 0)
                    selectedProjectId = item.Id;

                using var reportForm = new frmReportViewer(_expenseService, selectedProjectId);
                this.Cursor = Cursors.Default;
                reportForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể mở cửa sổ báo cáo:\n\n{ex.Message}",
                    "Lỗi Báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // ── Thanh trạng thái ─────────────────────────────────────────────────
        private void SetStatus(string msg)
        {
            if (lblStatus != null && !lblStatus.IsDisposed)
                lblStatus.Text = msg;
        }
    }
}