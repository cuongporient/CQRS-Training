using Assignment02_ProductInventory.Domain;

namespace Assignment02_ProductInventory.Application.Interfaces;

public interface IProductRepository
{
    void Add(Product product);
    Product? GetById(Guid id);
    bool SkuExists(string sku);
    List<Product> GetAll();
}
