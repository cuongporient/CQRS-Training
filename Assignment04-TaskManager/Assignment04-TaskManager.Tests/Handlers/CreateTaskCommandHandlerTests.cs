using Assignment04_TaskManager.Application.Commands;
using Assignment04_TaskManager.Application.Interfaces;
using Assignment04_TaskManager.Domain;

namespace Assignment04_TaskManager.Tests.Handlers;

[TestClass]
public class CreateTaskCommandHandlerTests
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly CreateTaskCommandHandler _handler;

    public CreateTaskCommandHandlerTests()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _handler = new CreateTaskCommandHandler(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task Handle_ValidRequest_ShouldAddTaskAndReturnId()
    {
        // Arrange
        var command = new CreateTaskCommand("Test Title", "Alice", Priority.High, DateTime.UtcNow.AddDays(1));
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreNotEqual(Guid.Empty, result);
        _repositoryMock.Verify(r => r.Add(It.Is<WorkTask>(t => 
            t.Title == command.Title && 
            t.AssignedTo == command.AssignedTo && 
            t.Priority == command.Priority &&
            t.Status == WorkStatus.Todo)), Times.Once);
    }

    [TestMethod]
    public async Task Handle_RepositoryThrows_ShouldPropagateException()
    {
        // Arrange
        var command = new CreateTaskCommand("Test Title", "Alice", Priority.High, null);
        _repositoryMock.Setup(r => r.Add(It.IsAny<WorkTask>())).Throws(new Exception("DB Error"));

        // Act & Assert
        await Assert.ThrowsExceptionAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
    }
}
