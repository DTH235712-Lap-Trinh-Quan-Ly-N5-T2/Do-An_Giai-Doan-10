using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmTaskList : BaseForm
    {
        // ── Dependencies ──────────────────────────────────────────
        private readonly ITaskService _taskService = null!;
        private readonly IProjectService _projectService = null!;
        private readonly IUserService _userService = null!;

        // ── State ─────────────────────────────────────────────────
        private List<TaskItem> _currentItems = new();
        private TaskItem? _selectedTask;
        private int _currentPage = 1;
        private int _totalCount = 0;
        private const int PAGE_SIZE = 20;

        private string? _externalFilter;
        private int? _externalProjectId;

        private readonly System.Windows.Forms.Timer _debounceTimer = new() { Interval = 500 };

        private List<Status> _statuses = new();
        private List<Project> _projects = new();

        // ── Constructors ──────────────────────────────────────────

        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmTaskList()
        {
            InitializeComponent();
            _debounceTimer.Tick += DebounceTimer_Tick;
        }

        public frmTaskList(
            ITaskService taskService,
            IProjectService projectService,
            IUserService userService)
#pragma warning disable CS0618
            : this()
#pragma warning restore CS0618
        {
            _taskService = taskService;
            _projectService = projectService;
            _userService = userService;

            ApplyClientStyles();

            _taskService.TaskDataChanged += OnTaskDataChanged;
            dgvTasks.CellFormatting      += dgvTasks_CellFormatting;
        }

        // ── Khởi tạo giao diện ────────────────────────────────────

        private void ApplyClientStyles()
        {
            // Header
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            lblHeader.Font = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;

            // Toolbar
            panelTop.BackColor = UIHelper.ColorBackground;
            flowToolbar.BackColor = UIHelper.ColorBackground;
            txtSearch.Font = UIHelper.FontSmall;
            cboProjectFilter.Font = UIHelper.FontSmall;
            cboStatusFilter.Font = UIHelper.FontSmall;

            UIHelper.StyleButton(btnAddNew, UIHelper.ButtonVariant.Primary);
            UIHelper.StyleButton(btnEdit, UIHelper.ButtonVariant.Success);
            UIHelper.StyleButton(btnDelete, UIHelper.ButtonVariant.Danger);
            UIHelper.StyleButton(btnRefresh, UIHelper.ButtonVariant.Secondary);

            // DataGridView
            UIHelper.StyleDataGridView(dgvTasks);
            UIHelper.ApplyAlternateRowColors(dgvTasks);

            colProgress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDueDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPriority.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Status bar & phân trang
            panelBottom.BackColor = UIHelper.ColorHeaderBg;
            flowPaging.BackColor = UIHelper.ColorHeaderBg;
            lblStatus.Font = UIHelper.FontSmall;
            lblStatus.ForeColor = UIHelper.ColorSubtitle;
            lblCount.Font = UIHelper.FontSmall;
            lblCount.ForeColor = UIHelper.ColorSubtitle;
            lblPage.Font = UIHelper.FontSmall;
            lblPage.ForeColor = UIHelper.ColorSubtitle;

            UIHelper.StyleButton(btnPrev, UIHelper.ButtonVariant.Secondary);
            UIHelper.StyleButton(btnNext, UIHelper.ButtonVariant.Secondary);
            btnPrev.Font = UIHelper.FontSmall;
            btnNext.Font = UIHelper.FontSmall;
        }

        // ── External Filter (Drill-down từ Dashboard) ─────────────

        public async Task ApplyExternalFilter(string filterType, int? projectId)
        {
            _externalFilter = filterType;
            _externalProjectId = projectId;

            if (projectId.HasValue)
            {
                foreach (ComboItem item in cboProjectFilter.Items)
                {
                    if (item.Id == projectId.Value)
                    {
                        cboProjectFilter.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                cboProjectFilter.SelectedIndex = 0;
            }

            _currentPage = 1;
            await LoadDataAsync();
        }

        private async void OnTaskDataChanged(object? sender, EventArgs e)
        {
            if (this.IsHandleCreated && !this.IsDisposed)
                this.Invoke((MethodInvoker)(async () => await LoadDataAsync()));
        }

        // ── Form Load ─────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyRolePermissions();
            await LoadLookupsAsync();
            await LoadDataAsync();
        }

        private void ApplyRolePermissions()
        {
            bool canEdit = AppSession.IsManager;
            btnAddNew.Visible = canEdit;
            btnEdit.Visible = canEdit;
            btnDelete.Visible = canEdit;
        }

        // ── Lookups ───────────────────────────────────────────────

        private async Task LoadLookupsAsync()
        {
            var t1 = _taskService.GetAllStatusesAsync();
            var t2 = _projectService.GetProjectsForUserAsync(
                         AppSession.UserId, AppSession.IsManager);
            await Task.WhenAll(t1, t2);

            _statuses = t1.Result;
            _projects = t2.Result;

            cboStatusFilter.Items.Clear();
            cboStatusFilter.Items.Add(new ComboItem(0, "— Tất cả trạng thái —"));
            foreach (var s in _statuses)
                cboStatusFilter.Items.Add(new ComboItem(s.Id, s.Name));
            cboStatusFilter.SelectedIndex = 0;

            cboProjectFilter.Items.Clear();
            cboProjectFilter.Items.Add(new ComboItem(0, "— Tất cả dự án —"));
            foreach (var p in _projects)
                cboProjectFilter.Items.Add(new ComboItem(p.Id, p.Name));
            cboProjectFilter.SelectedIndex = 0;

            cboProjectFilter.AdjustDropDownWidth();
            cboStatusFilter.AdjustDropDownWidth();
        }

        // ── Load Data ─────────────────────────────────────────────

        private async Task LoadDataAsync()
        {
            SetStatus("⏳ Đang tải...");
            try
            {
                var keyword = txtSearch.Text.Trim();
                var statusId = GetComboId(cboStatusFilter);
                var projectId = _externalProjectId ?? GetComboId(cboProjectFilter);
                int? assignedToId = (AppSession.IsAdmin || AppSession.IsManager)
                    ? null : AppSession.UserId;

                List<TaskItem> items;
                int total;

                if (!string.IsNullOrEmpty(_externalFilter))
                {
                    if (_externalFilter == "OVERDUE")
                    {
                        var overdue = await _taskService.GetOverdueTasksAsync();
                        if (projectId > 0) overdue = overdue.Where(x => x.ProjectId == projectId).ToList();
                        if (assignedToId.HasValue) overdue = overdue.Where(x => x.AssignedToId == assignedToId).ToList();
                        items = overdue; total = overdue.Count;
                        UpdateHeaderUI("CÔNG VIỆC QUÁ HẠN", UIHelper.ColorDanger);
                    }
                    else if (_externalFilter == "DUE_SOON")
                    {
                        var dueSoon = await _taskService.GetDueSoonTasksAsync(7);
                        if (projectId > 0) dueSoon = dueSoon.Where(x => x.ProjectId == projectId).ToList();
                        if (assignedToId.HasValue) dueSoon = dueSoon.Where(x => x.AssignedToId == assignedToId).ToList();
                        items = dueSoon; total = dueSoon.Count;
                        UpdateHeaderUI("CÔNG VIỆC SẮP TỚI HẠN (7 NGÀY)", UIHelper.ColorWarning);
                    }
                    else if (_externalFilter == "COMPLETED")
                    {
                        var res = await _taskService.GetPagedAsync(
                            _currentPage, PAGE_SIZE,
                            projectId > 0 ? projectId : null,
                            assignedToId, statusId: 10);
                        items = res.Items; total = res.TotalCount;
                        UpdateHeaderUI("CÔNG VIỆC ĐÃ HOÀN THÀNH", UIHelper.ColorSuccess);
                    }
                    else
                    {
                        var res = await _taskService.GetPagedAsync(
                            _currentPage, PAGE_SIZE,
                            projectId > 0 ? projectId : null,
                            assignedToId);
                        items = res.Items; total = res.TotalCount;
                        UpdateHeaderUI("DANH SÁCH CÔNG VIỆC", UIHelper.ColorPrimary);
                    }
                }
                else
                {
                    var (resItems, resTotal) = await _taskService.GetPagedAsync(
                        page: _currentPage,
                        pageSize: PAGE_SIZE,
                        projectId: projectId > 0 ? projectId : null,
                        assignedToId: assignedToId,
                        statusId: statusId > 0 ? statusId : null,
                        keyword: string.IsNullOrEmpty(keyword) ? null : keyword);

                    items = resItems; total = resTotal;
                    UpdateHeaderUI("DANH SÁCH CÔNG VIỆC", UIHelper.ColorPrimary);
                }

                _currentItems = items;
                _totalCount = total;

                BindGrid(items);
                UpdatePagingLabel();
                SetStatus($"Hiển thị {items.Count} / {total} công việc");

                if (total == 0 && AppSession.IsAdmin)
                    SetStatus("⚠ DB trống hoặc filter lỗi (Admin 0 Task)");
            }
            catch (Exception ex)
            {
                SetStatus("⚠ Lỗi tải dữ liệu.");
                MessageBox.Show($"Không thể tải dữ liệu:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateHeaderUI(string title, System.Drawing.Color accentColor)
        {
            lblHeader.Text = title;
            lblHeader.ForeColor = accentColor;
            this.Text = "TaskFlow — " + title;
        }

        // ── Grid Binding ──────────────────────────────────────────

        private void BindGrid(List<TaskItem> items)
        {
            dgvTasks.Rows.Clear();
            _selectedTask = null;
            RefreshButtonStates();

            foreach (var t in items)
            {
                var due = t.DueDate.HasValue
                    ? t.DueDate.Value.ToLocalTime().ToString("dd/MM/yyyy") : "—";

                // colTaskCode (cột ẩn) được thêm đầu tiên để CellFormatting lấy ra
                int idx = dgvTasks.Rows.Add(
                    t.Id,
                    t.Title,            // hiển thị thuần, CellFormatting sẽ nối TaskCode
                    t.Project?.Name ?? "—",
                    t.AssignedTo?.FullName ?? "—",
                    t.Priority?.Name ?? "—",
                    t.Status?.Name ?? "—",
                    $"{t.ProgressPercent}%",
                    due);

                // Gán Tag = TaskCode để CellFormatting có thể lấy ra mà không cần cột ẩn thứ 2
                dgvTasks.Rows[idx].Tag = t.TaskCode;

                UIHelper.ApplyTaskRowStyle(
                    dgvTasks.Rows[idx],
                    t.Status?.Name,
                    t.IsCompleted,
                    t.DueDate);
            }

            lblCount.Text = $"{items.Count} công việc";
        }

        /// <summary>
        /// Hiển thị “Tên task #Mã-task” trong cột Title mà không thêm cột mới.
        /// Lấy TaskCode từ Row.Tag (gán trong BindGrid).
        /// </summary>
        private void dgvTasks_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvTasks.Columns["colTitle"]?.Index) return;

            var row = dgvTasks.Rows[e.RowIndex];
            var taskCode = row.Tag as string;
            if (!string.IsNullOrWhiteSpace(taskCode) && e.Value is string title)
            {
                e.Value = $"{title} #{taskCode}";
                e.FormattingApplied = true;
            }
        }

        // ── Debounce Search ───────────────────────────────────────

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        private async void DebounceTimer_Tick(object? sender, EventArgs e)
        {
            _debounceTimer.Stop();
            _currentPage = 1;
            await LoadDataAsync();
        }

        // ── Filter Events ─────────────────────────────────────────

        private async void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            await LoadDataAsync();
        }

        // ── Pagination ────────────────────────────────────────────

        private void UpdatePagingLabel()
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)_totalCount / PAGE_SIZE));
            lblPage.Text = $"Trang {_currentPage} / {totalPages}";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage <= 1) return;
            _currentPage--;
            await LoadDataAsync();
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)_totalCount / PAGE_SIZE));
            if (_currentPage >= totalPages) return;
            _currentPage++;
            await LoadDataAsync();
        }

        // ── Selection ─────────────────────────────────────────────

        private void dgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                _selectedTask = null;
                RefreshButtonStates();
                return;
            }

            var cell = dgvTasks.SelectedRows[0].Cells["colId"].Value;
            if (cell == null) return;
            _selectedTask = _currentItems.FirstOrDefault(t => t.Id == (int)cell);
            RefreshButtonStates();
        }

        private void RefreshButtonStates()
        {
            bool sel = _selectedTask != null;
            btnEdit.Enabled = sel;
            btnDelete.Enabled = sel;
        }

        // ── CRUD ──────────────────────────────────────────────────

        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            btnAddNew.Enabled = false;
            try
            {
                using var dlg = new frmTaskEdit(_taskService, _projectService, _userService, null);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    await LoadDataAsync();
            }
            finally
            {
                if (!this.IsDisposed) btnAddNew.Enabled = true;
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedTask == null) return;

            btnEdit.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                using var dlg = new frmTaskEdit(_taskService, _projectService, _userService, _selectedTask.Id);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    await LoadDataAsync();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (!this.IsDisposed) btnEdit.Enabled = true;
            }
        }

        private void dgvTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit_Click(sender, e);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedTask == null) return;
            if (MessageBox.Show(
                    $"Xóa công việc \"{_selectedTask.Title}\"?\n\n⚠ Không thể hoàn tác.",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes) return;

            btnDelete.Enabled = false;
            try
            {
                var (ok, msg) = await _taskService.DeleteTaskAsync(_selectedTask.Id);
                MessageBox.Show(msg,
                    ok ? "Thành công" : "Không thể xóa",
                    MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (ok) await LoadDataAsync();
            }
            finally
            {
                if (!this.IsDisposed) btnDelete.Enabled = true;
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            try
            {
                _externalFilter = null;
                _externalProjectId = null;

                // FIX: Tải lại danh sách dự án & trạng thái TRƯỚC để cập nhật dự án mới vào ComboBox
                await LoadLookupsAsync();

                txtSearch.Clear();
                cboStatusFilter.SelectedIndex = 0;
                cboProjectFilter.SelectedIndex = 0;
                _currentPage = 1;
                await LoadDataAsync();
            }
            finally
            {
                if (!this.IsDisposed) btnRefresh.Enabled = true;
            }
        }

        // ── Helpers ───────────────────────────────────────────────

        private static int GetComboId(ComboBox cbo)
            => cbo.SelectedItem is ComboItem ci ? ci.Id : 0;

        private void SetStatus(string msg) => lblStatus.Text = msg;

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _taskService.TaskDataChanged -= OnTaskDataChanged;
            dgvTasks.CellFormatting      -= dgvTasks_CellFormatting;
            _debounceTimer.Stop();
            _debounceTimer.Dispose();
            base.OnFormClosed(e);
        }
    }
}