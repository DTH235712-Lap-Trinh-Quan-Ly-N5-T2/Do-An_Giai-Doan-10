using TaskFlowManagement.Core.Interfaces.Services;

namespace TaskFlowManagement.Core.Services.Tasks
{
    public class TaskEventBus : ITaskEventBus
    {
        public event EventHandler? TaskDataChanged;

        public void NotifyDataChanged()
        {
            TaskDataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
