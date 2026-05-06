namespace Assignment04_TaskManager.Presentation.Models;

public record CreateTaskRequest(string Title, string AssignedTo, string Priority, DateTime? Deadline);

public record ChangeStatusRequest(string NewStatus);
