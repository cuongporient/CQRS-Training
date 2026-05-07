using Mediator.Net;
using Mediator.Net.Contracts;
using Mediator.Net.Context;
using Assignment02_ProductInventory.Application.Interfaces;
using Assignment02_ProductInventory.Application.DTOs;
using Assignment02_ProductInventory.Application.Queries;
using Assignment02_ProductInventory.Application.Responses;
using FluentValidation;

namespace Assignment02_ProductInventory.Application.Handlers;

public class GetLowStockQueryHandler : IRequestHandler<GetLowStockQuery, GetLowStockResponse>
{
    private readonly IProductRepository _repository;
    private readonly IValidator<GetLowStockQuery> _validator;

    public GetLowStockQueryHandler(IProductRepository repository, IValidator<GetLowStockQuery> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<GetLowStockResponse> Handle(IReceiveContext<GetLowStockQuery> context, CancellationToken cancellationToken)
    {
        var request = context.Message;
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var lowStockItems = _repository.GetAll()
            .Where(p => p.IsActive && p.Stock <= request.Threshold)
            .Select(p => new LowStockItemDto(p.Id, p.Name, p.Sku, p.Stock))
            .ToList();

        return await Task.FromResult(new GetLowStockResponse(lowStockItems));
    }
}
