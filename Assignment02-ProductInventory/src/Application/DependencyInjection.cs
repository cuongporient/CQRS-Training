using Microsoft.Extensions.DependencyInjection;
using Mediator.Net;
using FluentValidation;

namespace Assignment02_ProductInventory.Application;

internal class ServiceProviderDependencyScope : IDependencyScope
{
    private readonly IServiceProvider _provider;
    private readonly IServiceScope? _scope;

    public ServiceProviderDependencyScope(IServiceProvider provider)
    {
        _provider = provider;
    }

    private ServiceProviderDependencyScope(IServiceScope scope)
    {
        _scope = scope;
        _provider = scope.ServiceProvider;
    }

    public T Resolve<T>()
    {
        var svc = _provider.GetService(typeof(T));
        if (svc != null) return (T)svc;
        return (T)CreateViaActivator(typeof(T));
    }

    public object Resolve(Type type)
    {
        var svc = _provider.GetService(type);
        if (svc != null) return svc;
        return CreateViaActivator(type);
    }
    
    private object CreateViaActivator(Type type)
    {
        // Prefer ActivatorUtilities so constructor dependencies are resolved from the provider.
        return Microsoft.Extensions.DependencyInjection.ActivatorUtilities.CreateInstance(_provider, type);
    }

    public IDependencyScope BeginScope()
    {
        var factory = _provider.GetService<IServiceScopeFactory>();
        if (factory == null) return this;
        var scope = factory.CreateScope();
        return new ServiceProviderDependencyScope(scope);
    }

    public void Dispose()
    {
        _scope?.Dispose();
    }
}

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Register the mediator using the service provider so handlers are created via DI
        services.AddSingleton<IMediator>(sp =>
        {
            var mediatorBuilder = new MediatorBuilder();
            mediatorBuilder.RegisterHandlers(typeof(DependencyInjection).Assembly);
            var depScope = new ServiceProviderDependencyScope(sp);
            return mediatorBuilder.Build(depScope);
        });

        return services;
    }
}
