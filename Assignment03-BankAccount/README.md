# Assignment 03: Bank Account

## Focus
This assignment demonstrates CQRS in a financial transaction scenario where commands have side effects across multiple entities. You'll implement deposit and withdrawal commands that modify account balance and generate transaction logs, plus a query that retrieves history. The key learning is understanding data consistency when a command affects multiple parts of the system — a critical concern in real financial systems.

## Commands to Implement

### DepositCommand
**Signature:** `DepositCommand(Guid AccountId, decimal Amount) → decimal`

**Handler Location:** `src/Application/Commands/DepositCommand.cs`

**Validation Rules:**
- Account must exist (throw `ArgumentException`)
- Amount must be > 0 (throw `ArgumentException`)

**Side Effects:**
- Increase account Balance
- Write TransactionLog with Type="Deposit"

### WithdrawCommand
**Signature:** `WithdrawCommand(Guid AccountId, decimal Amount) → decimal`

**Handler Location:** `src/Application/Commands/WithdrawCommand.cs`

**Validation Rules:**
- Account must exist (throw `ArgumentException`)
- Amount must be > 0 (throw `ArgumentException`)
- Balance must be >= Amount (throw `InvalidOperationException` with message "Insufficient balance")

**Side Effects:**
- Decrease account Balance
- Write TransactionLog with Type="Withdrawal"

## Queries to Implement

### GetTransactionHistoryQuery
**Signature:** `GetTransactionHistoryQuery(Guid AccountId) → List<TransactionDto>`

**Handler Location:** `src/Application/Queries/GetTransactionHistoryQuery.cs`

**DTO:** `TransactionDto(string Type, decimal Amount, decimal BalanceAfter, DateTime CreatedAt)`

Logs are already sorted descending by CreatedAt from the repository.

## Discussion Question

> ❓ **QUESTION:** The handler modifies BankAccount balance AND writes a TransactionLog in two separate steps. If the log write fails after the balance was already updated, what happens to the data? What mechanism would a real database need?

**Think about:** Transactions, ACID properties, rollback, event sourcing, or saga patterns.

## Expected Console Output

When all handlers are correctly implemented, running `dotnet run --project src/ConsoleApp` should produce:

```
[OK] Account seeded: Bob, Balance = 500000
[OK] DepositCommand succeeded: new balance = 700000
[OK] WithdrawCommand succeeded: new balance = 600000
[OK] WithdrawCommand correctly threw InvalidOperationException: Insufficient balance
[OK] GetTransactionHistoryQuery returned 2 records
  - Deposit: 200000, Balance after: 700000
  - Withdrawal: 100000, Balance after: 600000
```
