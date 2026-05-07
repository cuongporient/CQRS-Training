using MediatR;
using FluentValidation;
using Assignment04_TaskManager.Application.Interfaces;
using Assignment04_TaskManager.Domain;
using Assignment04_TaskManager.Domain.Exceptions;

namespace Assignment04_TaskManager.Application.Commands;

public record ChangeStatusCommand(Guid TaskId, WorkStatus NewStatus) : IRequest<WorkStatus>;

public class ChangeStatusCommandValidator : AbstractValidator<ChangeStatusCommand>
{
    public ChangeStatusCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("TaskId must not be empty");

        RuleFor(x => x.NewStatus)
            .NotEmpty().WithMessage("NewStatus must not be empty")
            .IsInEnum().WithMessage("NewStatus must be a valid status");
    }
}

public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, WorkStatus>
{
    private readonly ITaskRepository _taskRepository;

    public ChangeStatusCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<WorkStatus> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var task = _taskRepository.GetById(request.TaskId);

            if (task == null)
            {
                throw new NotFoundException(nameof(WorkTask), request.TaskId);
            }
            task.ChangeStatus(request.NewStatus);

            _taskRepository.Update(task);

            return task.Status;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
