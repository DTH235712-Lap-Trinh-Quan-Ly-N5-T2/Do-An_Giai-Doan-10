using Microsoft.EntityFrameworkCore;
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Infrastructure.Data.Seed;

namespace TaskFlowManagement.Infrastructure.Data
{
    /// <summary>
    /// Orchestrator seed data – gọi các Seeder nhỏ theo đúng thứ tự FK.
    /// Hỗ trợ Resume-Seed nếu lần trước bị lỗi (Idempotent).
    /// </summary>
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // 1. Kiểm tra bảng Task: Nếu đã có dữ liệu thì coi như Seed hoàn tất
            if (await context.TaskItems.AnyAsync()) return;

            // 2. Kiểm tra bảng User: Nếu chưa có User thì Seed toàn bộ dữ liệu nền tảng
            if (!await context.Users.AnyAsync())
            {
                // LOOKUP TABLES
                var roles = LookupSeeder.GetRoles();
                var priorities = LookupSeeder.GetPriorities();
                var statuses = LookupSeeder.GetStatuses();
                var categories = LookupSeeder.GetCategories();
                var tags = LookupSeeder.GetTags();

                context.AddRange(roles);
                context.AddRange(priorities);
                context.AddRange(statuses);
                context.AddRange(categories);
                context.AddRange(tags);
                await context.SaveChangesAsync();

                // USERS
                var users = UserSeeder.GetUsers();
                context.AddRange(users);
                await context.SaveChangesAsync();

                // ROLES
                var adminRole = roles.First(r => r.Name == "Admin");
                var managerRole = roles.First(r => r.Name == "Manager");
                var devRole = roles.First(r => r.Name == "Developer");

                var userRoles = users.Select(u => new UserRole
                {
                    UserId = u.Id,
                    RoleId = u.Username switch
                    {
                        "admin" => adminRole.Id,
                        var n when n.StartsWith("manager") => managerRole.Id,
                        _ => devRole.Id
                    }
                }).ToList();
                context.AddRange(userRoles);
                await context.SaveChangesAsync();

                // CUSTOMERS & PROJECTS
                var customers = ProjectSeeder.GetCustomers();
                context.AddRange(customers);
                await context.SaveChangesAsync();

                var projects = ProjectSeeder.GetProjects(users, customers);
                context.AddRange(projects);
                await context.SaveChangesAsync();

                // MEMBERS
                var devUsersForMember = users.Where(u => u.Username.StartsWith("dev")).ToList();
                var rngMember = new Random(42);
                var members = new List<ProjectMember>();
                foreach (var project in projects)
                {
                    var assigned = devUsersForMember.OrderBy(_ => rngMember.Next()).Take(rngMember.Next(3, 6)).ToList();
                    members.AddRange(assigned.Select(dev => new ProjectMember
                    {
                        ProjectId = project.Id,
                        UserId = dev.Id,
                        ProjectRole = "Developer",
                        JoinedAt = project.CreatedAt
                    }));
                }
                context.AddRange(members);
                await context.SaveChangesAsync();
            }

            // 3. SEED TASK & EXPENSE (Nếu code chạy đến đây nghĩa là User/Project đã có nhưng Task chưa có)
            var currentProjects = await context.Projects.ToListAsync();
            var currentMembers = await context.ProjectMembers.ToListAsync();
            var currentUsers = await context.Users.ToListAsync();
            var currentPriorities = await context.Priorities.ToListAsync();
            var currentStatuses = await context.Statuses.ToListAsync();
            var currentCategories = await context.Categories.ToListAsync();
            var rngData = new Random(42);

            // Sinh TaskItems
            var taskList = BuildTaskItems(currentProjects, currentMembers, currentUsers, currentPriorities, currentStatuses, currentCategories, rngData);
            context.AddRange(taskList);
            await context.SaveChangesAsync();

            // Sinh Expenses
            var expenses = BuildExpenses(currentProjects, currentUsers, rngData);
            context.AddRange(expenses);
            await context.SaveChangesAsync();

