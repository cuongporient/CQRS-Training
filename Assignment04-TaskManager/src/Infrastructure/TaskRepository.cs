using Assignment04_TaskManager.Domain;
using Assignment04_TaskManager.Application.Interfaces;

namespace Assignment04_TaskManager.Infrastructure;

public class TaskRepository : ITaskRepository
{
    private static readonly List<WorkTask> _tasks = new();

    public void Add(WorkTask task)
    {
        _tasks.Add(task);
    }

    public void Update(WorkTask task)
    {
        var existingTask = GetById(task.Id);
        if (existingTask != null)
        {
            var index = _tasks.IndexOf(existingTask);
            _tasks[index] = task;
        }
    }

    public WorkTask? GetById(Guid id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public List<WorkTask> GetAll()
    {
        return _tasks.ToList();
    }

    public List<WorkTask> GetWorkTasksByAssignee(string assignee)
    {
        return _tasks
            .Where(t => string.Equals(t.AssignedTo, assignee, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(t => t.Priority)
            .ToList();
    }

    public List<WorkTask> GetOverdueTasks()
    {
        return _tasks
            .Where(t => t.Deadline != null && t.Deadline < DateTime.UtcNow && t.Status != WorkStatus.Done)
            .OrderBy(t => t.Deadline)
            .ToList();
    }
}
