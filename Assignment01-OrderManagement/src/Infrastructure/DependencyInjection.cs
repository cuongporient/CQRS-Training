using Microsoft.Extensions.DependencyInjection;
using Assignment01_OrderManagement.Application.Interfaces;

namespace Assignment01_OrderManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}
