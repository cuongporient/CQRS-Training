using Assignment04_TaskManager.Application.Queries;
using Assignment04_TaskManager.Application.Interfaces;
using Assignment04_TaskManager.Domain;
using Assignment04_TaskManager.Application.DTO;

namespace Assignment04_TaskManager.Tests.Handlers;

[TestClass]
public class GetOverdueTasksQueryHandlerTests
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly GetOverdueTasksQueryHandler _handler;

    public GetOverdueTasksQueryHandlerTests()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _handler = new GetOverdueTasksQueryHandler(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task Handle_OverdueTasks_ShouldReturnMappedDtos()
    {
        // Arrange
        var tasks = new List<WorkTask>
        {
            new WorkTask { Id = Guid.NewGuid(), Title = "Overdue 1", Deadline = DateTime.UtcNow.AddDays(-1), Status = WorkStatus.Todo },
            new WorkTask { Id = Guid.NewGuid(), Title = "Overdue 2", Deadline = DateTime.UtcNow.AddDays(-2), Status = WorkStatus.InProgress }
        };
        _repositoryMock.Setup(r => r.GetOverdueTasks()).Returns(tasks);
        
        var query = new GetOverdueTasksQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(tasks[0].Title, result[0].Title);
        Assert.AreEqual("Todo", result[0].Status);
        Assert.AreEqual(tasks[1].Title, result[1].Title);
        Assert.AreEqual("InProgress", result[1].Status);
        _repositoryMock.Verify(r => r.GetOverdueTasks(), Times.Once);
    }

    [TestMethod]
    public async Task Handle_NoOverdueTasks_ShouldReturnEmptyList()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetOverdueTasks()).Returns(new List<WorkTask>());
        var query = new GetOverdueTasksQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.AreEqual(0, result.Count);
    }
}
