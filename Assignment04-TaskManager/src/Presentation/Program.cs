using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Assignment04_TaskManager.Application;
using Assignment04_TaskManager.Infrastructure;
using Assignment04_TaskManager.Application.Commands;
using Assignment04_TaskManager.Application.Queries;

// Setup DI
var services = new ServiceCollection();
services.AddApplication();
services.AddInfrastructure();

var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetRequiredService<IMediator>();

try
{
    var now = DateTime.UtcNow;

    // Test setup: Create 3 tasks
    var t1Id = await mediator.Send(new CreateTaskCommand("Write unit tests", "alice", Priority.High, now.AddDays(-2)));
    var t2Id = await mediator.Send(new CreateTaskCommand("Review PR", "bob", Priority.Medium, now.AddDays(3)));
    var t3Id = await mediator.Send(new CreateTaskCommand("Deploy to staging", "alice", Priority.High, now.AddDays(1)));
    Console.WriteLine($"[OK] Created 3 tasks");

    // Test 1: ChangeStatusCommand — valid transition (Todo → InProgress)
    var newStatus = await mediator.Send(new ChangeStatusCommand(t1Id, WorkStatus.InProgress));
    if (newStatus == WorkStatus.InProgress)
    {
        Console.WriteLine($"[OK] ChangeStatusCommand: Todo → InProgress succeeded");
    }

    // Test 2: ChangeStatusCommand — invalid transition (Todo → Done, should fail)
    try
    {
        await mediator.Send(new ChangeStatusCommand(t2Id, WorkStatus.Done));
        Console.WriteLine("[FAIL] Expected InvalidOperationException for invalid transition");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"[OK] ChangeStatusCommand correctly rejected invalid transition");
    }

    // Test 3: GetTasksByAssigneeQuery
    var aliceTasks = await mediator.Send(new GetTasksByAssigneeQuery("alice"));
    if (aliceTasks.Count == 2)
    {
        Console.WriteLine($"[OK] GetTasksByAssigneeQuery('alice') returned {aliceTasks.Count} tasks");
        foreach (var task in aliceTasks)
        {
            Console.WriteLine($"  - {task.Title} (Priority: {task.Priority})");
        }
    }

    // Test 4: GetOverdueTasksQuery
    var overdue = await mediator.Send(new GetOverdueTasksQuery());
    if (overdue.Count == 1 && overdue[0].Title == "Write unit tests")
    {
        Console.WriteLine($"[OK] GetOverdueTasksQuery returned {overdue.Count} overdue task(s)");
        foreach (var task in overdue)
        {
            Console.WriteLine($"  - {task.Title}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR] Unexpected exception: {ex.Message}");
}
