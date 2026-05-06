using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Assignment02_ProductInventory.Domain;
using Assignment02_ProductInventory.Application;
using Assignment02_ProductInventory.Infrastructure;
using Assignment02_ProductInventory.Application.Commands;
using Assignment02_ProductInventory.Application.Queries;

// Setup DI
var services = new ServiceCollection();
services.AddApplication();
services.AddInfrastructure();

var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetRequiredService<IMediator>();

try
{
    // Test 1: Add three products
    var p1Id = await mediator.Send(new AddProductCommand("Pen", "PEN-001", 5, 1.99m));
    Console.WriteLine($"[OK] Added Product 1 (PEN-001): {p1Id}");

    var p2Id = await mediator.Send(new AddProductCommand("Notebook", "NOTE-002", 2, 3.99m));
    Console.WriteLine($"[OK] Added Product 2 (NOTE-002): {p2Id}");

    var p3Id = await mediator.Send(new AddProductCommand("Ruler", "RUL-003", 50, 0.99m));
    Console.WriteLine($"[OK] Added Product 3 (RUL-003): {p3Id}");

    // Test 2: Restock Product 1
    var newStock = await mediator.Send(new RestockCommand(p1Id, 10));
    if (newStock == 15)
    {
        Console.WriteLine($"[OK] Restock Product 1: new stock = {newStock}");
    }
    else
    {
        Console.WriteLine($"[FAIL] Expected stock 15, got {newStock}");
    }

    // Test 3: Get low stock items (threshold = 10)
    var lowStockItems = await mediator.Send(new GetLowStockQuery(10));
    if (lowStockItems.Count == 1 && lowStockItems[0].Sku == "NOTE-002")
    {
        Console.WriteLine($"[OK] GetLowStockQuery returned {lowStockItems.Count} item: {lowStockItems[0].Name}");
    }
    else
    {
        Console.WriteLine($"[FAIL] Expected 1 low stock item (NOTE-002), got {lowStockItems.Count}");
    }

    // Test 4: Try adding duplicate SKU (should throw ArgumentException)
    try
    {
        await mediator.Send(new AddProductCommand("Pen Premium", "PEN-001", 20, 2.99m));
        Console.WriteLine("[FAIL] Expected ArgumentException for duplicate SKU");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"[OK] AddProductCommand correctly threw ArgumentException for duplicate SKU");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR] Unexpected exception: {ex.Message}");
}
