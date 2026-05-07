using Mediator.Net.Contracts;
using Assignment02_ProductInventory.Application.DTOs;

namespace Assignment02_ProductInventory.Application.Responses;

public class GetLowStockResponse : IResponse
{
    public List<LowStockItemDto> Items { get; }

    public GetLowStockResponse(List<LowStockItemDto> items)
    {
        Items = items;
    }
}
