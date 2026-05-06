using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Assignment02_ProductInventory.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DependencyInjection>());
        return services;
    }
}
