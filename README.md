# CQRS Training Exercises

## Assignments

| # | Name | Focus |
|---|------|-------|
| 01 | Order Management | Place and cancel orders, query by ID |
| 02 | Product Inventory | Add products, restock, query low stock |
| 03 | Bank Account | Deposit, withdraw, transaction history |
| 04 | Task Manager | Create tasks, status transitions, filtering |

## Submission Workflow

1. **Fork** this repository to your own GitHub account
2. Clone your fork locally:
   ```bash
   git clone https://github.com/<your-username>/CQRS-Training.git
   cd CQRS-Training
   ```
3. Create a branch for your work:
   ```bash
   git checkout -b assignment01/<your-name>
   ```
4. Implement the assignment, commit, and push:
   ```bash
   git add .
   git commit -m "Assignment 01: implement handlers"
   git push origin assignment01/<your-name>
   ```
5. Open a **Pull Request** from your fork back to the original repository for review.

## Quick Start

```bash
cd Assignment01-OrderManagement
dotnet run --project src/Presentation
```

Then open `http://localhost:5000/swagger` to explore and test the API.

## Your Job

Each assignment has pre-built controllers and domain/infrastructure layers. You need to implement the **Application layer** — create the commands, queries, and their MediatR handlers inside `src/Application/Commands/` and `src/Application/Queries/`.

The controller action bodies also throw `NotImplementedException` — fill those in to wire up your commands and queries via `_mediator.Send(...)`.

## Project Structure

```
src/
├── Domain/           # Entities and enums — read only
├── Application/      # ← your work: commands, queries, handlers
│   ├── Commands/
│   ├── Queries/
│   └── Interfaces/   # Repository contracts
├── Infrastructure/   # Repository implementations — read only
└── Presentation/     # Controllers and request models — wire up only
```
