using Mediator.Net;
using Mediator.Net.Contracts;
using Mediator.Net.Context;
using Assignment02_ProductInventory.Application.Interfaces;
using Assignment02_ProductInventory.Application.Commands;
using Assignment02_ProductInventory.Application.Responses;

namespace Assignment02_ProductInventory.Application.Handlers;

public class RestockCommandHandler : IRequestHandler<RestockCommand, RestockResponse>
{
    private readonly IProductRepository _repository;

    public RestockCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<RestockResponse> Handle(IReceiveContext<RestockCommand> context, CancellationToken cancellationToken)
    {
        var request = context.Message;

        var product = _repository.GetById(request.ProductId)!;

        product.Stock += request.Quantity;

        return await Task.FromResult(new RestockResponse(product.Stock));
    }
}
