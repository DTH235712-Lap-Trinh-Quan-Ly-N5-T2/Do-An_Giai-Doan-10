using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.Core.Services.Auth;
using TaskFlowManagement.Core.Services.Users;
using TaskFlowManagement.Core.Services.Projects;
using TaskFlowManagement.Core.Services.Expenses;
using TaskFlowManagement.Core.Services.Tasks;
using TaskFlowManagement.Infrastructure.Data;
using TaskFlowManagement.Infrastructure.Repositories;
using TaskFlowManagement.WinForms.Forms;

namespace TaskFlowManagement.WinForms
{
    /// <summary>
    /// Entry point – cấu hình DI Container và khởi động ứng dụng.
    /// </summary>
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        [STAThread]
        static void Main()
        {
            // --- Cấu hình Bắt lỗi Toàn cục (Global Exception Handling) ---
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            
            // 1. Lỗi xuất phát từ UI Thread (Nhấn nút, cuộn chuột, load form...)
            Application.ThreadException += (sender, args) =>
            {
                MessageBox.Show(
                    $"[UI Error] Đã xảy ra sự cố đột xuất:\n\n{args.Exception.Message}\n\nHệ thống vẫn đang an toàn. Vui lòng thử lại thao tác.", 
                    "Cảnh báo Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };

            // 2. Lỗi xuất phát từ Background Thread (Task.Run, BackgroundWorker...)
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                var ex = args.ExceptionObject as Exception;
                MessageBox.Show(
                    $"[Background Error] Lỗi tiến trình ngầm:\n\n{ex?.Message}", 
                    "Lỗi Hệ Thống", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            ApplicationConfiguration.Initialize();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            var services = new ServiceCollection();

            // 1. Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            services.AddSingleton<IConfiguration>(config);

            // 2. Database / Việc dùng Factory rất quan trọng trong WinForms để tránh xung đột DbContext khi mở nhiều Form cùng lúc. 
                           // Nó cũng cấu hình RetryOnFailure (tự kết nối lại nếu mạng lỗi) và NoTracking (tăng tốc độ đọc dữ liệu).
            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    sql =>
                    {
                        sql.EnableRetryOnFailure(maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null);
                        sql.CommandTimeout(30);
                    })
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );

            // 3. Repositories / Đăng ký các lớp xử lý nghiệp vụ (Business Logic) và truy vấn dữ liệu (Data Access).
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();

            // 4. Services / Đăng ký các lớp xử lý nghiệp vụ (Business Logic) và truy vấn dữ liệu (Data Access).
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IExpenseService, ExpenseService>();

            // 5. Forms / Các Form cũng được đưa vào DI. Điều này cho phép các Form nhận các Service thông qua hàm Constructor (Constructor Injection).
            services.AddTransient<frmLogin>();
            services.AddTransient<frmMain>();
            services.AddTransient<frmHome>();
            // GD2: Quản lý Người dùng & Khách hàng
            services.AddTransient<frmUsers>();
            services.AddTransient<frmCustomers>();
            services.AddTransient<frmChangePassword>();
            // GD3: Quản lý Dự án
            services.AddTransient<frmProjects>();
            // GD4: Quản lý Công việc
            // DI tự inject (ITaskService, IProjectService, IUserService) vào constructor
            services.AddTransient<frmTaskList>();
            services.AddTransient<frmMyTasks>();
            // GD6: Dashboard Thống Kê
            services.AddTransient<frmDashboard>();
            // GD8: Quản lý Chi phí
            services.AddTransient<frmExpenses>();
            // GD9: Báo cáo Chi phí & Ngân sách (RDLC)
            // frmReportViewer KHÔNG đăng ký Transient vì nhận tham số runtime (projectId)
            // → Được khởi tạo trực tiếp bằng new frmReportViewer(_expenseService, projectId)
            //   trong frmExpenses.OpenReportAsync() – đây là exception pattern hợp lệ.

            ServiceProvider = services.BuildServiceProvider();

            // Migrate + Seed
            try
            {
                using var scope = ServiceProvider.CreateScope();
                var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
                using var context = factory.CreateDbContext();
                context.Database.Migrate();
                DbSeeder.SeedAsync(context).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Không thể kết nối database:\n\n{ex.Message}\n\n" +
                    "Kiểm tra:\n" +
                    "  1. SQL Server Express đang chạy\n" +
                    "  2. Tên instance trong appsettings.json đúng không\n" +
                    "  3. Windows Firewall không chặn SQL Server",
                    "Lỗi kết nối Database",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Khởi động: Login → Main
            // #6: Bọc using để Dispose frmLogin sau khi đăng nhập xong
            using var loginForm = ServiceProvider.GetRequiredService<frmLogin>();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                var mainForm = ServiceProvider.GetRequiredService<frmMain>();
                Application.Run(mainForm);
            }
        }
    }
}