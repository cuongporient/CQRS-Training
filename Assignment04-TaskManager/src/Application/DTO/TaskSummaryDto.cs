namespace Assignment04_TaskManager.Application.DTO;

using Assignment04_TaskManager.Domain;

public record TaskSummaryDto(Guid Id, string Title, string Status, string Priority, DateTime? Deadline)
{
}