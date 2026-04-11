// ============================================================
//  ucTaskCard.cs  —  TaskFlowManagement.WinForms.Controls
//
//  NÂNG CẤP so với phiên bản cũ:
//    1. Height 140px  — đủ chỗ cho ProgressBar + DueDate chip
//    2. Thanh màu Priority 3px bên trái — nhận diện nhanh mà không cần đọc chữ
//    3. Inline editing — double-click lblTitle → sửa trực tiếp tại chỗ
//    4. Events mới: TitleChanged, ProgressChanged — để frmKanban lưu DB
//    5. UpdateBoundTask() — cập nhật toàn bộ card sau khi lưu xong
// ============================================================
using TaskFlowManagement.Core.Constants;
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Controls
{
    public sealed class StatusChangedEventArgs : EventArgs
    {
        public int TaskId    { get; }
        public int NewStatusId { get; }
        public StatusChangedEventArgs(int taskId, int newStatusId)
        { TaskId = taskId; NewStatusId = newStatusId; }
    }

    public partial class ucTaskCard : UserControl
    {
        // ── State ─────────────────────────────────────────────────────────────
        private int  _taskId;
        private int  _currentStatusId;
        private bool _isDragging;
        private Point _dragStartPoint;

        public int TaskId => _taskId;

        // ── Events ────────────────────────────────────────────────────────────
        /// <summary>Context-menu status change (right-click).</summary>
        public event EventHandler<StatusChangedEventArgs>? StatusChanged;

        /// <summary>Double-click trên card.</summary>
        public event EventHandler<int>? CardDoubleClicked;

        /// <summary>
        /// Người dùng đã sửa tiêu đề trực tiếp trên card (double-click lblTitle).
        /// frmKanban lắng nghe và gọi _taskService.UpdateTaskAsync().
        /// </summary>
        public event EventHandler<(int TaskId, string NewTitle)>? TitleChanged;

        // ── Constructor ───────────────────────────────────────────────────────
        public ucTaskCard()
        {
            InitializeComponent();
            WireContextMenuEvents();
            WireDoubleClickRecursive();
            this.MouseDown += OnCardMouseDown;
            this.MouseMove += OnCardMouseMove;
        }

        // ── Drag-drop ─────────────────────────────────────────────────────────
        private void OnCardMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _dragStartPoint = e.Location;
        }

        private void OnCardMouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || _isDragging) return;
            var delta = new Point(
                Math.Abs(e.X - _dragStartPoint.X),
                Math.Abs(e.Y - _dragStartPoint.Y));
            if (delta.X < SystemInformation.DragSize.Width &&
                delta.Y < SystemInformation.DragSize.Height) return;

            _isDragging = true;
            DoDragDrop(_taskId, DragDropEffects.Move);
            _isDragging = false;
        }

        // ── Data binding ──────────────────────────────────────────────────────
        /// <summary>Bind TaskItem lần đầu khi tạo card.</summary>
        public void Bind(TaskItem task)
        {
            ArgumentNullException.ThrowIfNull(task);
            _taskId          = task.Id;
            _currentStatusId = task.StatusId;

            // Tiêu đề — nối #[TaskCode] nếu có (format: Tên task #Mã-task)
            string displayTitle = string.IsNullOrWhiteSpace(task.Title)
                ? "(Không có tiêu đề)"
                : task.Title.Trim();

            if (!string.IsNullOrWhiteSpace(task.TaskCode))
                displayTitle = $"{displayTitle} #{task.TaskCode}";

            lblTitle.Text = displayTitle;

            // Assignee + avatar
            string? name = task.AssignedTo?.FullName;
            if (string.IsNullOrWhiteSpace(name))
                name = task.AssignedToId.HasValue ? $"User #{task.AssignedToId}" : "Chưa phân công";
            lblAssignee.Text    = name;
            lblAvatarInitials.Text = GetInitials(name);

            // Priority chip + thanh màu
            string priorityName = task.Priority?.Name
                ?? WorkflowConstants.GetPriorityName(task.PriorityId);
            lblPriority.Text     = priorityName;
            lblPriority.ForeColor = GetPriorityForeColor(priorityName);
            pnlPriorityBar.BackColor = GetPriorityBarColor(priorityName);

            // Status chip
            lblStatus.Text      = WorkflowConstants.GetStatusName(_currentStatusId);
            lblStatus.ForeColor = UIHelper.ApplyTaskRowForeColor(_currentStatusId, task.IsCompleted, task.DueDate);

            // Due date chip
            ApplyDueDateChip(task.DueDate, task.IsCompleted);

            // Progress bar
            byte progress = task.ProgressPercent;
            pnlProgressFill.Width = (int)(pnlProgressTrack.Width * progress / 100.0);
            lblProgress.Text      = $"{progress}%";

            UpdateMenuItemState();
        }

        /// <summary>
        /// Cập nhật toàn bộ card sau khi frmKanban lưu xong (ví dụ sau inline title edit).
        /// Gọi thay cho Bind() để tránh tạo lại event subscriptions.
        /// </summary>
        public void UpdateBoundTask(TaskItem task) => Bind(task);

        /// <summary>Chỉ cập nhật status (dùng sau drag-drop / context-menu).</summary>
        public void UpdateBoundStatus(int newStatusId)
        {
            _currentStatusId  = newStatusId;
            lblStatus.Text    = WorkflowConstants.GetStatusName(newStatusId);
            UpdateMenuItemState();
        }

        // ── Inline title editing ──────────────────────────────────────────────
        /// <summary>
        /// Double-click vào lblTitle → TextBox overlay xuất hiện tại chỗ.
        /// Nhấn Enter hoặc mất focus → lưu và raise TitleChanged.
        /// Nhấn Escape → hủy, khôi phục chữ cũ.
        /// </summary>
        private void lblTitle_DoubleClick(object? sender, EventArgs e)
        {
            // Tạo TextBox overlay khớp vị trí lblTitle
            var txt = new TextBox
            {
                Text        = lblTitle.Text,
                Font        = lblTitle.Font,
                Bounds      = new Rectangle(
                                  lblTitle.Left,
                                  lblTitle.Top,
                                  lblTitle.Width,
                                  lblTitle.Height),
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength   = 200,
                BackColor   = Color.FromArgb(255, 252, 232),   // vàng nhạt báo hiệu đang edit
            };

            string originalText = lblTitle.Text;

            void CommitEdit()
            {
                string newTitle = txt.Text.Trim();
                pnlContainer.Controls.Remove(txt);
                txt.Dispose();

                if (!string.IsNullOrWhiteSpace(newTitle) && newTitle != originalText)
                {
                    lblTitle.Text = newTitle;
                    TitleChanged?.Invoke(this, (_taskId, newTitle));
                }
            }

            void CancelEdit()
            {
                pnlContainer.Controls.Remove(txt);
                txt.Dispose();
                lblTitle.Text = originalText;
            }

            txt.KeyDown += (_, ke) =>
            {
                if (ke.KeyCode == Keys.Enter)   { ke.SuppressKeyPress = true; CommitEdit(); }
                else if (ke.KeyCode == Keys.Escape) CancelEdit();
            };
            txt.LostFocus += (_, _) => CommitEdit();

            pnlContainer.Controls.Add(txt);
            txt.BringToFront();
            txt.Focus();
            txt.SelectAll();
        }

        // ── Context-menu events ───────────────────────────────────────────────
        private void WireContextMenuEvents()
        {
            miCreated.Click    += (_, _) => RaiseStatusChanged(1);
            miAssigned.Click   += (_, _) => RaiseStatusChanged(2);
            miInProgress.Click += (_, _) => RaiseStatusChanged(3);
            miFailed.Click     += (_, _) => RaiseStatusChanged(4);
            miReview1.Click    += (_, _) => RaiseStatusChanged(5);
            miReview2.Click    += (_, _) => RaiseStatusChanged(6);
            miApproved.Click   += (_, _) => RaiseStatusChanged(7);
            miInTest.Click     += (_, _) => RaiseStatusChanged(8);
            miResolved.Click   += (_, _) => RaiseStatusChanged(9);
            miClosed.Click     += (_, _) => RaiseStatusChanged(10);
        }

        private void RaiseStatusChanged(int newStatusId)
        {
            if (_taskId <= 0 || newStatusId <= 0 || newStatusId == _currentStatusId) return;
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(_taskId, newStatusId));
        }

        private void UpdateMenuItemState()
        {
            miCreated.Enabled    = _currentStatusId != 1;
            miAssigned.Enabled   = _currentStatusId != 2;
            miInProgress.Enabled = _currentStatusId != 3;
            miFailed.Enabled     = _currentStatusId != 4;
            miReview1.Enabled    = _currentStatusId != 5;
            miReview2.Enabled    = _currentStatusId != 6;
            miApproved.Enabled   = _currentStatusId != 7;
            miInTest.Enabled     = _currentStatusId != 8;
            miResolved.Enabled   = _currentStatusId != 9;
            miClosed.Enabled     = _currentStatusId != 10;
        }

        // ── Double-click propagation ──────────────────────────────────────────
        private void WireDoubleClickRecursive()
        {
            // Chỉ gắn CardDoubleClicked cho container & avatar, KHÔNG gắn vào lblTitle
            // (lblTitle có handler riêng cho inline edit)
            pnlContainer.DoubleClick  += InvokeCardDoubleClick;
            lblAvatarInitials.DoubleClick += InvokeCardDoubleClick;
            lblAssignee.DoubleClick   += InvokeCardDoubleClick;
            lblStatus.DoubleClick     += InvokeCardDoubleClick;
            lblPriority.DoubleClick   += InvokeCardDoubleClick;
            lblDueDate.DoubleClick    += InvokeCardDoubleClick;
            // Gắn double-click của lblTitle cho inline edit
            lblTitle.DoubleClick      += lblTitle_DoubleClick;
        }

        private void InvokeCardDoubleClick(object? sender, EventArgs e)
        {
            if (_taskId > 0) CardDoubleClicked?.Invoke(this, _taskId);
        }

        // ── Helper methods ────────────────────────────────────────────────────
        private void ApplyDueDateChip(DateTime? dueDate, bool isCompleted)
        {
            if (!dueDate.HasValue)
            {
                lblDueDate.Visible = false;
                return;
            }

            lblDueDate.Visible = true;
            var local = dueDate.Value.ToLocalTime();
            lblDueDate.Text = $"⏰ {local:dd/MM}";

            bool overdue = local < DateTime.Now && !isCompleted;
            bool dueSoon = !overdue && local <= DateTime.Now.AddDays(3) && !isCompleted;

            if (overdue)
            {
                lblDueDate.ForeColor = UIHelper.ColorDanger;
                lblDueDate.BackColor = Color.FromArgb(254, 226, 226);
            }
            else if (dueSoon)
            {
                lblDueDate.ForeColor = UIHelper.ColorWarning;
                lblDueDate.BackColor = Color.FromArgb(254, 243, 199);
            }
            else
            {
                lblDueDate.ForeColor = UIHelper.ColorMuted;
                lblDueDate.BackColor = Color.FromArgb(241, 245, 249);
            }
        }

        private static string GetInitials(string name)
        {
            var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2) return $"{parts[0][0]}{parts[^1][0]}".ToUpper();
            return name.Length >= 2 ? name[..2].ToUpper() : name.ToUpper();
        }

        // Màu text cho priority chip
        private static Color GetPriorityForeColor(string priorityName) =>
            priorityName.Trim().ToUpperInvariant() switch
            {
                "CRITICAL" => UIHelper.ColorDanger,
                "HIGH"     => Color.FromArgb(180, 83, 9),   // amber-700
                "MEDIUM"   => UIHelper.ColorWarning,
                "LOW"      => UIHelper.ColorSuccess,
                _          => UIHelper.ColorMuted
            };

        // Màu thanh bên trái 3px
        private static Color GetPriorityBarColor(string priorityName) =>
            priorityName.Trim().ToUpperInvariant() switch
            {
                "CRITICAL" => UIHelper.ColorDanger,
                "HIGH"     => UIHelper.ColorWarning,
                "MEDIUM"   => UIHelper.ColorPrimary,
                "LOW"      => UIHelper.ColorSuccess,
                _          => UIHelper.ColorBorderLight
            };
    }
}
