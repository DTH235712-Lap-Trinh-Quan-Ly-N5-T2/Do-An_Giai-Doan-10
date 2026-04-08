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

        // ── Constructor rỗng: CHỈ dùng cho WinForms Designer ─────────────────
        public frmExpenseEdit()
        {
            InitializeComponent();
        }

        // ── DI Constructor: dùng khi chạy thật ───────────────────────────────
        public frmExpenseEdit(IExpenseService expenseService, IProjectService projectService, Expense? expense = null)
        {
            _expenseService = expenseService;
            _projectService = projectService;
            _editingExpense = expense;

            InitializeComponent();
            ApplyClientStyles();
            SetupUI();
            WireEvents();
        }

        // ── Tất cả UIHelper / helper method được tách ra khỏi Designer ───────
        private void ApplyClientStyles()
        {
            // ── panelHeader ───────────────────────────────────────────────────
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelAccentLine.BackColor = UIHelper.ColorPrimary;
            lblTitleForm.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitleForm.ForeColor = UIHelper.ColorHeaderFg;

            // ── panelBody ─────────────────────────────────────────────────────
            panelBody.BackColor = UIHelper.ColorBackground;

            int y = 20, gap = 60, lx = 20, tw = 360;

            ApplyLabel(lblProject, "DỰ ÁN *", lx, y);
            ApplyCombo(cboProject, lx, y + 20, tw, 1);

            y += gap;
            ApplyLabel(lblType, "LOẠI CHI PHÍ *", lx, y);
            ApplyCombo(cboType, lx, y + 20, tw, 2);
            cboType.Items.Clear();
            cboType.Items.AddRange(new object[] { "Nhân công", "Phần mềm", "Hạ tầng", "Khác" });

            y += gap;
            ApplyLabel(lblAmount, "SỐ TIỀN (VNĐ) *", lx, y);
            numAmount.Location = new Point(lx, y + 20);
            numAmount.Size = new Size(tw, 30);
            numAmount.Maximum = 999999999999;
            numAmount.ThousandsSeparator = true;
            numAmount.Font = UIHelper.FontBase;
            numAmount.TabIndex = 3;

            y += gap;
            ApplyLabel(lblDate, "NGÀY PHÁT SINH", lx, y);
            dtpDate.Location = new Point(lx, y + 20);
            dtpDate.Size = new Size(tw, 30);
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Font = UIHelper.FontBase;
            dtpDate.TabIndex = 4;

            y += gap;
            ApplyLabel(lblNote, "GHI CHÚ", lx, y);
            txtNote.Location = new Point(lx, y + 20);
            txtNote.Size = new Size(tw, 80);
            txtNote.Multiline = true;
            txtNote.Font = UIHelper.FontBase;
            txtNote.BorderStyle = BorderStyle.FixedSingle;
            txtNote.PlaceholderText = "Nhập ghi chú (nếu có)...";
            txtNote.TabIndex = 5;

            // ── panelFooter ───────────────────────────────────────────────────
            panelFooterLine.BackColor = UIHelper.ColorBorderLight;
            lblError.ForeColor = UIHelper.ColorDanger;
            lblError.Font = UIHelper.FontSmall;

            UIHelper.StyleButton(btnSave, UIHelper.ButtonVariant.Primary);
            UIHelper.StyleButton(btnCancel, UIHelper.ButtonVariant.Secondary);
        }

        // Helpers dùng riêng trong ApplyClientStyles — KHÔNG đặt trong Designer
        private void ApplyLabel(Label lbl, string text, int x, int y)
        {
            lbl.AutoSize = true;
            lbl.Font = UIHelper.FontLabel;
            lbl.Location = new Point(x, y);
            lbl.Text = text;
        }

        private void ApplyCombo(ComboBox cbo, int x, int y, int w, int tabIdx)
        {
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.Font = UIHelper.FontBase;
            cbo.Location = new Point(x, y);
            cbo.Size = new Size(w, 30);
            cbo.TabIndex = tabIdx;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Phần còn lại: giữ nguyên hoàn toàn
        // ─────────────────────────────────────────────────────────────────────

        private void SetupUI()
        {
            if (_editingExpense != null)
            {
                lblTitleForm.Text = "✏️  Sửa chi phí";
                this.Text = "Sửa chi phí";
                btnSave.Text = "💾  Cập nhật chi phí";
            }
            else
            {
                lblTitleForm.Text = "➕  Thêm chi phí mới";
                this.Text = "Thêm chi phí";
                btnSave.Text = "💾  Lưu chi phí";
                dtpDate.Value = DateTime.Today;
            }

            numAmount.ThousandsSeparator = true;
            numAmount.DecimalPlaces = 0;
            numAmount.Maximum = 1000000000;
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
                    AppSession.UserId, AppSession.IsManager || AppSession.IsAdmin);

                cboProject.Items.Clear();
                foreach (var p in projects)
                    cboProject.Items.Add(new ComboItem(p.Id, p.Name));

                if (cboProject.Items.Count > 0) cboProject.SelectedIndex = 0;

                cboProject.AdjustDropDownWidth();
                cboType.AdjustDropDownWidth();
            }
            catch (Exception ex)
            {
                lblError.Text = "Lỗi tải dự án: " + ex.Message;
            }
        }

        private void BindData()
        {
            if (_editingExpense == null) return;

            for (int i = 0; i < cboProject.Items.Count; i++)
            {
                if ((cboProject.Items[i] as ComboItem)?.Id == _editingExpense.ProjectId)
                { cboProject.SelectedIndex = i; break; }
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
                    this.InvokeIfRequired(() => {
                        if (ok) { this.DialogResult = DialogResult.OK; this.Close(); }
                        else ShowError(msg);
                    });
                }
                else
                {
                    var (ok, msg) = await _expenseService.UpdateExpenseAsync(expense);
                    this.InvokeIfRequired(() => {
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