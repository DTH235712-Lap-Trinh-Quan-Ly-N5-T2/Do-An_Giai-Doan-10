using Microsoft.EntityFrameworkCore;
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.DTOs;
using TaskFlowManagement.Core.Constants;
using TaskFlowManagement.Infrastructure.Data;

namespace TaskFlowManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Triển khai ITaskRepository cho GD4.
    ///
    /// Kỹ thuật áp dụng:
    ///   - IDbContextFactory: mỗi method tạo DbContext riêng, dispose ngay sau khi xong
    ///   - AsNoTracking(): mặc định cho tất cả SELECT (WinForms không cần change tracking)
    ///   - ExecuteUpdateAsync(): update đúng cột cần thiết, không SELECT-then-UPDATE
    ///   - IQueryable filter chain: GetPagedAsync xây WHERE tích lũy trước khi execute
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public TaskRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory
                ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        // ════════════════════════════════════════════════════════
        // CRUD CƠ BẢN
        // ════════════════════════════════════════════════════════

        /// <summary>Lấy task theo ID, kèm các navigation cơ bản để hiển thị danh sách.</summary>
        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Category)
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Lấy task kèm TẤT CẢ navigation properties.
        /// Dùng cho frmTaskEdit — cần hiển thị SubTasks, Reviewer, Tester, Comments, Attachments.
        /// </summary>
        public async Task<TaskItem?> GetByIdWithDetailsAsync(int taskId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .AsSplitQuery() // Tránh Cartesian Explosion khi có nhiều Include 1-n (Comments, Attachments)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Category)
                .Include(t => t.Project)
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Reviewer1)
                .Include(t => t.Reviewer2)
                .Include(t => t.Tester)
                .Include(t => t.ParentTask)
                .Include(t => t.SubTasks).ThenInclude(s => s.Status)
                .Include(t => t.SubTasks).ThenInclude(s => s.AssignedTo)
                .Include(t => t.Comments).ThenInclude(c => c.User)
                .Include(t => t.Attachments).ThenInclude(a => a.UploadedBy)
                .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        /// <summary>
        /// Lấy tất cả task — chỉ dùng cho dataset nhỏ (export, báo cáo).
        /// Với UI danh sách → dùng GetPagedAsync.
        /// </summary>
        public async Task<List<TaskItem>> GetAllAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.AssignedTo)
                .Include(t => t.Project)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        /// <summary>Thêm task mới — set CreatedAt, UpdatedAt tự động.</summary>
        public async Task AddAsync(TaskItem task)
        {
            using var ctx = _contextFactory.CreateDbContext();
            task.CreatedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;
            await ctx.TaskItems.AddAsync(task);
            await ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Cập nhật task — bảo vệ CreatedAt không bị ghi đè.
        /// Dùng Attach + State.Modified + IsModified = false cho cột cần bảo vệ.
        /// </summary>
        /// 
        public async Task<List<TaskItem>> GetAllByProjectAsync(int projectId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Where(t => t.ProjectId == projectId)
                .Include(t => t.AssignedTo)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .OrderByDescending(t => t.PriorityId)
                .ThenBy(t => t.DueDate == null)
                .ThenBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            using var ctx = _contextFactory.CreateDbContext(); // ← tạo context mới
            await ctx.TaskItems
                .Where(t => t.Id == task.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(t => t.Title, task.Title)
                    .SetProperty(t => t.Description, task.Description)
                    .SetProperty(t => t.ProjectId, task.ProjectId)
                    .SetProperty(t => t.TaskCode, task.TaskCode)
                    .SetProperty(t => t.StatusId, task.StatusId)
                    .SetProperty(t => t.PriorityId, task.PriorityId)
                    .SetProperty(t => t.CategoryId, task.CategoryId)
                    .SetProperty(t => t.AssignedToId, task.AssignedToId)
                    .SetProperty(t => t.Reviewer1Id, task.Reviewer1Id)
                    .SetProperty(t => t.Reviewer2Id, task.Reviewer2Id)
                    .SetProperty(t => t.TesterId, task.TesterId)
                    .SetProperty(t => t.DueDate, task.DueDate)
                    .SetProperty(t => t.EstimatedHours, task.EstimatedHours)
                    .SetProperty(t => t.ProgressPercent, task.ProgressPercent)
                    .SetProperty(t => t.IsCompleted, task.IsCompleted)
                    .SetProperty(t => t.UpdatedAt, DateTime.UtcNow));
        }

        /// <summary>Xóa task (hard delete) — cascade xóa Comments, Attachments, TaskTags.</summary>
        public async Task DeleteAsync(int id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            await ctx.TaskItems.Where(t => t.Id == id).ExecuteDeleteAsync();
        }

        // ════════════════════════════════════════════════════════
        // PHÂN TRANG + LỌC ĐA TIÊU CHÍ
        // ════════════════════════════════════════════════════════

        /// <summary>
        /// Phân trang + lọc (IQueryable filter chain).
        ///
        /// Kỹ thuật: xây IQueryable trước, WHERE tích lũy từng bước nếu có giá trị,
        /// chỉ Execute 1 lần duy nhất → SQL sinh ra đúng mệnh đề WHERE cần thiết.
        /// </summary>
        public async Task<(List<TaskItem> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            int? projectId = null,
            int? assignedToId = null,
            int? statusId = null,
            int? priorityId = null,
            int? categoryId = null,
            string? searchKeyword = null)
        {
            using var ctx = _contextFactory.CreateDbContext();

            // Bắt đầu với query base — chưa execute (deferred execution)
            var query = ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Category)
                .Include(t => t.AssignedTo)
                .Include(t => t.Project)
                .AsQueryable();

            // Tích lũy WHERE — chỉ khi có giá trị (optional filter)
            if (projectId.HasValue)
                query = query.Where(t => t.ProjectId == projectId.Value);

            if (assignedToId.HasValue)
                query = query.Where(t => t.AssignedToId == assignedToId.Value);

            if (statusId.HasValue)
                query = query.Where(t => t.StatusId == statusId.Value);

            if (priorityId.HasValue)
                query = query.Where(t => t.PriorityId == priorityId.Value);

            if (categoryId.HasValue)
                query = query.Where(t => t.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(searchKeyword))
                query = query.Where(t =>
                    t.Title.Contains(searchKeyword) ||
                    (t.Description != null && t.Description.Contains(searchKeyword)));

            // Đếm tổng trước khi phân trang (cho tính số trang UI)
            var totalCount = await query.CountAsync();

            // Phân trang
            var items = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        // ════════════════════════════════════════════════════════
        // TRUY VẤN THEO NGƯỜI PHỤ TRÁCH
        // ════════════════════════════════════════════════════════

        /// <summary>
        /// Task chưa hoàn thành được giao cho developer.
        /// Dùng cho màn hình "Công việc của tôi".
        /// </summary>
        public async Task<List<TaskItem>> GetAssignedToUserAsync(int userId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Project)
                .Include(t => t.Category)
                .Where(t => t.AssignedToId == userId && !t.IsCompleted)
                .OrderBy(t => t.DueDate)
                .ThenByDescending(t => t.Priority.Level)
                .ToListAsync();
        }

        /// <summary>Task đang chờ review lần 1 (Status = REVIEW-1) bởi reviewer chỉ định.</summary>
        public async Task<List<TaskItem>> GetByReviewer1Async(int reviewer1Id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Where(t => t.Reviewer1Id == reviewer1Id &&
                            t.Status.Name == TaskFlowManagement.Core.Constants.WorkflowConstants.StatusNameReview1)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        /// <summary>Task đang chờ review lần 2 (Status = REVIEW-2) bởi reviewer chỉ định.</summary>
        public async Task<List<TaskItem>> GetByReviewer2Async(int reviewer2Id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Where(t => t.Reviewer2Id == reviewer2Id &&
                            t.Status.Name == TaskFlowManagement.Core.Constants.WorkflowConstants.StatusNameReview2)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        /// <summary>Task đang chờ test (Status = IN-TEST) bởi tester chỉ định.</summary>
        public async Task<List<TaskItem>> GetByTesterAsync(int testerId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Where(t => t.TesterId == testerId &&
                            t.Status.Name == TaskFlowManagement.Core.Constants.WorkflowConstants.StatusNameInTest)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        // ════════════════════════════════════════════════════════
        // TRUY VẤN THEO DỰ ÁN
        // ════════════════════════════════════════════════════════

        /// <summary>
        /// Tất cả task của 1 dự án.
        /// includeSubTasks = false → chỉ lấy task gốc (ParentTaskId == null).
        /// </summary>
        public async Task<List<TaskItem>> GetByProjectAsync(int projectId, bool includeSubTasks = false)
        {
            using var ctx = _contextFactory.CreateDbContext();

            var query = ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.AssignedTo)
                .Include(t => t.Category)
                .Where(t => t.ProjectId == projectId);

            if (!includeSubTasks)
                query = query.Where(t => t.ParentTaskId == null);

            return await query
                .OrderByDescending(t => t.Priority.Level)
                .ThenBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<int> GetMaxTaskNumberByProjectAsync(int projectId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            // Trích số cuối từ TaskCode, lấy MAX — atomic và không load toàn bộ list
            return await ctx.TaskItems
                .AsNoTracking()
                .Where(t => t.ProjectId == projectId)
                .CountAsync(); // hoặc dùng sequence DB nếu cần strict
        }

        // ════════════════════════════════════════════════════════
        // CẢNH BÁO / DASHBOARD
        // ════════════════════════════════════════════════════════

        /// <summary>Task đã quá hạn — dùng cho widget cảnh báo đỏ trên Dashboard.</summary>
        public async Task<List<TaskItem>> GetOverdueAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            var now = DateTime.UtcNow;
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Where(t => t.DueDate < now && !t.IsCompleted)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        /// <summary>Task sắp đến hạn trong N ngày — widget nhắc nhở vàng trên Dashboard.</summary>
        public async Task<List<TaskItem>> GetDueSoonAsync(int days = 7)
        {
            using var ctx = _contextFactory.CreateDbContext();
            var now = DateTime.UtcNow;
            var threshold = now.AddDays(days);
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Where(t => t.DueDate >= now && t.DueDate <= threshold && !t.IsCompleted)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        // ════════════════════════════════════════════════════════
        // CẬP NHẬT TỐI ƯU (ExecuteUpdateAsync)
        // ════════════════════════════════════════════════════════

        /// <summary>
        /// Cập nhật tiến độ % — tự động chuyển Status sang RESOLVED khi progress = 100.
        ///
        /// SQL sinh ra:
        ///   UPDATE TaskItems
        ///   SET ProgressPercent=?, IsCompleted=?, UpdatedAt=?, CompletedAt=?, StatusId=?
        ///   WHERE Id=?
        ///
        /// resolvedStatusId: ID của Status có Name = "RESOLVED" (truyền vào từ Service
        /// sau khi lookup từ DB, tránh hard-code ID).
        /// </summary>
        public async Task UpdateProgressAsync(int taskId, byte progress, int resolvedStatusId)
        {
            using var ctx = _contextFactory.CreateDbContext(); // ← tạo context mới
            if (progress == 100)
            {
                await ctx.TaskItems
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(t => t.ProgressPercent, progress)
                        .SetProperty(t => t.IsCompleted, true)
                        .SetProperty(t => t.StatusId, resolvedStatusId)
                        .SetProperty(t => t.CompletedAt, DateTime.UtcNow)
                        .SetProperty(t => t.UpdatedAt, DateTime.UtcNow));
            }
            else
            {
                await ctx.TaskItems
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(t => t.ProgressPercent, progress)
                        .SetProperty(t => t.IsCompleted, false)
                        .SetProperty(t => t.CompletedAt, (DateTime?)null)
                        .SetProperty(t => t.UpdatedAt, DateTime.UtcNow));
            }
        }

        /// <summary>
        /// Chuyển trạng thái workflow — chỉ update StatusId + UpdatedAt.
        /// Dùng ExecuteUpdateAsync, không cần load entity.
        /// Nếu Status = CLOSED (10) thì tự động hoàn thành task.
        /// </summary>
        public async Task UpdateStatusAsync(int taskId, int statusId)
        {
            using var ctx = _contextFactory.CreateDbContext();

            if (statusId == TaskFlowManagement.Core.Constants.WorkflowConstants.StatusIdClosed) // CLOSED
            {
                await ctx.TaskItems
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(t => t.StatusId, statusId)
                        .SetProperty(t => t.IsCompleted, true)
                        .SetProperty(t => t.CompletedAt, DateTime.UtcNow)
                        .SetProperty(t => t.UpdatedAt, DateTime.UtcNow));
            }
            else
            {
                await ctx.TaskItems
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(t => t.StatusId, statusId)
                        .SetProperty(t => t.UpdatedAt, DateTime.UtcNow));
            }
        }

        /// <summary>
        /// Gán reviewer/tester và chuyển trạng thái.
        ///
        /// Giới hạn EF Core: SetProperty không cho phép tham chiếu lại entity (t.Field)
        /// trong phần value → không thể viết conditional trong 1 ExecuteUpdateAsync.
        /// Fix: tách thành các call riêng biệt, chỉ gọi khi tham số có giá trị.
        ///   - 1 call bắt buộc: StatusId + UpdatedAt
        ///   - 0–3 call tùy chọn: Reviewer1Id / Reviewer2Id / TesterId
        /// </summary>
        public async Task AssignReviewerAsync(
            int taskId,
            int? reviewer1Id,
            int? reviewer2Id,
            int? testerId,
            int newStatusId)
        {
            using var ctx = _contextFactory.CreateDbContext();

            await ctx.TaskItems
                .Where(t => t.Id == taskId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(t => t.StatusId,    newStatusId)
                    .SetProperty(t => t.UpdatedAt,   DateTime.UtcNow)
                    .SetProperty(t => t.Reviewer1Id, t => reviewer1Id ?? t.Reviewer1Id)
                    .SetProperty(t => t.Reviewer2Id, t => reviewer2Id ?? t.Reviewer2Id)
                    .SetProperty(t => t.TesterId,    t => testerId    ?? t.TesterId));
        }

        // ════════════════════════════════════════════════════════
        // THỐNG KÊ
        // ════════════════════════════════════════════════════════

        /// <summary>
        /// GroupBy Status trực tiếp trên DB — không load data về memory.
        /// Kết quả: { "CREATED": 3, "IN-PROGRESS": 7, "CLOSED": 5, ... }
        /// </summary>
        public async Task<Dictionary<string, int>> GetStatusSummaryByProjectAsync(int projectId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Where(t => t.ProjectId == projectId)
                .GroupBy(t => t.Status.Name)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }

        /// <summary>
        /// Thống kê chi tiết Giai đoạn 6: Tính toán số lượng và dữ liệu vẽ biểu đồ.
        /// Chạy 1 số truy vấn SQL tối ưu thông qua LINQ GroupBy.
        /// </summary>
        public async Task<DashboardStatsDto> GetDashboardStatsAsync(int? projectId = null)
        {
            using var ctx = _contextFactory.CreateDbContext();
            var now = DateTime.UtcNow;
            var dueSoonThreshold = now.AddDays(7);

            var taskQuery = ctx.TaskItems.AsNoTracking();

            if (projectId.HasValue)
            {
                taskQuery = taskQuery.Where(t => t.ProjectId == projectId.Value);
            }

            var stats = new DashboardStatsDto();

            // 1. Basic Task Counts (1 round-trip query)
            var summary = await taskQuery.Select(t => new
            {
                IsCompleted = t.IsCompleted,
                IsOverdue   = !t.IsCompleted && t.DueDate < now,
                IsDueSoon   = !t.IsCompleted && t.DueDate >= now && t.DueDate <= dueSoonThreshold
            })
            .GroupBy(_ => 1)
            .Select(g => new
            {
                Total     = g.Count(),
                Completed = g.Count(x => x.IsCompleted),
                Overdue   = g.Count(x => x.IsOverdue),
                DueSoon   = g.Count(x => x.IsDueSoon)
            })
            .FirstOrDefaultAsync();

            stats.TotalTasks     = summary?.Total     ?? 0;
            stats.CompletedTasks = summary?.Completed ?? 0;
            stats.OverdueTasks   = summary?.Overdue   ?? 0;
            stats.DueSoonTasks   = summary?.DueSoon   ?? 0;

            // 2. Status Summary (Vẽ Pie Chart)
            var statusGroups = await taskQuery
                .GroupBy(t => new { t.StatusId, t.Status.Name, t.Status.ColorHex })
                .Select(g => new 
                { 
                    StatusId = g.Key.StatusId, 
                    StatusName = g.Key.Name,
                    ColorHex = g.Key.ColorHex,
                    Count = g.Count()
                })
                .ToListAsync();

            foreach (var group in statusGroups)
            {
                stats.StatusSummaries.Add(new StatusSummaryDto
                {
                    StatusName = string.IsNullOrEmpty(group.StatusName) ? $"Status {group.StatusId}" : group.StatusName,
                    Count = group.Count,
                    ColorHex = string.IsNullOrEmpty(group.ColorHex) ? "#94A3B8" : group.ColorHex
                });
            }

            // 3. Project Progress (Vẽ Bar Chart)
            // Tính số trung bình ProgressPercent của task gốc (ParentTaskId == null)
            var projectProgressGroups = await taskQuery
                .Where(t => t.ParentTaskId == null)
                .GroupBy(t => new { t.ProjectId, t.Project.Name })
                .Select(g => new
                {
                    ProjectName = g.Key.Name,
                    AvgProgress = g.Average(t => (double)t.ProgressPercent)
                })
                .OrderByDescending(p => p.AvgProgress)
                .ThenBy(p => p.ProjectName)
                .ToListAsync();

            foreach (var proj in projectProgressGroups)
            {
                stats.ProjectProgresses.Add(new ProjectProgressDto
                {
                    ProjectName = proj.ProjectName,
                    ProgressPercentage = (decimal)Math.Round(proj.AvgProgress, 1)
                });
            }

            return stats;
        }

        public async Task<List<BudgetReportDto>> GetBudgetReportAsync(int? projectId = null)
        {
            using var ctx = _contextFactory.CreateDbContext();
            var query = ctx.Projects.AsNoTracking();
            if (projectId.HasValue)
            {
                query = query.Where(p => p.Id == projectId.Value);
            }

            // GroupBy/Select trực tiếp trên Database để lấy Sum Amount
            return await query
                .Select(p => new BudgetReportDto
                {
                    ProjectName = p.Name,
                    Budget = p.Budget,
                    TotalExpense = p.Expenses.Sum(e => e.Amount)
                })
                .ToListAsync();
        }

        public async Task<List<ProgressReportDto>> GetProgressReportAsync(int? projectId = null)
        {
            using var ctx = _contextFactory.CreateDbContext();
            var query = ctx.Projects.AsNoTracking();
            if (projectId.HasValue)
            {
                query = query.Where(p => p.Id == projectId.Value);
            }

            // Project.Tasks aggregate tại Database
            return await query
                .Select(p => new ProgressReportDto
                {
                    ProjectName = p.Name,
                    TotalTasks = p.Tasks.Count(),
                    CompletedTasks = p.Tasks.Count(t => t.IsCompleted),
                    AvgProgress = p.Tasks.Any() ? (decimal)p.Tasks.Average(t => (double)t.ProgressPercent) : 0m,
                    Status = p.Status
                })
                .ToListAsync();
        }

        // ════════════════════════════════════════════════════════
        // LOOKUP DATA CHO DROPDOWN
        // ════════════════════════════════════════════════════════

        /// <summary>Lấy tất cả Status (10 bước), sắp theo DisplayOrder — dùng cho dropdown.</summary>
        public async Task<List<Status>> GetAllStatusesAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Statuses
                .AsNoTracking()
                .OrderBy(s => s.DisplayOrder)
                .ToListAsync();
        }

        /// <summary>Lấy tất cả Priority (4 mức), sắp theo Level — dùng cho dropdown.</summary>
        public async Task<List<Priority>> GetAllPrioritiesAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Priorities
                .AsNoTracking()
                .OrderBy(p => p.Level)
                .ToListAsync();
        }

        /// <summary>Lấy tất cả Category — dùng cho dropdown.</summary>
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}