# Assignment 01: Order Management

## Focus
This assignment introduces CQRS fundamentals using a simple order management scenario. You'll implement commands that validate customer and item data before creating orders, and queries to retrieve order information. The handlers demonstrate how to separate write operations (commands) from read operations (queries).

## Commands to Implement

### PlaceOrderCommand
**Signature:** `PlaceOrderCommand(string CustomerName, List<OrderItem> Items) → Guid`

**Handler Location:** `src/Application/Commands/PlaceOrderCommand.cs`

**Validation Rules:**
- CustomerName must not be empty (throw `ArgumentException`)
- Items list must not be empty (throw `ArgumentException`)

### CancelOrderCommand
**Signature:** `CancelOrderCommand(Guid OrderId) → bool`

**Handler Location:** `src/Application/Commands/CancelOrderCommand.cs`

**Validation Rules:**
- Return `false` if order not found
- Return `false` if order status is already "Cancelled"

## Queries to Implement

### GetOrderByIdQuery
**Signature:** `GetOrderByIdQuery(Guid OrderId) → Order?`

**Handler Location:** `src/Application/Queries/GetOrderByIdQuery.cs`

Returns the Order entity (or null if not found).

## Discussion Question

> ❓ **QUESTION:** This query returns the Order entity directly instead of a DTO. What problems could this cause in a real-world application?

**Think about:** Exposing internal domain models, schema evolution, client coupling, testing complexity.

## Expected Console Output

When all handlers are correctly implemented, running `dotnet run --project src/ConsoleApp` should produce:

```
[OK] PlaceOrderCommand succeeded: Order ID = <guid>
[OK] GetOrderByIdQuery succeeded: Alice, Status = Pending
[OK] CancelOrderCommand succeeded: Order cancelled
[OK] CancelOrderCommand correctly returned false for already-cancelled order
[OK] PlaceOrderCommand correctly threw ArgumentException: <message>
```
