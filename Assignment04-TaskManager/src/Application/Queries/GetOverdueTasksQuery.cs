using MediatR;
using Assignment04_TaskManager.Domain;
using Assignment04_TaskManager.Application.Interfaces;

namespace Assignment04_TaskManager.Application.Queries;

public record GetOverdueTasksQuery() : IRequest<List<TaskSummaryDto>>;

public class GetOverdueTasksQueryHandler : IRequestHandler<GetOverdueTasksQuery, List<TaskSummaryDto>>
{
    private readonly ITaskRepository _taskRepository;

    public GetOverdueTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public Task<List<TaskSummaryDto>> Handle(GetOverdueTasksQuery request, CancellationToken cancellationToken)
    {
        // TODO 1: Get all tasks from repository
        // TODO 2: Filter: Deadline != null AND Deadline < DateTime.UtcNow AND Status != Done
        // TODO 3: Sort: Deadline ascending (most overdue first)
        // TODO 4: Map to TaskSummaryDto, return list
        // ❓ QUESTION: This exercise uses a dedicated ChangeStatusCommand rather than a generic
        //    UpdateTaskCommand(..., newStatus, ...). When does splitting commands like this
        //    make sense in CQRS, and when would a single general-purpose update command
        //    be more appropriate?
        throw new NotImplementedException();
    }
}
