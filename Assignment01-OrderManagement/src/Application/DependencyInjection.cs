using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Assignment01_OrderManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DependencyInjection>());
        return services;
    }
}
