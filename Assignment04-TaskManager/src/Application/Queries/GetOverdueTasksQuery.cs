using MediatR;
using Assignment04_TaskManager.Application.Interfaces;
using Assignment04_TaskManager.Domain;
using Assignment04_TaskManager.Application.DTO;

namespace Assignment04_TaskManager.Application.Queries;

public record GetOverdueTasksQuery() : IRequest<List<TaskSummaryDto>>;

public class GetOverdueTasksQueryHandler(ITaskRepository _taskRepository) : IRequestHandler<GetOverdueTasksQuery, List<TaskSummaryDto>>
{
    public async Task<List<TaskSummaryDto>> Handle(GetOverdueTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = _taskRepository.GetOverdueTasks();

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
