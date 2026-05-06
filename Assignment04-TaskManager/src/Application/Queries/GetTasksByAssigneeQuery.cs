using MediatR;
using Assignment04_TaskManager.Domain;
using Assignment04_TaskManager.Application.Interfaces;

namespace Assignment04_TaskManager.Application.Queries;

public record TaskSummaryDto(Guid Id, string Title, WorkStatus Status, Priority Priority, DateTime? Deadline);

public record GetTasksByAssigneeQuery(string Assignee) : IRequest<List<TaskSummaryDto>>;

public class GetTasksByAssigneeQueryHandler : IRequestHandler<GetTasksByAssigneeQuery, List<TaskSummaryDto>>
{
    private readonly ITaskRepository _taskRepository;

    public GetTasksByAssigneeQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public Task<List<TaskSummaryDto>> Handle(GetTasksByAssigneeQuery request, CancellationToken cancellationToken)
    {
        // TODO 1: Get all tasks from repository
        // TODO 2: Filter: AssignedTo equals Assignee (case-insensitive)
        // TODO 3: Sort: Priority descending (High first)
        // TODO 4: Map to TaskSummaryDto, return list
        throw new NotImplementedException();
    }
}
