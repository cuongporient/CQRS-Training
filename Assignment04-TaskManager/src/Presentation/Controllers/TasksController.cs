using MediatR;
using Microsoft.AspNetCore.Mvc;
using Assignment04_TaskManager.Presentation.Models;
using Assignment04_TaskManager.Application.Commands;
using Assignment04_TaskManager.Application.Queries;
using Assignment04_TaskManager.Domain;

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
        if (!Enum.TryParse<Priority>(request.Priority, true, out var priority))
        {
            return BadRequest("Invalid priority value.");
        }

        var command = new CreateTaskCommand(request.Title, request.AssignedTo, priority, request.Deadline);
        var id = await _mediator.Send(command);

        return CreatedAtAction(null, new { id }, id);
    }

    // PATCH /tasks/{id}/status
    // Body: { "newStatus": "InProgress" }
    // Returns: 200 OK with the updated status, or 422 for invalid state transitions
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeStatusRequest request)
    {
        if (!Enum.TryParse<WorkStatus>(request.NewStatus, true, out var newStatus))
        {
            return BadRequest("Invalid status value.");
        }

        var command = new ChangeStatusCommand(id, newStatus);
        var updatedStatus = await _mediator.Send(command);
        return Ok(new { status = updatedStatus.ToString() });
    }

    // GET /tasks?assignee=alice
    // Returns: 200 OK with tasks for that assignee, sorted by priority descending
    [HttpGet]
    public async Task<IActionResult> GetByAssignee([FromQuery] string assignee)
    {
        var query = new GetTasksByAssigneeQuery(assignee);
        var tasks = await _mediator.Send(query);
        return Ok(tasks);
    }

    // GET /tasks/overdue
    // Returns: 200 OK with overdue tasks sorted by deadline ascending
    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdue()
    {
        var query = new GetOverdueTasksQuery();
        var tasks = await _mediator.Send(query);
        return Ok(tasks);
    }
}
