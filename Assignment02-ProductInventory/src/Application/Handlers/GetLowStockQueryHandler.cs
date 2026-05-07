using Mediator.Net;
using Mediator.Net.Contracts;
using Mediator.Net.Context;
using Assignment02_ProductInventory.Application.Interfaces;
using Assignment02_ProductInventory.Application.DTOs;
using Assignment02_ProductInventory.Application.Queries;
using Assignment02_ProductInventory.Application.Responses;

namespace Assignment02_ProductInventory.Application.Handlers;

public class GetLowStockQueryHandler : IRequestHandler<GetLowStockQuery, GetLowStockResponse>
{
    private readonly IProductRepository _repository;

    public GetLowStockQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetLowStockResponse> Handle(IReceiveContext<GetLowStockQuery> context, CancellationToken cancellationToken)
    {
        var request = context.Message;
        var lowStockItems = _repository.GetAll()
            .Where(p => p.IsActive && p.Stock <= request.Threshold)
            .Select(p => new LowStockItemDto(p.Id, p.Name, p.Sku, p.Stock))
            .ToList();

        return await Task.FromResult(new GetLowStockResponse(lowStockItems));
    }
}
