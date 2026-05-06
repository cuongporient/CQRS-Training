using MediatR;
using Assignment02_ProductInventory.Application.Interfaces;

namespace Assignment02_ProductInventory.Application.Queries;

public record LowStockItemDto(Guid Id, string Name, string Sku, int Stock);

public record GetLowStockQuery(int Threshold) : IRequest<List<LowStockItemDto>>;

public class GetLowStockQueryHandler : IRequestHandler<GetLowStockQuery, List<LowStockItemDto>>
{
    private readonly IProductRepository _productRepository;

    public GetLowStockQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<List<LowStockItemDto>> Handle(GetLowStockQuery request, CancellationToken cancellationToken)
    {
        // TODO 1: Get all products from repository
        // TODO 2: Filter: IsActive == true AND Stock <= Threshold
        // TODO 3: Map each result to LowStockItemDto, return list
        // ❓ QUESTION: This handler calls GetAll() then filters in memory.
        //    What problem does this cause at scale (e.g. 1 million products)?
        //    How would CQRS help address this at the logical level?
        throw new NotImplementedException();
    }
}
