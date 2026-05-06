using Microsoft.Extensions.DependencyInjection;
using Assignment04_TaskManager.Application.Interfaces;

namespace Assignment04_TaskManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ITaskRepository, TaskRepository>();
        return services;
    }
}
