using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Assignment01_OrderManagement.Domain;
using Assignment01_OrderManagement.Application;
using Assignment01_OrderManagement.Infrastructure;
using Assignment01_OrderManagement.Application.Commands;
using Assignment01_OrderManagement.Application.Queries;

// Setup DI
var services = new ServiceCollection();
services.AddApplication();
services.AddInfrastructure();

var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetRequiredService<IMediator>();

try
{
    // Test 1: PlaceOrderCommand with valid data
    var items1 = new List<OrderItem> { new("Book", 2, 15.99m) };
    var orderId = await mediator.Send(new PlaceOrderCommand("Alice", items1));
    Console.WriteLine($"[OK] PlaceOrderCommand succeeded: Order ID = {orderId}");

    // Test 2: GetOrderByIdQuery
    var order = await mediator.Send(new GetOrderByIdQuery(orderId));
    if (order != null)
    {
        Console.WriteLine($"[OK] GetOrderByIdQuery succeeded: {order.CustomerName}, Status = {order.Status}");
    }

    // Test 3: CancelOrderCommand (first cancellation)
    var cancelResult = await mediator.Send(new CancelOrderCommand(orderId));
    if (cancelResult)
    {
        Console.WriteLine($"[OK] CancelOrderCommand succeeded: Order cancelled");
    }

    // Test 4: CancelOrderCommand (already cancelled, should return false)
    var cancelAgain = await mediator.Send(new CancelOrderCommand(orderId));
    if (!cancelAgain)
    {
        Console.WriteLine($"[OK] CancelOrderCommand correctly returned false for already-cancelled order");
    }

    // Test 5: PlaceOrderCommand with invalid data (empty CustomerName)
    try
    {
        var items2 = new List<OrderItem> { new("Pen", 10, 0.99m) };
        await mediator.Send(new PlaceOrderCommand("", items2));
        Console.WriteLine("[FAIL] Expected ArgumentException for empty CustomerName");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"[OK] PlaceOrderCommand correctly threw ArgumentException: {ex.Message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR] Unexpected exception: {ex.Message}");
}
