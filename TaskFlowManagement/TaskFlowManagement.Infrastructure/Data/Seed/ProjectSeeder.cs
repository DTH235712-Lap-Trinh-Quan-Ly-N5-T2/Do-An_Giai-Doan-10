using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seed dữ liệu mẫu cho Customer và Project.
    /// Nhận danh sách Users đã lưu để gán OwnerId đúng.
    /// </summary>
    internal static class ProjectSeeder
    {
        internal static List<Customer> GetCustomers() => new()
        {
            new() { CompanyName = "FPT Software",        ContactName = "Nguyễn Minh Tuấn", Email = "contact@fpt.com",       Phone = "0901234001" },
            new() { CompanyName = "VNG Corporation",     ContactName = "Trần Thị Hương",   Email = "contact@vng.com.vn",    Phone = "0901234002" },
            new() { CompanyName = "VNPT Technology",     ContactName = "Lê Văn Đức",       Email = "contact@vnpt-it.com.vn",Phone = "0901234003" },
            new() { CompanyName = "Tiki Corporation",    ContactName = "Phạm Thị Thu",     Email = "contact@tiki.vn",       Phone = "0901234004" },
            new() { CompanyName = "Momo E-Wallet",       ContactName = "Hoàng Văn Long",   Email = "contact@momo.vn",       Phone = "0901234005" },
        };

        internal static List<Project> GetProjects(
            List<User> users, List<Customer> customers)
        {
            var m1 = users.First(u => u.Username == "manager1");
            var m2 = users.First(u => u.Username == "manager2");
            var m3 = users.First(u => u.Username == "manager3");
            var managerIds = new[] { m1.Id, m2.Id, m3.Id };

            var now = DateTime.UtcNow;
            var rng = new Random(42);
            var projects = new List<Project>();

            // 5 Dự án mẫu cốt lõi (Hardcoded để demo ý nghĩa)
            projects.Add(new Project {
                Name = "Hệ thống quản lý nhân sự FPT",
                Description = "Xây dựng phần mềm quản lý nhân sự cho FPT Software",
                OwnerId = m1.Id, CustomerId = customers[0].Id,
                StartDate = DateOnly.FromDateTime(now.AddMonths(-3)), PlannedEndDate = DateOnly.FromDateTime(now.AddMonths(3)),
                Status = "InProgress", Priority = 3, Budget = 500_000_000,
                CreatedAt = now.AddMonths(-3), UpdatedAt = now
            });
            projects.Add(new Project {
                Name = "App thanh toán VNG Pay",
                Description = "Phát triển ứng dụng thanh toán di động cho VNG",
                OwnerId = m1.Id, CustomerId = customers[1].Id,
                StartDate = DateOnly.FromDateTime(now.AddMonths(-2)), PlannedEndDate = DateOnly.FromDateTime(now.AddMonths(4)),
                Status = "InProgress", Priority = 4, Budget = 800_000_000,
                CreatedAt = now.AddMonths(-2), UpdatedAt = now
            });
            
            // Một Dự án CỐ TÌNH tạo Tên Dài (Phá Form Stress Test)
            projects.Add(new Project {
                Name = "Dự_Án_Demo_Nâng_Cấp_Lõi_Ngân_Hàng_Tích_Hợp_AI_Khong_Khoang_Trang", // < 100 chars
                Description = "Dự án này cố tình có tên dài và không chứa một khoảng trắng nào để ép tất cả các thuộc tính WrapMode của DataGridView, ToolTip, Label và ComboBox phải bộc lộ điểm yếu hoặc tự động điều chỉnh.\n\nDòng 2 đoạn mô tả: Test xuống dòng.\n\nDòng 3: Đảm bảo LayoutPanel chống tràn.",
                OwnerId = m2.Id, CustomerId = customers[2].Id,
                StartDate = DateOnly.FromDateTime(now.AddMonths(-1)), PlannedEndDate = DateOnly.FromDateTime(now.AddMonths(5)),
                Status = "NotStarted", Priority = 2, Budget = 300_000_000, 
                CreatedAt = now.AddMonths(-1), UpdatedAt = now
            });

            // Sinh random thêm 22 Dự án để ép hiển thị Scrollbar (Tổng 25 Projects)
            var projectPrefixes = new[] { "Triển khai", "Tích hợp", "Nâng cấp", "Xây dựng", "Kiểm thử", "Bảo trì", "Tối ưu hóa ERP", "Chuyển đổi số" };
            var projectSuffixes = new[] { "Module Kế toán", "Hệ thống CRM", "Ứng dụng Mobile", "Cổng thông tin", "Hệ thống AI cốt lõi", "Bảo mật mạng" };

            for (int i = 4; i <= 25; i++)
            {
                var pfx = projectPrefixes[rng.Next(projectPrefixes.Length)];
                var sfx = projectSuffixes[rng.Next(projectSuffixes.Length)];
                var startOffset = rng.Next(-6, 2);
                var duration = rng.Next(2, 6);

                projects.Add(new Project
                {
                    Name = $"{pfx} {sfx} - Giai đoạn {i}",
                    Description = $"Mô tả tự động sinh cho dự án số {i}: {pfx} {sfx} đảm bảo hiệu suất và tiến độ.\n\nTest ký tự đặc biệt: < ! @ # $ % ^ & * >",
                    OwnerId = managerIds[rng.Next(managerIds.Length)],
                    CustomerId = customers[rng.Next(customers.Count)].Id,
                    StartDate = DateOnly.FromDateTime(now.AddMonths(startOffset)),
                    PlannedEndDate = DateOnly.FromDateTime(now.AddMonths(startOffset + duration)),
                    Status = rng.Next(100) > 80 ? "NotStarted" : (rng.Next(100) > 20 ? "InProgress" : "Completed"),
                    Priority = (byte)rng.Next(1, 5),
                    Budget = rng.Next(50, 1500) * 1_000_000m, // Ngân sách từ 50tr - 1.5 tỷ
                    CreatedAt = now.AddMonths(startOffset),
                    UpdatedAt = now
                });
            }

            return projects;
        }
    }
}
