using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmExpenseEdit : BaseForm
    {
        private readonly IExpenseService _expenseService;
        private readonly IProjectService _projectService;
        private readonly Expense? _editingExpense;

        // Chỉ dùng cho WinForms Designer — không gọi ở runtime
        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmExpenseEdit()
        {
            InitializeComponent();
        }

        public frmExpenseEdit(
            IExpenseService expenseService,
            IProjectService projectService,
            Expense? expense = null)
        {
            _expenseService = expenseService;
            _projectService = projectService;
            _editingExpense = expense;

            InitializeComponent();
            ApplyClientStyles();
            SetupUI();
            WireEvents();
        }

        // Chuẩn hóa màu sắc, font, và kiểu dáng theo bộ UIHelper của dự án
        private void ApplyClientStyles()
        {
            // ── Header ────────────────────────────────────────────────────────
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelAccentLine.BackColor = UIHelper.ColorPrimary;
            lblTitleForm.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitleForm.ForeColor = UIHelper.ColorHeaderFg;

            // ── Body / nền form ───────────────────────────────────────────────
            panelBody.BackColor = UIHelper.ColorBackground;
            tableLayout.BackColor = UIHelper.ColorBackground;

            // ── Labels ────────────────────────────────────────────────────────
            foreach (Label lbl in new[] { lblProject, lblType, lblAmount, lblDate, lblNote })
            {
                lbl.Font = UIHelper.FontLabel;
                lbl.ForeColor = UIHelper.ColorDark;
            }

            // ── Inputs ────────────────────────────────────────────────────────
            foreach (ComboBox cbo in new[] { cboProject, cboType })
            {
                cbo.Font = UIHelper.FontBase;
                cbo.ForeColor = UIHelper.ColorTextPrimary;
                cbo.BackColor = UIHelper.ColorSurface;
            }

            numAmount.Font = UIHelper.FontBase;
            numAmount.ForeColor = UIHelper.ColorTextPrimary;
            numAmount.BackColor = UIHelper.ColorSurface;

            dtpDate.Font = UIHelper.FontBase;
            dtpDate.CalendarForeColor = UIHelper.ColorTextPrimary;
            dtpDate.CalendarMonthBackground = UIHelper.ColorSurface;

            txtNote.Font = UIHelper.FontBase;
            txtNote.ForeColor = UIHelper.ColorTextPrimary;
            txtNote.BackColor = UIHelper.ColorSurface;
            txtNote.BorderStyle = BorderStyle.FixedSingle;

            // ── Footer ────────────────────────────────────────────────────────
            panelFooter.BackColor = UIHelper.ColorBackground;
            panelFooterLine.BackColor = UIHelper.ColorBorderLight;
            flowButtons.BackColor = UIHelper.ColorBackground;

            lblError.Font = UIHelper.FontSmall;
            lblError.ForeColor = UIHelper.ColorDanger;

            UIHelper.StyleButton(btnSave, UIHelper.ButtonVariant.Primary);
            UIHelper.StyleButton(btnCancel, UIHelper.ButtonVariant.Secondary);
        }

        private void SetupUI()
        {
            cboType.Items.Clear();
            cboType.Items.AddRange(new object[] { "Nhân công", "Phần mềm", "Hạ tầng", "Khác" });

            numAmount.ThousandsSeparator = true;
            numAmount.DecimalPlaces = 0;
            numAmount.Maximum = 1_000_000_000_000m;

            if (_editingExpense != null)
            {
                lblTitleForm.Text = "✏️  Sửa chi phí";
                this.Text = "Sửa chi phí";
                btnSave.Text = "💾  Cập nhật";
            }
            else
            {
                lblTitleForm.Text = "➕  Thêm chi phí mới";
                this.Text = "Thêm chi phí";
                btnSave.Text = "💾  Lưu";
                dtpDate.Value = DateTime.Today;
            }
        }

        private void WireEvents()
        {
            this.Load += async (s, e) =>
            {
                await LoadProjectsAsync();
                if (_editingExpense != null) BindData();
            };

            btnSave.Click += async (s, e) => await SaveAsync();
            btnCancel.Click += (s, e) => this.Close();
        }

        private async Task LoadProjectsAsync()
        {
            try
            {
                var projects = await _projectService.GetProjectsForUserAsync(
                    AppSession.UserId,
                    AppSession.IsManager || AppSession.IsAdmin);

                cboProject.Items.Clear();
                foreach (var p in projects)
                    cboProject.Items.Add(new ComboItem(p.Id, p.Name));

                if (cboProject.Items.Count > 0)
                    cboProject.SelectedIndex = 0;

                cboProject.AdjustDropDownWidth();
                cboType.AdjustDropDownWidth();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải dự án: " + ex.Message);
            }
        }

        private void BindData()
        {
            if (_editingExpense == null) return;

            for (int i = 0; i < cboProject.Items.Count; i++)
            {
                if ((cboProject.Items[i] as ComboItem)?.Id == _editingExpense.ProjectId)
                {
                    cboProject.SelectedIndex = i;
                    break;
                }
            }

            cboType.SelectedItem = _editingExpense.ExpenseType;
            numAmount.Value = _editingExpense.Amount;
            dtpDate.Value = _editingExpense.ExpenseDate.ToDateTime(TimeOnly.MinValue);
            txtNote.Text = _editingExpense.Note;
        }

        private async Task SaveAsync()
        {
            if (cboProject.SelectedItem == null) { ShowError("Vui lòng chọn dự án."); return; }
            if (cboType.SelectedItem == null) { ShowError("Vui lòng chọn loại chi phí."); return; }
            if (numAmount.Value <= 0) { ShowError("Số tiền phải lớn hơn 0."); return; }
            if (dtpDate.Value.Date > DateTime.Today) { ShowError("Ngày chi phí không được lớn hơn ngày hiện tại."); return; }

            lblError.Text = "";
            btnSave.Enabled = false;

            try
            {
                var expense = _editingExpense ?? new Expense();
                expense.ProjectId = (cboProject.SelectedItem as ComboItem)!.Id;
                expense.ExpenseType = cboType.SelectedItem.ToString()!;
                expense.Amount = numAmount.Value;
                expense.ExpenseDate = DateOnly.FromDateTime(dtpDate.Value);
                expense.Note = txtNote.Text.Trim();

                if (_editingExpense == null)
                {
                    expense.CreatedById = AppSession.UserId;
                    var (ok, msg) = await _expenseService.AddExpenseAsync(expense);
                    this.InvokeIfRequired(() =>
                    {
                        if (ok) { this.DialogResult = DialogResult.OK; this.Close(); }
                        else ShowError(msg);
                    });
                }
                else
                {
                    var (ok, msg) = await _expenseService.UpdateExpenseAsync(expense);
                    this.InvokeIfRequired(() =>
                    {
                        if (ok) { this.DialogResult = DialogResult.OK; this.Close(); }
                        else ShowError(msg);
                    });
                }
            }
            catch (Exception ex)
            {
                this.InvokeIfRequired(() => ShowError("Lỗi hệ thống: " + ex.Message));
            }
            finally
            {
                this.InvokeIfRequired(() => btnSave.Enabled = true);
            }
        }

        private void ShowError(string msg)
        {
            lblError.Text = "⚠️ " + msg;
            System.Media.SystemSounds.Beep.Play();
        }
    }
}