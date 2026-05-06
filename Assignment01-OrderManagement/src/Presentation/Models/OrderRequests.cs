namespace Assignment01_OrderManagement.Presentation.Models;

public record PlaceOrderRequest(string CustomerName, List<OrderItemRequest> Items);

public record OrderItemRequest(string ProductName, int Quantity, decimal Price);
