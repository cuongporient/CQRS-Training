# Assignment 04: Task Manager

## Focus
This assignment demonstrates CQRS with complex filtering, sorting, and state machine validation. You'll implement commands that enforce strict state transitions, and queries that perform sophisticated filtering and sorting on domain models. This shows how CQRS is particularly valuable when read and write logic have very different requirements — writes need strict validation, reads need flexible filtering.

## Commands to Implement

### CreateTaskCommand
**Signature:** `CreateTaskCommand(string Title, string AssignedTo, Priority Priority, DateTime? Deadline) → Guid`

**Handler Location:** `src/Application/Commands/CreateTaskCommand.cs`

**Validation Rules:**
- Title must not be empty (throw `ArgumentException`)
- AssignedTo must not be empty (throw `ArgumentException`)

### ChangeStatusCommand
**Signature:** `ChangeStatusCommand(Guid TaskId, WorkStatus NewStatus) → WorkStatus`

**Handler Location:** `src/Application/Commands/ChangeStatusCommand.cs`

**Validation Rules:**
- Task must exist (throw `ArgumentException`)
- **State transitions are restricted:**
  - `Todo` → `InProgress` ✓ allowed
  - `InProgress` → `Done` ✓ allowed
  - **All other transitions** → throw `InvalidOperationException` with descriptive message (e.g., "Cannot transition from Done to InProgress")

## Queries to Implement

### GetTasksByAssigneeQuery
**Signature:** `GetTasksByAssigneeQuery(string Assignee) → List<TaskSummaryDto>`

**Handler Location:** `src/Application/Queries/GetTasksByAssigneeQuery.cs`

**DTO:** `TaskSummaryDto(Guid Id, string Title, WorkStatus Status, Priority Priority, DateTime? Deadline)`

**Filter & Sort Logic:**
- Filter: AssignedTo equals Assignee (case-insensitive)
- Sort: By Priority descending (High → Medium → Low)

### GetOverdueTasksQuery
**Signature:** `GetOverdueTasksQuery() → List<TaskSummaryDto>`

**Handler Location:** `src/Application/Queries/GetOverdueTasksQuery.cs`

**DTO:** Reuse `TaskSummaryDto`

**Filter & Sort Logic:**
- Filter: `Deadline != null AND Deadline < DateTime.UtcNow AND Status != Done`
- Sort: By Deadline ascending (most overdue first)

## Discussion Question

> ❓ **QUESTION:** This exercise uses a dedicated ChangeStatusCommand rather than a generic UpdateTaskCommand(..., newStatus, ...). When does splitting commands like this make sense in CQRS, and when would a single general-purpose update command be more appropriate?

**Think about:** Command semantics, validation complexity, auditability, domain intent, and command explosion.

## Expected Console Output

When all handlers are correctly implemented, running `dotnet run --project src/ConsoleApp` should produce:

```
[OK] Created 3 tasks
[OK] ChangeStatusCommand: Todo → InProgress succeeded
[OK] ChangeStatusCommand correctly rejected invalid transition
[OK] GetTasksByAssigneeQuery('alice') returned 2 tasks
  - Deploy to staging (Priority: High)
  - Write unit tests (Priority: High)
[OK] GetOverdueTasksQuery returned 1 overdue task(s)
  - Write unit tests
```

*Note: Task order in GetTasksByAssigneeQuery is by Priority descending (both alice's tasks are High priority, so order between them may vary — the test checks count and task names, not order).*
