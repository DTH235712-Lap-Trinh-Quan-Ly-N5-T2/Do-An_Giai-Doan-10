using System.Reflection;
using System.Windows.Forms;

namespace TaskFlowManagement.WinForms.Common
{
    /// <summary>
    /// Các phương thức mở rộng (Extension Methods) tối ưu hóa UI cho DataGridView.
    /// </summary>
    public static class DataGridViewExtensions
    {
        /// <summary>
    /// Kích hoạt DoubleBuffered ẩn của DataGridView thông qua Reflection.
    /// Giúp loại bỏ hoàn toàn hiện tượng nháy hình (flickering) khi cuộn chuột qua lượng dữ liệu lớn.
        /// </summary>
        /// <param name="dgv">Target DataGridView</param>
        /// <param name="setting">Bật (true) hoặc Tắt (false)</param>
        public static void EnableDoubleBuffer(this DataGridView dgv, bool setting = true)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(dgv, setting, null);
            }
        }
    }
}
