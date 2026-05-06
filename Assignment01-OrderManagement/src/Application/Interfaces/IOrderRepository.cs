using Assignment01_OrderManagement.Domain;

namespace Assignment01_OrderManagement.Application.Interfaces;

public interface IOrderRepository
{
    void Add(Order order);
    Order? GetById(Guid id);
}
