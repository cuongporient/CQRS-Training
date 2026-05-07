using Mediator.Net.Contracts;

namespace Assignment02_ProductInventory.Application.Commands;

public record RestockCommand(Guid ProductId, int Quantity) : IRequest;
