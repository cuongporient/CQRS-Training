using Mediator.Net;
using Mediator.Net.Contracts;
using Mediator.Net.Context;
using Assignment02_ProductInventory.Domain;
using Assignment02_ProductInventory.Application.Interfaces;
using Assignment02_ProductInventory.Application.Commands;
using Assignment02_ProductInventory.Application.Responses;

namespace Assignment02_ProductInventory.Application.Handlers;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductResponse>
{
    private readonly IProductRepository _repository;

    public AddProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<AddProductResponse> Handle(IReceiveContext<AddProductCommand> context, CancellationToken cancellationToken)
    {
        var request = context.Message;

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Sku = request.Sku,
            Stock = request.Stock,
            Price = request.Price,
            IsActive = true
        };

        _repository.Add(product);

        return await Task.FromResult(new AddProductResponse(product.Id));
    }
}
