namespace Assignment02_ProductInventory.Presentation.Models;

public record AddProductRequest(string Name, string Sku, int Stock, decimal Price);
