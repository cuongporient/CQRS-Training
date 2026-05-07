using Mediator.Net;
using Mediator.Net.Contracts;
using Mediator.Net.Context;
using Assignment02_ProductInventory.Application.Interfaces;
using Assignment02_ProductInventory.Application.Commands;
using Assignment02_ProductInventory.Application.Responses;
using FluentValidation;

namespace Assignment02_ProductInventory.Application.Handlers;

public class RestockCommandHandler : IRequestHandler<RestockCommand, RestockResponse>
{
    private readonly IProductRepository _repository;
    private readonly IValidator<RestockCommand> _validator;

    public RestockCommandHandler(IProductRepository repository, IValidator<RestockCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<RestockResponse> Handle(IReceiveContext<RestockCommand> context, CancellationToken cancellationToken)
    {
        var request = context.Message;
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var product = _repository.GetById(request.ProductId);
        if (product == null) throw new ValidationException($"Product with id '{request.ProductId}' not found");
        if (!product.IsActive) throw new ValidationException("Product must be active to restock");

        product.Stock += request.Quantity;

        return await Task.FromResult(new RestockResponse(product.Stock));
    }
}
