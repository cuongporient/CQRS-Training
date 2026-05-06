using MediatR;
using Assignment01_OrderManagement.Application.Interfaces;

namespace Assignment01_OrderManagement.Application.Commands;

public record CancelOrderCommand(Guid OrderId) : IRequest<bool>;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public CancelOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        // TODO 1: Find order by OrderId — return false if not found
        // TODO 2: Return false if Status is already "Cancelled"
        // TODO 3: Set Status to "Cancelled", return true
        throw new NotImplementedException();
    }
}
