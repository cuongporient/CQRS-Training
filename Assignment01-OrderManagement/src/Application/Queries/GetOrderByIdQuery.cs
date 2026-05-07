using Assignment01_OrderManagement.Application.Interfaces;
using Assignment01_OrderManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01_OrderManagement.Application.Queries;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<Order?>;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order?>
{
    private readonly IOrderRepository _orderRepository;
    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        Order? currentOrder = _orderRepository.GetById(request.OrderId);

        return Task.FromResult(currentOrder);
    }
}

