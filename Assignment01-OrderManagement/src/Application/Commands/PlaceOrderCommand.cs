using System;
using System.Collections.Generic;
using System.Text;
using Assignment01_OrderManagement.Application.Interfaces;
using Assignment01_OrderManagement.Domain;
using MediatR;

namespace Assignment01_OrderManagement.Application.Commands;

public record PlaceOrderCommand(string CustomerName, List<OrderItemRequestDto> Items) : IRequest<Guid>;
public record OrderItemRequestDto(string ProductName, int Quantity, decimal Price);
public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;

    public PlaceOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<Guid> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        Order newOrder = new();

        newOrder.CustomerName = request.CustomerName;
        foreach (var item in request.Items)
        {
            OrderItem orderItem = new OrderItem(item.ProductName, item.Quantity, item.Price);
            newOrder.Items.Add(orderItem);
        }

        _orderRepository.Add(newOrder);

        return Task.FromResult(newOrder.Id);
    }
}


