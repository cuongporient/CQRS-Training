using Assignment04_TaskManager.Application.Commands;
using Assignment04_TaskManager.Application.Interfaces;
using Assignment04_TaskManager.Domain;
using Assignment04_TaskManager.Domain.Exceptions;

namespace Assignment04_TaskManager.Tests.Handlers;

[TestClass]
public class ChangeStatusCommandHandlerTests
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly ChangeStatusCommandHandler _handler;

    public ChangeStatusCommandHandlerTests()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _handler = new ChangeStatusCommandHandler(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task Handle_ValidTransition_ShouldUpdateStatusAndReturnIt()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var task = new WorkTask { Id = taskId, Status = WorkStatus.Todo };
        _repositoryMock.Setup(r => r.GetById(taskId)).Returns(task);
        
        var command = new ChangeStatusCommand(taskId, WorkStatus.InProgress);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreEqual(WorkStatus.InProgress, result);
        Assert.AreEqual(WorkStatus.InProgress, task.Status);
        _repositoryMock.Verify(r => r.Update(task), Times.Once);
    }

    [TestMethod]
    public async Task Handle_TaskNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetById(taskId)).Returns((WorkTask?)null);
        var command = new ChangeStatusCommand(taskId, WorkStatus.InProgress);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_InvalidTransition_ShouldThrowBadRequestException()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var task = new WorkTask { Id = taskId, Status = WorkStatus.Todo }; // Todo -> Done is invalid
        _repositoryMock.Setup(r => r.GetById(taskId)).Returns(task);
        var command = new ChangeStatusCommand(taskId, WorkStatus.Done);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<BadRequestException>(async () => await _handler.Handle(command, CancellationToken.None));
    }
}
