using MediatR;
using Assignment04_TaskManager.Domain;
using Assignment04_TaskManager.Application.Interfaces;

namespace Assignment04_TaskManager.Application.Commands;

public record ChangeStatusCommand(Guid TaskId, WorkStatus NewStatus) : IRequest<WorkStatus>;

public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, WorkStatus>
{
    private readonly ITaskRepository _taskRepository;

    public ChangeStatusCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public Task<WorkStatus> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        // TODO 1: Find task by TaskId — throw ArgumentException if not found
        // TODO 2: Validate transition is allowed:
        //           Todo → InProgress    ✓
        //           InProgress → Done    ✓
        //           anything else        → throw InvalidOperationException with descriptive message
        // TODO 3: Set task Status to NewStatus, return new Status
        throw new NotImplementedException();
    }
}
