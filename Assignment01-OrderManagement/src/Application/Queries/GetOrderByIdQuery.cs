using MediatR;
using Assignment01_OrderManagement.Domain;
using Assignment01_OrderManagement.Application.Interfaces;

namespace Assignment01_OrderManagement.Application.Queries;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<Order?>;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO 1: Find order by OrderId
        // TODO 2: Return the Order, or null if not found
        // ❓ QUESTION: This query returns the Order entity directly instead of a DTO.
        //    What problems could this cause in a real-world application?
        throw new NotImplementedException();
    }
}
