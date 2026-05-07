using MediatR;
using Assignment04_TaskManager.Application.Interfaces;
using Assignment04_TaskManager.Domain;
using Assignment04_TaskManager.Application.DTO;

namespace Assignment04_TaskManager.Application.Queries;

public record GetTasksByAssigneeQuery(string Assignee) : IRequest<List<TaskSummaryDto>>;

public class GetTasksByAssigneeQueryHandler(ITaskRepository _taskRepository) : IRequestHandler<GetTasksByAssigneeQuery, List<TaskSummaryDto>>
{
    public async Task<List<TaskSummaryDto>> Handle(GetTasksByAssigneeQuery request, CancellationToken cancellationToken)
    {
        var tasks = _taskRepository.GetWorkTasksByAssignee(request.Assignee);

        var result = tasks.Select(t => new TaskSummaryDto(
            t.Id,
            t.Title,
            t.Status.ToString(),
            t.Priority.ToString(),
            t.Deadline
        )).ToList();

        return result;
    }
}
