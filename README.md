# CQRS Training Exercises

## What is CQRS?

Command Query Responsibility Segregation (CQRS) is a pattern that separates the logic for reading data (queries) from the logic for writing/modifying data (commands). In a CQRS architecture, you create different objects for updating data and for reading data. This separation allows each side to be optimized independently — commands focus on business rules and state changes, while queries focus on efficient data retrieval. At the logical level, CQRS structures your application to make the intent of operations explicit: when you invoke a command, it's clear that something is changing; when you invoke a query, it's clear that you're asking for information without side effects.

## Assignments

| # | Name | Focus |
|---|------|-------|
| 01 | Order Management | Basic CQRS with commands and queries, validation, and handler implementation |
| 02 | Product Inventory | Command validation, repository methods, filtering, and scaling concerns |
| 03 | Bank Account | State consistency, transaction logging, and atomicity challenges |
| 04 | Task Manager | Complex filtering, enum-based validation, and command constraints |

## Setup

**Prerequisites:**
- .NET 8 SDK or later.

**Quick Start:**
```bash
# Clone/navigate to the repository
cd CQRS-Training

# Navigate to any assignment folder
cd Assignment01-OrderManagement

# Restore dependencies and run
dotnet restore
dotnet run --project src/ConsoleApp
```

## How It Works

All test cases in `Program.cs` are **pre-written and ready to run**. Your job is to implement the handler bodies in the Application layer.

Each handler includes structured TODO comments that guide implementation:
```csharp
public async Task<Guid> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
{
    // TODO 1: Validate CustomerName is not empty — throw ArgumentException if violated
    // TODO 2: Validate Items is not empty — throw ArgumentException if violated
    // TODO 3: Create new Order from command data, add to repository
    // TODO 4: Return the new Order's Id
    throw new NotImplementedException();
}
```

When you complete all handlers correctly, `dotnet run` will print `[OK]` for each passing test case.
