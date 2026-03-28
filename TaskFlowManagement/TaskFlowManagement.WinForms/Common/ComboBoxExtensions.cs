using System;
using System.Drawing;
using System.Windows.Forms;

namespace TaskFlowManagement.WinForms.Common
{
    public static class ComboBoxExtensions
    {
        /// <summary>
        /// Tự động tính toán và mở rộng chiều ngang của DropDown (danh sách xổ xuống) 
        /// để hiển thị đầy đủ text của item dài nhất mà không bị cắt hoặc tràn.
        /// Thường gọi sau khi đã Binding dữ liệu vào ComboBox.
        /// </summary>
        public static void AdjustDropDownWidth(this ComboBox comboBox)
        {
            if (comboBox.Items.Count == 0) return;

            int maxWidth = comboBox.DropDownWidth;
            using (Graphics g = comboBox.CreateGraphics())
            {
                // Đo chiều rộng của từng phần tử
                foreach (var item in comboBox.Items)
                {
                    string text = comboBox.GetItemText(item);
                    int currentWidth = (int)g.MeasureString(text, comboBox.Font).Width;
                    
                    // Nếu lớn hơn maxWidth hiện tại thì cập nhật
                    if (currentWidth > maxWidth)
                    {
                        maxWidth = currentWidth;
                    }
                }
            }

            // Thêm một khoản bù đắp (padding) cho thanh cuộn dọc (Scrollbar) nếu hiển thị nhiều dòng
            int padding = SystemInformation.VerticalScrollBarWidth + 10;
            comboBox.DropDownWidth = maxWidth + padding;
        }
    }
}
