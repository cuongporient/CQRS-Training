using Assignment04_TaskManager.Application.Queries;
using Assignment04_TaskManager.Application.Interfaces;
using Assignment04_TaskManager.Domain;
using Assignment04_TaskManager.Application.DTO;

namespace Assignment04_TaskManager.Tests.Handlers;

[TestClass]
public class GetTasksByAssigneeQueryHandlerTests
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly GetTasksByAssigneeQueryHandler _handler;

    public GetTasksByAssigneeQueryHandlerTests()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _handler = new GetTasksByAssigneeQueryHandler(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task Handle_ExistingTasks_ShouldReturnMappedDtos()
    {
        // Arrange
        var assignee = "Alice";
        var tasks = new List<WorkTask>
        {
            new WorkTask { Id = Guid.NewGuid(), Title = "Task 1", AssignedTo = assignee, Priority = Priority.High, Status = WorkStatus.Todo },
            new WorkTask { Id = Guid.NewGuid(), Title = "Task 2", AssignedTo = assignee, Priority = Priority.Medium, Status = WorkStatus.InProgress }
        };
        _repositoryMock.Setup(r => r.GetWorkTasksByAssignee(assignee)).Returns(tasks);
        
        var query = new GetTasksByAssigneeQuery(assignee);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(tasks[0].Title, result[0].Title);
        Assert.AreEqual("Todo", result[0].Status);
        Assert.AreEqual("High", result[0].Priority);
        Assert.AreEqual(tasks[1].Title, result[1].Title);
        Assert.AreEqual("InProgress", result[1].Status);
        Assert.AreEqual("Medium", result[1].Priority);
        _repositoryMock.Verify(r => r.GetWorkTasksByAssignee(assignee), Times.Once);
    }

    [TestMethod]
    public async Task Handle_NoTasks_ShouldReturnEmptyList()
    {
        // Arrange
        var assignee = "Bob";
        _repositoryMock.Setup(r => r.GetWorkTasksByAssignee(assignee)).Returns(new List<WorkTask>());
        var query = new GetTasksByAssigneeQuery(assignee);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.AreEqual(0, result.Count);
    }
}
