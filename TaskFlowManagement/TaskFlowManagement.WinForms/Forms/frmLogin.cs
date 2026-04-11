using System.Text.Json;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmLogin : Form
    {
        // ── Constructor dành riêng cho WinForms Designer ──────────────────
        [Obsolete("Chỉ dùng cho WinForms Designer")]
        public frmLogin()
        {
            InitializeComponent();
        }

        // ── Fields ────────────────────────────────────────────────────────
        private readonly IAuthService _authService;
        private bool _passwordVisible = false;
        private System.Windows.Forms.Timer _loadingTimer = null!;
        private int _loadingWidth = 0;

        // ── Constructor chính với Dependency Injection ────────────────────
        public frmLogin(IAuthService authService)
        {
            _authService = authService;
            InitializeComponent();
            ApplyClientStyles();
            SetupLoadingTimer();
            LoadSavedUsername();

            btnEye.Click += BtnEye_Click;
        }

        // ── Thiết lập giao diện theo chuẩn màu Tailwind từ UIHelper ──────
        private void ApplyClientStyles()
        {
            // Nền tổng thể và panel trái
            this.BackColor = UIHelper.ColorHeaderBg;
            panelLeft.BackColor = UIHelper.ColorHeaderBg;

            // Panel phải: nền trắng — vùng form đăng nhập
            panelRight.BackColor = UIHelper.ColorSurface;

            // Logo và tên ứng dụng
            lblAppName.ForeColor = UIHelper.ColorTextOnDark;
            lblAppName.Font = UIHelper.FontHeadingLarge;

            // Badge phiên bản
            lblBadgeDot.ForeColor = UIHelper.ColorAccentTeal;
            lblBadgeText.ForeColor = UIHelper.ColorPrimaryLight;

            // Tagline — trắng/sky để phân tầng thị giác trên nền tối
            lblTagline.ForeColor = UIHelper.ColorTextOnDark;
            lblTagline.Font = UIHelper.FontHeadingMedium;
            lblTaglineSub.ForeColor = UIHelper.ColorAccentSky;
            lblTaglineSub.Font = UIHelper.FontHeadingMedium;

            // Gạch ngang trang trí
            panelAccent.BackColor = UIHelper.ColorPrimary;
            panelTitleLine.BackColor = UIHelper.ColorPrimary;

            // Danh sách tính năng — tableFeatures và từng ô
            tableFeatures.BackColor = UIHelper.ColorHeaderBg;
            lblFeatIcon1.ForeColor = UIHelper.ColorAccentTeal;
            lblFeatIcon2.ForeColor = UIHelper.ColorAccentTeal;
            lblFeatIcon3.ForeColor = UIHelper.ColorAccentTeal;
            lblFeat1.ForeColor = UIHelper.TextMuted;
            lblFeat2.ForeColor = UIHelper.TextMuted;
            lblFeat3.ForeColor = UIHelper.TextMuted;
            lblFeat1.Font = UIHelper.FontBody;
            lblFeat2.Font = UIHelper.FontBody;
            lblFeat3.Font = UIHelper.FontBody;

            // Tiêu đề và phụ đề panel phải
            lblTitle.ForeColor = UIHelper.ColorTextPrimary;
            lblTitle.Font = UIHelper.FontHeadingLarge;
            lblSubtitle.ForeColor = UIHelper.ColorMuted;
            lblSubtitle.Font = UIHelper.FontBody;

            // Nhãn input
            lblLabelUser.ForeColor = UIHelper.ColorTextSecondary;
            lblLabelUser.Font = UIHelper.FontLabelBold;
            lblLabelPass.ForeColor = UIHelper.ColorTextSecondary;
            lblLabelPass.Font = UIHelper.FontLabelBold;

            // Wrapper input — BackColor đồng nhất với TextBox để caret không bị lệch màu nền
            panelInputUser.BackColor = UIHelper.ColorInputBackground;
            panelInputPass.BackColor = UIHelper.ColorInputBackground;

            // TextBox: BackColor khớp với panel cha — điều kiện cần để caret render đúng
            txtUsername.BackColor = UIHelper.ColorInputBackground;
            txtUsername.ForeColor = UIHelper.ColorTextDark;
            txtUsername.Font = UIHelper.FontInput;

            txtPassword.BackColor = UIHelper.ColorInputBackground;
            txtPassword.ForeColor = UIHelper.ColorTextDark;
            txtPassword.Font = UIHelper.FontInput;

            // Icon trong input
            lblIconUser.ForeColor = UIHelper.ColorIconMuted;
            lblIconPass.ForeColor = UIHelper.ColorIconMuted;

            // Nút toggle hiện/ẩn mật khẩu
            btnEye.ForeColor = UIHelper.ColorIconMuted;

            // Checkbox ghi nhớ tài khoản
            chkRemember.ForeColor = UIHelper.ColorTextSecondary;
            chkRemember.Font = UIHelper.FontBody;

            // Thông báo lỗi xác thực
            lblError.ForeColor = UIHelper.ColorError;
            lblError.Font = UIHelper.FontBody;

            // Thanh tiến trình loading
            panelProgress.BackColor = UIHelper.ColorPrimaryLight;

            // Nút đăng nhập
            btnLogin.BackColor = UIHelper.ColorPrimary;
            btnLogin.ForeColor = UIHelper.ColorTextOnDark;
            btnLogin.Font = UIHelper.FontButtonBold;
            btnLogin.FlatAppearance.MouseOverBackColor = UIHelper.ColorPrimaryHover;
            btnLogin.FlatAppearance.MouseDownBackColor = UIHelper.ColorPrimaryDark;

            // Footer
            lblFooter.ForeColor = UIHelper.ColorMuted;
            lblFooter.Font = UIHelper.FontSmall;
        }

        // ── Khởi tạo bộ đếm thời gian cho animation thanh loading ────────
        private void SetupLoadingTimer()
        {
            _loadingTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _loadingTimer.Tick += LoadingTimer_Tick;
        }

        private void LoadingTimer_Tick(object? sender, EventArgs e)
        {
            _loadingWidth += 8;
            int maxWidth = panelProgress.Parent?.Width ?? 540;
            if (_loadingWidth >= maxWidth)
            {
                _loadingWidth = maxWidth;
                _loadingTimer.Stop();
            }
            panelProgress.Width = _loadingWidth;
            panelProgress.Invalidate();
        }

        // ── Khôi phục tên đăng nhập đã lưu từ phiên trước ───────────────
        private void LoadSavedUsername()
        {
            var saved = ReadSavedUsername();
            if (!string.IsNullOrEmpty(saved))
            {
                txtUsername.Text = saved;
                chkRemember.Checked = true;
                txtPassword.Focus();
            }
            else
            {
                txtUsername.Focus();
            }
        }

        // ── Event handlers ────────────────────────────────────────────────

        private void txtUsername_EnterLeave(object? sender, EventArgs e)
            => panelInputUser.Invalidate();

        private void txtPassword_EnterLeave(object? sender, EventArgs e)
            => panelInputPass.Invalidate();

        private void BtnEye_Click(object? sender, EventArgs e)
        {
            _passwordVisible = !_passwordVisible;
            txtPassword.UseSystemPasswordChar = !_passwordVisible;
            btnEye.Text = _passwordVisible ? "🙈" : "👁";
            txtPassword.Focus();
            txtPassword.SelectionStart = txtPassword.Text.Length;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin_Click(sender, e);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            btnLogin.Enabled = false;
            btnLogin.Text = "Đang xác thực...";

            _loadingWidth = 0;
            panelProgress.Width = 0;
            panelProgress.Visible = true;
            _loadingTimer.Start();

            try
            {
                var result = await _authService.LoginAsync(
                    txtUsername.Text.Trim(),
                    // Không .Trim() mật khẩu — BCrypt.Verify() so sánh ký tự chính xác
                    txtPassword.Text);

                _loadingTimer.Stop();
                panelProgress.Width = panelProgress.Parent?.Width ?? 540;
                panelProgress.Invalidate();
                await Task.Delay(150);

                if (!result.Success)
                {
                    panelProgress.Visible = false;
                    panelProgress.Width = 0;
                    lblError.Text = result.ErrorMessage ?? "Đăng nhập thất bại.";
                    txtPassword.Clear();
                    txtPassword.Focus();
                    await ShakePanelAsync(panelRight);
                    return;
                }

                AppSession.Login(result.User!);
                SaveUsername(chkRemember.Checked ? txtUsername.Text.Trim() : "");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                _loadingTimer.Stop();
                panelProgress.Visible = false;
                lblError.Text = $"⚠  Lỗi kết nối: {ex.Message}";
            }
            finally
            {
                // Chỉ reset button nếu form chưa đóng, tránh crash khi control đã disposed
                if (!this.IsDisposed)
                {
                    btnLogin.Enabled = true;
                    btnLogin.Text = "ĐĂNG NHẬP  →";
                }
            }
        }

        private async Task ShakePanelAsync(System.Windows.Forms.Panel panel)
        {
            int original = panel.Left;
            foreach (var offset in new[] { -6, 6, -5, 5, -3, 3, 0 })
            {
                if (panel.IsDisposed) return;
                panel.Left = original + offset;
                await Task.Delay(30);
            }
            if (!panel.IsDisposed) panel.Left = original;
        }

        // ── Paint events ──────────────────────────────────────────────────

        private void panelRing1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(40, UIHelper.ColorPrimary), 1f);
            e.Graphics.DrawEllipse(pen, 0, 0, panelRing1.Width - 1, panelRing1.Height - 1);
        }

        private void panelRing2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(35, UIHelper.ColorAccentTeal), 1f);
            e.Graphics.DrawEllipse(pen, 0, 0, panelRing2.Width - 1, panelRing2.Height - 1);
        }

        private void panelLogoBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, panelLogoBox.Width, panelLogoBox.Height);
            using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                rect, UIHelper.ColorPrimary, UIHelper.ColorAccentTeal, 135f);
            using var path = CreateRoundedPath(rect, 12);
            e.Graphics.FillPath(brush, path);
        }

        private void panelBadge_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, panelBadge.Width - 1, panelBadge.Height - 1);
            using var pen = new Pen(Color.FromArgb(70, UIHelper.ColorPrimary), 1f);
            using var path = CreateRoundedPath(rect, 13);
            e.Graphics.DrawPath(pen, path);
        }

        private void panelInput_Paint(object sender, PaintEventArgs e)
        {
            // Viền sáng lên khi TextBox bên trong đang được focus
            var panel = (System.Windows.Forms.Panel)sender;
            bool focused = panel.ContainsFocus;
            var borderColor = focused ? UIHelper.ColorPrimary : UIHelper.ColorBorder;
            float borderWidth = focused ? 2f : 1.5f;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            using var pen = new Pen(borderColor, borderWidth);
            using var path = CreateRoundedPath(rect, 10);
            e.Graphics.DrawPath(pen, path);
        }

        private void panelProgress_Paint(object sender, PaintEventArgs e)
        {
            if (panelProgress.Width <= 0) return;
            using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Rectangle(0, 0, panelProgress.Width, panelProgress.Height),
                UIHelper.ColorPrimary, UIHelper.ColorAccentTeal, 0f);
            e.Graphics.FillRectangle(brush, 0, 0, panelProgress.Width, panelProgress.Height);
        }

        private void btnLogin_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                btnLogin.ClientRectangle,
                UIHelper.ColorPrimary,
                UIHelper.ColorPrimaryHover,
                90f);
            using var path = CreateRoundedPath(btnLogin.ClientRectangle, 10);
            e.Graphics.FillPath(brush, path);

            using var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            e.Graphics.DrawString(
                btnLogin.Text, btnLogin.Font,
                Brushes.White,
                btnLogin.ClientRectangle, sf);
        }

        // ── Helper: GraphicsPath hình chữ nhật bo góc ────────────────────
        private static System.Drawing.Drawing2D.GraphicsPath CreateRoundedPath(
            Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.X + bounds.Width - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.X + bounds.Width - d, bounds.Y + bounds.Height - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // ── Lưu trữ tên đăng nhập giữa các phiên ────────────────────────
        private static readonly string SettingsPath =
            Path.Combine(AppContext.BaseDirectory, "user_prefs.json");

        private static string ReadSavedUsername()
        {
            try
            {
                if (!File.Exists(SettingsPath)) return string.Empty;
                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(
                    File.ReadAllText(SettingsPath));
                return dict?.GetValueOrDefault("SavedUsername") ?? string.Empty;
            }
            catch { return string.Empty; }
        }

        private static void SaveUsername(string username)
        {
            try
            {
                File.WriteAllText(SettingsPath,
                    JsonSerializer.Serialize(
                        new Dictionary<string, string> { ["SavedUsername"] = username }));
            }
            catch { /* Bỏ qua lỗi ghi file cài đặt người dùng */ }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _loadingTimer?.Stop();
            _loadingTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}