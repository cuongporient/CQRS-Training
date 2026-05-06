using MediatR;
using Assignment02_ProductInventory.Domain;
using Assignment02_ProductInventory.Application.Interfaces;

namespace Assignment02_ProductInventory.Application.Commands;

public record AddProductCommand(string Name, string Sku, int Stock, decimal Price) : IRequest<Guid>;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public AddProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        // TODO 1: Validate Name is not empty — throw ArgumentException
        // TODO 2: Validate Sku is not empty — throw ArgumentException
        // TODO 3: Validate Sku does not already exist (use repo.SkuExists) — throw ArgumentException
        // TODO 4: Validate Stock >= 0 — throw ArgumentException
        // TODO 5: Validate Price > 0 — throw ArgumentException
        // TODO 6: Create Product, add to repository, return Id
        throw new NotImplementedException();
    }
}
