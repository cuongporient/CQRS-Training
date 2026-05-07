using Mediator.Net.Contracts;

namespace Assignment02_ProductInventory.Application.Responses;

public record AddProductResponse(Guid Id) : IResponse;
