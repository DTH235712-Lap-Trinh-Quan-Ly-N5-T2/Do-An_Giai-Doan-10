// ============================================================
//  UIHelper.cs  —  TaskFlowManagement.WinForms.Common
//  Tất cả màu sắc, font, style được định nghĩa TẠI ĐÂY.
//  Không được hard-code Color/Font trong bất kỳ Form nào nữa.
// ============================================================
using System.Drawing;
using System.Windows.Forms;

namespace TaskFlowManagement.WinForms.Common
{
    public static partial class UIHelper
    {
        // ── Color Palette ─────────────────────────────────────────────────────
        public static readonly Color ColorSurface = Color.White;
        public static readonly Color ColorBackground = Color.FromArgb(241, 245, 249); // slate-100
        public static readonly Color ColorHeaderBg = Color.FromArgb(15, 23, 42);  // slate-900
        public static readonly Color ColorHeaderFg = Color.White;
        public static readonly Color ColorSubtitle = Color.FromArgb(148, 163, 184); // slate-400
        public static readonly Color ColorMuted = Color.FromArgb(100, 116, 139); // slate-500
        public static readonly Color ColorBorderLight = Color.FromArgb(226, 232, 240); // slate-200
        public static readonly Color ColorDark = Color.FromArgb(30, 41, 59);  // slate-800
        public static readonly Color ColorTextPrimary = Color.FromArgb(30, 41, 59);  // slate-800

        // Alias ngữ nghĩa
        public static readonly Color ColorTextSecondary = Color.FromArgb(100, 116, 139); // slate-500
        public static readonly Color ColorAccent = Color.FromArgb(37, 99, 235); // blue-600

        // Button / Accent colors
        public static readonly Color ColorPrimary = Color.FromArgb(37, 99, 235); // blue-600
        public static readonly Color ColorPrimaryHover = Color.FromArgb(29, 78, 216); // blue-700
        public static readonly Color ColorSuccess = Color.FromArgb(5, 150, 105); // emerald-600
        public static readonly Color ColorSuccessHover = Color.FromArgb(4, 120, 87);  // emerald-700
        public static readonly Color ColorDanger = Color.FromArgb(220, 38, 38);  // red-600
        public static readonly Color ColorDangerHover = Color.FromArgb(185, 28, 28);  // red-700
        public static readonly Color ColorWarning = Color.FromArgb(245, 158, 11);  // amber-500
        public static readonly Color ColorWarningHover = Color.FromArgb(217, 119, 6);   // amber-600
        public static readonly Color ColorInfo = Color.FromArgb(14, 116, 144); // cyan-700
        public static readonly Color ColorInfoHover = Color.FromArgb(12, 94, 116);
        public static readonly Color ColorPurple = Color.FromArgb(124, 58, 237); // violet-600
        public static readonly Color ColorPurpleHover = Color.FromArgb(109, 40, 217); // violet-700
        public static readonly Color ColorSlate = Color.FromArgb(71, 85, 105); // slate-600

        // DataGridView selection
        public static readonly Color ColorSelectionBg = Color.FromArgb(219, 234, 254); // blue-100
        public static readonly Color ColorSelectionFg = Color.FromArgb(30, 58, 138); // blue-900

        // Semantic row colors
        public static readonly Color ColorRowOverdue = Color.FromArgb(185, 28, 28);
        public static readonly Color ColorRowCompleted = Color.FromArgb(5, 150, 105);
        public static readonly Color ColorRowCancelled = Color.FromArgb(148, 163, 184);
        public static readonly Color ColorRowInProgress = Color.FromArgb(37, 99, 235);
        public static readonly Color ColorRowReview = Color.FromArgb(180, 83, 9);
        public static readonly Color ColorRowInTest = Color.FromArgb(109, 40, 217);

        // ── Kanban Column Colors ──────────────────────────────────────────────
        // To Do  (slate)
        public static readonly Color ColorColumnTodoBg = Color.FromArgb(226, 232, 240); // slate-200
        public static readonly Color ColorColumnTodoFg = Color.FromArgb(51, 65, 85);  // slate-700
        public static readonly Color ColorColumnTodoBody = Color.FromArgb(248, 250, 252); // slate-50

        // In Progress  (blue)
        public static readonly Color ColorColumnInProgressBg = Color.FromArgb(191, 219, 254); // blue-200
        public static readonly Color ColorColumnInProgressFg = Color.FromArgb(30, 58, 138); // blue-900
        public static readonly Color ColorColumnInProgressBody = Color.FromArgb(239, 246, 255); // blue-50

