using Assignment02_ProductInventory.Domain;
using Assignment02_ProductInventory.Application.Interfaces;

namespace Assignment02_ProductInventory.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public Product? GetById(Guid id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public bool SkuExists(string sku)
    {
        return _products.Any(p => p.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));
    }

    public List<Product> GetAll()
    {
        return _products.ToList();
    }
}
