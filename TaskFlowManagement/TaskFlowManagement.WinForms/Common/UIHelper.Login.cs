// ============================================================
//  UIHelper.Login.cs  —  TaskFlowManagement.WinForms.Common
//  Partial extension: token màu và font dành riêng cho frmLogin.
//  Tách file riêng để không làm bẩn UIHelper gốc.
// ============================================================
using System.Drawing;

namespace TaskFlowManagement.WinForms.Common
{
    public static partial class UIHelper
    {
        // ── Màu nền & viền input ──────────────────────────────────────────────
        public static readonly Color ColorInputBackground = Color.FromArgb(248, 250, 252); // slate-50
        public static readonly Color ColorBorder = Color.FromArgb(226, 232, 240); // slate-200

        // ── Màu text ─────────────────────────────────────────────────────────
        public static readonly Color ColorTextDark = Color.FromArgb(15, 23, 42);  // slate-950
        public static readonly Color ColorTextOnDark = Color.White;
        public static readonly Color TextMuted = Color.FromArgb(176, 200, 230); // custom slate-light (panel trái)

        // ── Màu icon trong input ──────────────────────────────────────────────
        public static readonly Color ColorIconMuted = Color.FromArgb(148, 163, 184); // slate-400

        // ── Màu accent bổ sung ────────────────────────────────────────────────
        public static readonly Color ColorAccentTeal = Color.FromArgb(6, 182, 212);  // cyan-500
        public static readonly Color ColorAccentSky = Color.FromArgb(56, 189, 248); // sky-400
        public static readonly Color ColorPrimaryLight = Color.FromArgb(219, 234, 254); // blue-100
        public static readonly Color ColorPrimaryDark = Color.FromArgb(30, 64, 175);  // blue-800

        // ── Màu badge ────────────────────────────────────────────────────────
        public static readonly Color ColorBadgeBackground = Color.FromArgb(20, 37, 99, 235); // blue-600 @ 8% opacity

        // ── Màu lỗi (alias rõ nghĩa hơn ColorDanger) ─────────────────────────
        public static readonly Color ColorError = Color.FromArgb(220, 38, 38); // red-600

        // ── Font dành cho frmLogin ────────────────────────────────────────────
        public static readonly Font FontHeadingLarge = new("Segoe UI", 20F, FontStyle.Bold);
        public static readonly Font FontHeadingMedium = new("Segoe UI", 22F, FontStyle.Bold);
        public static readonly Font FontBody = new("Segoe UI", 9.5F);
        public static readonly Font FontInput = new("Segoe UI", 11F);
        public static readonly Font FontLabelBold = new("Segoe UI", 8.5F, FontStyle.Bold);
        public static readonly Font FontButtonBold = new("Segoe UI", 11F, FontStyle.Bold);
    }
}