        // Review  (amber)
        public static readonly Color ColorColumnReviewBg = Color.FromArgb(253, 230, 138); // amber-200
        public static readonly Color ColorColumnReviewFg = Color.FromArgb(120, 53, 15);  // amber-900
        public static readonly Color ColorColumnReviewBody = Color.FromArgb(255, 251, 235); // amber-50

        // Testing  (violet)
        public static readonly Color ColorColumnTestingBg = Color.FromArgb(221, 214, 254); // violet-200
        public static readonly Color ColorColumnTestingFg = Color.FromArgb(76, 29, 149); // violet-900
        public static readonly Color ColorColumnTestingBody = Color.FromArgb(245, 243, 255); // violet-50

        // Failed  (red)
        public static readonly Color ColorColumnFailedBg = Color.FromArgb(254, 202, 202); // red-200
        public static readonly Color ColorColumnFailedFg = Color.FromArgb(127, 29, 29);  // red-900
        public static readonly Color ColorColumnFailedBody = Color.FromArgb(254, 242, 242); // red-50

        // Done  (emerald)
        public static readonly Color ColorColumnDoneBg = Color.FromArgb(167, 243, 208); // emerald-200
        public static readonly Color ColorColumnDoneFg = Color.FromArgb(6, 78, 59);  // emerald-900
        public static readonly Color ColorColumnDoneBody = Color.FromArgb(236, 253, 245); // emerald-50

