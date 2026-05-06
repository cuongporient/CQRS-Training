using Microsoft.Extensions.DependencyInjection;
using Assignment03_BankAccount.Application.Interfaces;

namespace Assignment03_BankAccount.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IBankRepository, BankRepository>();
        return services;
    }
}
