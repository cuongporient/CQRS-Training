using Assignment01_OrderManagement.Application.Interfaces;
using Assignment01_OrderManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01_OrderManagement.Application.Commands;

public record CancelOrderCommand(Guid orderId) : IRequest<bool>;

public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public CancelOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        Order? currentOrder =  _orderRepository.GetById(request.orderId);

        if (currentOrder != null && currentOrder.Status != "Cancelled")
        {
            currentOrder.Status = "Cancelled";
            return Task.FromResult(true);
            
        }
        return Task.FromResult(false);
    }
}

