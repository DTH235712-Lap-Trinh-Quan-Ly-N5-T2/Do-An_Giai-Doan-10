namespace TaskFlowManagement.Core.Interfaces.Services
{
    public interface ITaskEventBus
    {
        event EventHandler? TaskDataChanged;
        void NotifyDataChanged();
    }
}