            // Sinh Comments & Attachments cho một số task mẫu
            await SeedSocialData(context, taskList, currentUsers, rngData);
        }

        private static List<TaskItem> BuildTaskItems(
            List<Project> projects,
            List<ProjectMember> members,
            List<User> users,
            List<Priority> priorities,
            List<Status> statuses,
            List<Category> categories,
            Random rng)
        {
            var now = DateTime.UtcNow;
            var templates = new[]
            {
                ("Phân tích yêu cầu hệ thống", "Feature", "High"),
                ("Thiết kế database schema", "Feature", "High"),
                ("Cài đặt môi trường development", "Feature", "Medium"),
                ("Xây dựng module xác thực Login", "Feature", "Critical"),
                ("API quản lý người dùng", "Feature", "High"),
                ("UI Dashboard tổng quan", "Feature", "Medium"),
                ("Fix bug phân quyền sai", "Bug", "High"),
                ("Viết unit test Auth module", "Testing", "Medium"),
                ("Tối ưu query performance", "Improvement", "Medium"),
                ("Review code Pull Request #12", "Research", "Low"),
            };

            var result = new List<TaskItem>();
            // Bộ đếm TaskCode theo từng dự án (FPT-1, FPT-2, ...)
            var taskCounterPerProject = new Dictionary<int, int>();

            foreach (var project in projects)
            {
                var projectDevs = members
                    .Where(m => m.ProjectId == project.Id)
                    .Select(m => users.FirstOrDefault(u => u.Id == m.UserId))
                    .Where(u => u != null)
                    .ToList();

                var owner = users.FirstOrDefault(u => u.Id == project.OwnerId) ?? users.First();
                var numTasks = rng.Next(10, 21);
                taskCounterPerProject[project.Id] = 0;

                for (int i = 0; i < numTasks; i++)
                {
                    var (titleBase, catName, priName) = templates[i % templates.Length];
                    var title = titleBase;  // Giữ tên công việc thuần túy, TaskCode được lưu riêng vào trường TaskCode
                    
                    var priority = priorities.FirstOrDefault(p => p.Name == priName) ?? priorities.First();
                    var status = statuses[rng.Next(statuses.Count)];
                    var category = categories.FirstOrDefault(c => c.Name == catName) ?? categories.First();
                    var assignee = projectDevs.Count > 0 ? projectDevs[rng.Next(projectDevs.Count)] : owner;

                    byte progress = status.Name == "CLOSED" || status.Name == "RESOLVED" ? (byte)100 : (status.Name == "IN-PROGRESS" ? (byte)rng.Next(10, 90) : (byte)0);
                    var isCompleted = progress == 100;

                    // Sinh TaskCode: [ProjectCode]-[Số thứ tự]
                    taskCounterPerProject[project.Id]++;
                    var taskCode = string.IsNullOrWhiteSpace(project.ProjectCode)
                        ? $"TASK-{project.Id}-{taskCounterPerProject[project.Id]}"
                        : $"{project.ProjectCode}-{taskCounterPerProject[project.Id]}";

                    result.Add(new TaskItem
                    {
                        Title = title.Length > 200 ? title.Substring(0, 197) + "..." : title,
                        TaskCode = taskCode,
                        Description = $"Mô tả chi tiết cho công việc {title}. Dữ liệu seed GD10.",
                        ProjectId = project.Id,
                        CreatedById = owner.Id,
                        AssignedToId = assignee?.Id,
                        PriorityId = priority.Id,
                        StatusId = status.Id,
                        CategoryId = category.Id,
                        DueDate = now.AddDays(rng.Next(-5, 20)),
                        ProgressPercent = progress,
                        IsCompleted = isCompleted,
                        EstimatedHours = rng.Next(4, 24),
                        CreatedAt = project.CreatedAt,
                        UpdatedAt = now
                    });
                }
            }
            return result;
        }

        private static List<Expense> BuildExpenses(List<Project> projects, List<User> users, Random rng)
        {
            var result = new List<Expense>();
            var types = new[] { "Nhân công", "Phần mềm", "Hạ tầng", "Khác" };
            foreach (var project in projects)
            {
                int count = rng.Next(1, 4);
                for (int i = 0; i < count; i++)
                {
                    result.Add(new Expense
                    {
                        ProjectId = project.Id,
                        ExpenseType = types[rng.Next(types.Length)],
                        Amount = rng.Next(5, 50) * 1000000m,
                        ExpenseDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-rng.Next(0, 30))),
                        Note = "Chi phí định kỳ giả lập GD10",
                        CreatedById = users[rng.Next(users.Count)].Id,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }
            return result;
        }

        private static async Task SeedSocialData(AppDbContext context, List<TaskItem> tasks, List<User> users, Random rng)
        {
            var comments = new List<Comment>();
            foreach (var task in tasks.Take(20))
            {
                comments.Add(new Comment
                {
                    TaskItemId = task.Id,
                    UserId = users[rng.Next(users.Count)].Id,
                    Content = "Đây là thảo luận mẫu tự động sinh ra.",
                    CreatedAt = DateTime.UtcNow
                });
            }
            context.AddRange(comments);
            await context.SaveChangesAsync();
        }
    }
}
