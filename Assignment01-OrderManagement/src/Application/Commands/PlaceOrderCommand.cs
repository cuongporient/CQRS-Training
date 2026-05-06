using MediatR;
using Assignment01_OrderManagement.Domain;
using Assignment01_OrderManagement.Application.Interfaces;

namespace Assignment01_OrderManagement.Application.Commands;

public record PlaceOrderCommand(string CustomerName, List<OrderItem> Items) : IRequest<Guid>;

public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;

    public PlaceOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<Guid> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        // TODO 1: Validate CustomerName is not empty — throw ArgumentException if violated
        // TODO 2: Validate Items is not empty — throw ArgumentException if violated
        // TODO 3: Create new Order from command data, add to repository
        // TODO 4: Return the new Order's Id
        throw new NotImplementedException();
    }
}
