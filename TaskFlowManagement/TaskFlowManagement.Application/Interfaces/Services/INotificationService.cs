namespace TaskFlowManagement.Core.Interfaces.Services
{
    public interface INotificationService
    {
        void Push(int targetUserId, string title, string message, NotificationLevel level = NotificationLevel.Info);
        event EventHandler<NotificationEventArgs>? NotificationReceived;
    }

    public enum NotificationLevel { Info, Success, Warning, Error }

    public class NotificationEventArgs : EventArgs
    {
        public int TargetUserId { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public NotificationLevel Level { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.Now;
    }
}