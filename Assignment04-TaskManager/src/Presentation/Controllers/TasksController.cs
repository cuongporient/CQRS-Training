using MediatR;
using Microsoft.AspNetCore.Mvc;
using Assignment04_TaskManager.Presentation.Models;

namespace Assignment04_TaskManager.Presentation.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST /tasks
    // Body: { "title": "Write unit tests", "assignedTo": "alice", "priority": "High", "deadline": "2026-05-01" }
    // Returns: 201 Created with the new task ID
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        // TODO: Parse request.Priority to the Priority enum
        // TODO: Create a CreateTaskCommand from the request, send it via _mediator
        // Return CreatedAtAction with the new task ID
        throw new NotImplementedException();
    }

    // PATCH /tasks/{id}/status
    // Body: { "newStatus": "InProgress" }
    // Returns: 200 OK with the updated status, or 422 for invalid state transitions
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeStatusRequest request)
    {
        // TODO: Parse request.NewStatus to the WorkStatus enum
        // TODO: Create a ChangeStatusCommand, send it via _mediator
        // Catch InvalidOperationException (bad transition) and return UnprocessableEntity with message
        // Return Ok(new { status = newStatus }) on success
        throw new NotImplementedException();
    }

    // GET /tasks?assignee=alice
    // Returns: 200 OK with tasks for that assignee, sorted by priority descending
    [HttpGet]
    public async Task<IActionResult> GetByAssignee([FromQuery] string assignee)
    {
        // TODO: Create a GetTasksByAssigneeQuery with the assignee, send it via _mediator
        // Return Ok(tasks)
        throw new NotImplementedException();
    }

    // GET /tasks/overdue
    // Returns: 200 OK with overdue tasks sorted by deadline ascending
    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdue()
    {
        // TODO: Create a GetOverdueTasksQuery, send it via _mediator
        // Return Ok(tasks)
        throw new NotImplementedException();
    }
}
