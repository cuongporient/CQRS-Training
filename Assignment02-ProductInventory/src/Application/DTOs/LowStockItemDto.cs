namespace Assignment02_ProductInventory.Application.DTOs;

public record LowStockItemDto(Guid Id, string Name, string Sku, int Stock);
