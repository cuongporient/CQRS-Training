using Microsoft.Extensions.DependencyInjection;
using Assignment02_ProductInventory.Application.Interfaces;

namespace Assignment02_ProductInventory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
