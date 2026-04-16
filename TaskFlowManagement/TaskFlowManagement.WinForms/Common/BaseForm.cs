// ============================================================
//  BaseForm.cs  —  TaskFlowManagement.WinForms.Common
//  Tất cả MDI-child Form kế thừa class này.
//  Mục đích:
//    1. Bật DoubleBuffered → chống nháy hình
//    2. Set Font + BackColor chung 1 lần duy nhất
//    3. WS_EX_COMPOSITED để tránh flicker trên MDI container
// ============================================================
using System.Drawing;
using System.Windows.Forms;

namespace TaskFlowManagement.WinForms.Common
{
    /// <summary>
    /// Base form cho toàn bộ TaskFlow WinForms application.
    /// Inherit class này thay vì <see cref="Form"/> trực tiếp.
    /// <para>
    /// Cách dùng trong Designer.cs:
    /// <code>partial class frmProjects : BaseForm { ... }</code>
    /// </para>
    /// </summary>
    public class BaseForm : Form
    {
        public BaseForm()
        {
            // Anti-flicker: bật double buffering ngay từ constructor
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);
            this.UpdateStyles();

            // Chuẩn hóa DPI scaling — an toàn ở mọi context
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;

            // Lúc Designer load, LicenseManager.UsageMode == Designtime
            // → assembly chưa được build đầy đủ → gọi UIHelper ở đây gây crash Designer.
            if (!IsInDesignMode())
            {
                this.Font = UIHelper.FontBase;
                this.BackColor = UIHelper.ColorSurface;
            }
        }

        /// <summary>
        /// WS_EX_COMPOSITED: giảm flickering cho MDI child forms
        /// khi có nhiều control Dock / Fill.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        /// <summary>
        /// Kiểm tra an toàn hơn <see cref="Component.DesignMode"/>:
        /// DesignMode built-in chỉ trả về true khi control đã được add vào
        /// designer surface — constructor chạy TRƯỚC lúc đó nên luôn false.
        /// LicenseManager.UsageMode là cách duy nhất đáng tin trong constructor.
        /// </summary>
        private static bool IsInDesignMode()
            => System.ComponentModel.LicenseManager.UsageMode
               == System.ComponentModel.LicenseUsageMode.Designtime;
    }
}