using Mediator.Net.Contracts;

namespace Assignment02_ProductInventory.Application.Commands;

public record AddProductCommand(string Name, string Sku, int Stock, decimal Price) : IRequest;
