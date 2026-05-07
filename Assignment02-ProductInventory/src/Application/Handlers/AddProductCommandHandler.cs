using Mediator.Net;
using Mediator.Net.Contracts;
using Mediator.Net.Context;
using Assignment02_ProductInventory.Domain;
using Assignment02_ProductInventory.Application.Interfaces;
using Assignment02_ProductInventory.Application.Commands;
using Assignment02_ProductInventory.Application.Responses;
using FluentValidation;

namespace Assignment02_ProductInventory.Application.Handlers;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductResponse>
{
    private readonly IProductRepository _repository;
    private readonly IValidator<AddProductCommand> _validator;

    public AddProductCommandHandler(IProductRepository repository, IValidator<AddProductCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<AddProductResponse> Handle(IReceiveContext<AddProductCommand> context, CancellationToken cancellationToken)
    {
        var request = context.Message;
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

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
