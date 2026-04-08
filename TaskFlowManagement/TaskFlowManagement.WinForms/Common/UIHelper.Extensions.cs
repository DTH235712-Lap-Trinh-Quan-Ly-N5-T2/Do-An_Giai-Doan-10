// ============================================================
//  UIHelper_Extensions.cs  —  TaskFlowManagement.WinForms.Common
//
//  Bổ sung method mới vào UIHelper (partial class trick).
//  Thêm ApplyTaskRowForeColor() để ucTaskCard.cs dùng được
//  mà không duplicate logic màu sắc.
//
//  ĐỂ TÍCH HỢP: Đổi tên file thành UIHelper.Extensions.cs và
//  thêm "partial" vào khai báo class UIHelper gốc:
//      public static partial class UIHelper { ... }
//  Sau đó class này sẽ compile chung với UIHelper.cs.
//
//  HOẶC đơn giản: copy nội dung static method bên dưới vào
//  cuối UIHelper.cs hiện có (trước dấu } đóng class).
// ============================================================
using System.Drawing;

namespace TaskFlowManagement.WinForms.Common
{
    // NOTE: Nếu dùng partial, đổi public static class → public static partial class ở UIHelper.cs gốc
    public static partial class UIHelper
    {
        /// <summary>
        /// Trả về màu ForeColor cho chip Status trên ucTaskCard.
        /// Tham số statusId thay vì statusName để tránh lookup string.
        /// Single source of truth — thay thế GetPriorityColor() cũ trong ucTaskCard.
        /// </summary>
        public static Color ApplyTaskRowForeColor(int statusId, bool isCompleted, DateTime? dueDate)
        {
            // Quá hạn ưu tiên cao nhất
            if (dueDate.HasValue && dueDate.Value < DateTime.UtcNow && !isCompleted)
                return ColorRowOverdue;

            return statusId switch
            {
                10 => ColorRowCompleted,                    // CLOSED
                9  => Color.FromArgb(22, 163, 74),          // RESOLVED green-600
                4  => ColorDanger,                          // FAILED
                3  => ColorRowInProgress,                   // IN-PROGRESS
                5 or 6 => ColorRowReview,                   // REVIEW-1/2
                8  => ColorRowInTest,                       // IN-TEST
                7  => Color.FromArgb(13, 148, 136),         // APPROVED teal-600
                _  => ColorDark                             // CREATED / ASSIGNED / default
            };
        }
    }
}
