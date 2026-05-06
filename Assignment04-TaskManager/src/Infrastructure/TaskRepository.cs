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

    public WorkTask? GetById(Guid id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public List<WorkTask> GetAll()
    {
        return _tasks.ToList();
    }
}
