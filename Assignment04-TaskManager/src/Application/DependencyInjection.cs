using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Assignment04_TaskManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DependencyInjection>());
        return services;
    }
}