        // ── Fonts ─────────────────────────────────────────────────────────────
        public static readonly Font FontBase = new("Segoe UI", 9.5F);
        public static readonly Font FontSmall = new("Segoe UI", 9F);
        public static readonly Font FontBold = new("Segoe UI", 9F, FontStyle.Bold);
        public static readonly Font FontLabel = new("Segoe UI", 8.5F, FontStyle.Bold);
        public static readonly Font FontHeaderLarge = new("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font FontGridHeader = new("Segoe UI", 9F, FontStyle.Bold);
        public static readonly Font FontColumnHeader = new("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font FontBadge = new("Segoe UI", 8.5F, FontStyle.Bold);

        // ── Button Variants ───────────────────────────────────────────────────
        public enum ButtonVariant
        {
            Primary, Success, Danger, Warning, Info, Purple, Slate, Secondary
        }

        public static void StyleButton(Button btn, ButtonVariant variant = ButtonVariant.Primary)
        {
            var (bg, hover, fg) = variant switch
            {
                ButtonVariant.Success => (ColorSuccess, ColorSuccessHover, Color.White),
                ButtonVariant.Danger => (ColorDanger, ColorDangerHover, Color.White),
                ButtonVariant.Warning => (ColorWarning, ColorWarningHover, Color.White),
                ButtonVariant.Info => (ColorInfo, ColorInfoHover, Color.White),
                ButtonVariant.Purple => (ColorPurple, ColorPurpleHover, Color.White),
                ButtonVariant.Slate => (ColorSlate, Color.FromArgb(51, 65, 85), Color.White),
                ButtonVariant.Secondary => (ColorSurface, ColorBorderLight, ColorSlate),
                _ => (ColorPrimary, ColorPrimaryHover, Color.White),
            };

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.MouseOverBackColor = hover;
            btn.BackColor = bg;
            btn.ForeColor = fg;
            btn.Font = FontBold;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;

            if (variant == ButtonVariant.Secondary)
            {
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = ColorBorderLight;
            }
            else
            {
                btn.FlatAppearance.BorderSize = 0;
            }
        }

        public static void StyleToolButton(
            Button btn, string text, ButtonVariant variant,
            int x, int y, int w, int h)
        {
            StyleButton(btn, variant);
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(w, h);
        }

        // ── DataGridView ──────────────────────────────────────────────────────
        public static void StyleDataGridView(DataGridView dgv)
        {
            // Bật DoubleBuffered (ẩn) qua Reflection để chống màn hình DataGridView chớp nháy (flicker) khi bind dữ liệu.
            var propInfo = typeof(DataGridView).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            propInfo?.SetValue(dgv, true, null);

            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.BackgroundColor = ColorSurface;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Font = FontBase;
            dgv.GridColor = ColorBackground;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowTemplate.Height = 38;
            dgv.DefaultCellStyle.Padding = new Padding(6, 0, 0, 0);

            var hs = dgv.ColumnHeadersDefaultCellStyle;
            hs.BackColor = ColorHeaderBg;
            hs.ForeColor = ColorHeaderFg;
            hs.Font = FontGridHeader;
            hs.Padding = new Padding(6, 0, 0, 0);

            dgv.DefaultCellStyle.SelectionBackColor = ColorSelectionBg;
            dgv.DefaultCellStyle.SelectionForeColor = ColorSelectionFg;
        }

        public static void ApplyAlternateRowColors(DataGridView dgv)
        {
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = ColorSelectionBg;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = ColorSelectionFg;
        }

        // ── Row Color Helpers ─────────────────────────────────────────────────
        public static void ApplyProjectRowStyle(
            DataGridViewRow row, string? status, DateOnly? plannedEndDate)
        {
            if (status == "Completed")
                row.DefaultCellStyle.ForeColor = ColorSuccess;
            else if (status == "Cancelled")
            {
                row.DefaultCellStyle.ForeColor = ColorRowCancelled;
                row.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            }
            else if (plannedEndDate.HasValue
                && plannedEndDate.Value < DateOnly.FromDateTime(DateTime.Now))
                row.DefaultCellStyle.ForeColor = ColorDanger;
        }

        public static void ApplyTaskRowStyle(
            DataGridViewRow row, string? statusName, bool isCompleted, DateTime? dueDate)
        {
            if (dueDate.HasValue && dueDate.Value < DateTime.UtcNow && !isCompleted)
            {
                row.DefaultCellStyle.ForeColor = ColorRowOverdue;
                return;
            }

            row.DefaultCellStyle.ForeColor = statusName switch
            {
                "CLOSED" => ColorRowCompleted,
                "RESOLVED" => Color.FromArgb(22, 163, 74),
                "FAILED" => ColorDanger,
                "IN-PROGRESS" => ColorRowInProgress,
                "REVIEW-1" or "REVIEW-2" => ColorRowReview,
                "IN-TEST" => ColorRowInTest,
                _ => ColorDark,
            };
        }

        // ── Format Helpers ────────────────────────────────────────────────────
        public static string FormatProjectStatus(string? status) => status switch
        {
            "NotStarted" => "📋 Chưa bắt đầu",
            "InProgress" => "🔄 Đang thực hiện",
            "OnHold" => "⏸ Tạm dừng",
            "Completed" => "✅ Hoàn thành",
            "Cancelled" => "❌ Đã hủy",
            _ => status ?? "—",
        };

        // ── Panel Factory Methods ─────────────────────────────────────────────
        public static Panel CreateHeaderPanel(string title, string subtitle)
        {
            var panel = new Panel { BackColor = ColorHeaderBg, Dock = DockStyle.Top, Height = 68 };

            var lblTitle = new Label
            {
                AutoSize = false,
                Font = FontHeaderLarge,
                ForeColor = ColorHeaderFg,
                Location = new Point(20, 10),
                Size = new Size(800, 30),
                Text = title,
            };
            var lblSub = new Label
            {
                AutoSize = false,
                Font = FontSmall,
                ForeColor = ColorSubtitle,
                Location = new Point(22, 44),
                Size = new Size(800, 18),
                Text = subtitle,
            };

            panel.Controls.AddRange(new Control[] { lblTitle, lblSub });
            return panel;
        }

        public static Panel CreateToolbarPanel(int height = 52)
            => new Panel
            {
                BackColor = Color.FromArgb(248, 250, 252),
                Dock = DockStyle.Top,
                Height = height,
            };

        public static (Panel Panel, Label Label) CreateStatusBar()
        {
            var panel = new Panel { BackColor = ColorHeaderBg, Dock = DockStyle.Bottom, Height = 28 };
            var lbl = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = FontSmall,
                ForeColor = ColorSubtitle,
                Padding = new Padding(12, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft,
            };
            panel.Controls.Add(lbl);
            return (panel, lbl);
        }

        // ── TextBox / ComboBox ────────────────────────────────────────────────
        public static void StyleSearchBox(TextBox txt, string placeholder = "🔍  Tìm kiếm...")
        {
            txt.Font = new Font("Segoe UI", 10F);
            txt.PlaceholderText = placeholder;
            txt.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void StyleFilterCombo(ComboBox cbo)
        {
            cbo.Font = new Font("Segoe UI", 9.5F);
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.FlatStyle = FlatStyle.Flat;
        }
    }
}