using Assignment04_TaskManager.Domain;

namespace Assignment04_TaskManager.Application.Interfaces;

public interface ITaskRepository
{
    void Add(WorkTask task);
    WorkTask? GetById(Guid id);
    List<WorkTask> GetAll();
}
