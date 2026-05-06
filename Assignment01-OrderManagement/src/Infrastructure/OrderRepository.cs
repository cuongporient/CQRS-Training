using Assignment01_OrderManagement.Domain;
using Assignment01_OrderManagement.Application.Interfaces;

namespace Assignment01_OrderManagement.Infrastructure;

public class OrderRepository : IOrderRepository
{
    private static readonly List<Order> _orders = new();

    public void Add(Order order)
    {
        _orders.Add(order);
    }

    public Order? GetById(Guid id)
    {
        return _orders.FirstOrDefault(o => o.Id == id);
    }
}
