using Assignment04_TaskManager.Domain.Exceptions;

namespace Assignment04_TaskManager.Domain;

public class WorkTask
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AssignedTo { get; set; } = string.Empty;
    public WorkStatus Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime CreatedAt { get; set; }

    public void ChangeStatus(WorkStatus newStatus)
    {
        if (!AllowTransition(newStatus))
        {
            throw new BadRequestException($"Cannot transition from {this.Status} to {newStatus}");
        }
        Status = newStatus;
    }

    public Boolean AllowTransition(WorkStatus newStatus)
    {
        if(newStatus == WorkStatus.InProgress)
        {
            return this.Status == WorkStatus.Todo;
        }
        else if(newStatus == WorkStatus.Done)
        {
            return this.Status == WorkStatus.InProgress;
        }
        return false;
    }
}
