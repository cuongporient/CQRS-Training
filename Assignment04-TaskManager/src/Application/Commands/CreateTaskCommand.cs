using MediatR;
using Assignment04_TaskManager.Domain;
using Assignment04_TaskManager.Application.Interfaces;

namespace Assignment04_TaskManager.Application.Commands;

public record CreateTaskCommand(string Title, string AssignedTo, Priority Priority, DateTime? Deadline) : IRequest<Guid>;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly ITaskRepository _taskRepository;

    public CreateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // TODO 1: Validate Title is not empty — throw ArgumentException
        // TODO 2: Validate AssignedTo is not empty — throw ArgumentException
        // TODO 3: Create WorkTask, add to repository, return Id
        throw new NotImplementedException();
    }
}
