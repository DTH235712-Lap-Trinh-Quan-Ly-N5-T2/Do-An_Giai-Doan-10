using TaskFlowManagement.Core.Interfaces.Services;

namespace TaskFlowManagement.Core.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        public event EventHandler<NotificationEventArgs>? NotificationReceived;   // ✅ đổi tên

        public void Push(int targetUserId, string title, string message, NotificationLevel level = NotificationLevel.Info)
        {
            _ = System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    NotificationReceived?.Invoke(this, new NotificationEventArgs   // ✅ đổi tên
                    {
                        TargetUserId = targetUserId,
                        Title        = title,
                        Message      = message,
                        Level        = level
                    });
                }
                catch { /* nuốt exception */ }
            });
        }
    }
}