using MediatR;
using Assignment02_ProductInventory.Application.Interfaces;

namespace Assignment02_ProductInventory.Application.Commands;

public record RestockCommand(Guid ProductId, int Quantity) : IRequest<int>;

public class RestockCommandHandler : IRequestHandler<RestockCommand, int>
{
    private readonly IProductRepository _productRepository;

    public RestockCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<int> Handle(RestockCommand request, CancellationToken cancellationToken)
    {
        // TODO 1: Find product by ProductId — throw ArgumentException if not found
        // TODO 2: Validate product IsActive == true — throw InvalidOperationException if not
        // TODO 3: Validate Quantity > 0 — throw ArgumentException
        // TODO 4: Add Quantity to product Stock, return new Stock value
        throw new NotImplementedException();
    }
}
