using MediatR;
using FluentValidation;
using Assignment04_TaskManager.Application.Interfaces;
using Assignment04_TaskManager.Domain;

namespace Assignment04_TaskManager.Application.Commands;

public record CreateTaskCommand(string Title, string AssignedTo, Priority Priority, DateTime? Deadline) : IRequest<Guid>;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title must not be empty");

        RuleFor(v => v.AssignedTo)
            .NotEmpty().WithMessage("AssignedTo must not be empty");
    }
}

public class CreateTaskCommandHandler(ITaskRepository _taskRepository) : IRequestHandler<CreateTaskCommand, Guid>
{
    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var newTask = new WorkTask
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            AssignedTo = request.AssignedTo,
            Status = WorkStatus.Todo,
            Priority = request.Priority,
            Deadline = request.Deadline,
            CreatedAt = DateTime.UtcNow
        };
        _taskRepository.Add(newTask);
        return newTask.Id;
    }
}
