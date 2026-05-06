using Microsoft.Extensions.DependencyInjection;
using Assignment01_OrderManagement.Application.Interfaces;

namespace Assignment01_OrderManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IOrderRepository, OrderRepository>();
        return services;
    }
}
