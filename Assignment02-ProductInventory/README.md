# Assignment 02: Product Inventory

## Focus
This assignment extends CQRS with more comprehensive validation, repository methods, and filtering logic. You'll implement commands that validate product data against existing inventory, and queries that retrieve filtered product lists. This demonstrates how CQRS separates complex validation logic (commands) from complex filtering and projection logic (queries).

## Commands to Implement

### AddProductCommand
**Signature:** `AddProductCommand(string Name, string Sku, int Stock, decimal Price) → Guid`

**Handler Location:** `src/Application/Commands/AddProductCommand.cs`

**Validation Rules:**
- Name must not be empty (throw `ArgumentException`)
- Sku must not be empty (throw `ArgumentException`)
- Sku must not already exist in repository (throw `ArgumentException`)
- Stock must be >= 0 (throw `ArgumentException`)
- Price must be > 0 (throw `ArgumentException`)

### RestockCommand
**Signature:** `RestockCommand(Guid ProductId, int Quantity) → int`

**Handler Location:** `src/Application/Commands/RestockCommand.cs`

**Validation Rules:**
- Product must exist (throw `ArgumentException`)
- Product must be active (IsActive == true, throw `InvalidOperationException`)
- Quantity must be > 0 (throw `ArgumentException`)

## Queries to Implement

### GetLowStockQuery
**Signature:** `GetLowStockQuery(int Threshold) → List<LowStockItemDto>`

**Handler Location:** `src/Application/Queries/GetLowStockQuery.cs`

**DTO:** `LowStockItemDto(Guid Id, string Name, string Sku, int Stock)`

**Filter Logic:**
- Only active products (IsActive == true)
- Stock <= Threshold

## Discussion Question

> ❓ **QUESTION:** This handler calls GetAll() then filters in memory. What problem does this cause at scale (e.g. 1 million products)? How would CQRS help address this at the logical level?

**Think about:** Memory usage, query performance, the separation between write and read models.

## Expected Console Output

When all handlers are correctly implemented, running `dotnet run --project src/ConsoleApp` should produce:

```
[OK] Added Product 1 (PEN-001): <guid>
[OK] Added Product 2 (NOTE-002): <guid>
[OK] Added Product 3 (RUL-003): <guid>
[OK] Restock Product 1: new stock = 15
[OK] GetLowStockQuery returned 1 item: Notebook
[OK] AddProductCommand correctly threw ArgumentException for duplicate SKU
```
