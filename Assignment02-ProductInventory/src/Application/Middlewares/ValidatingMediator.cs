using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment02_ProductInventory.Application.Middlewares;

public sealed class ValidatingMediator : IMediator
{
    private readonly IMediator _inner;
    private readonly IServiceProvider _serviceProvider;

    public ValidatingMediator(IMediator inner, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(inner);
        ArgumentNullException.ThrowIfNull(serviceProvider);

        _inner = inner;
        _serviceProvider = serviceProvider;
    }

    public Task SendAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : ICommand
    {
        return _inner.SendAsync(message, cancellationToken);
    }

    public Task<TResponse> SendAsync<TMessage, TResponse>(TMessage message, CancellationToken cancellationToken)
        where TMessage : ICommand
        where TResponse : IResponse
    {
        return _inner.SendAsync<TMessage, TResponse>(message, cancellationToken);
    }

    public Task SendAsync<TMessage>(IReceiveContext<TMessage> context, CancellationToken cancellationToken)
        where TMessage : ICommand
    {
        return _inner.SendAsync(context, cancellationToken);
    }

    public Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : IEvent
    {
        return _inner.PublishAsync(message, cancellationToken);
    }

    public Task PublishAsync<TMessage>(IReceiveContext<TMessage> context, CancellationToken cancellationToken)
        where TMessage : IEvent
    {
        return _inner.PublishAsync(context, cancellationToken);
    }

    public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest
        where TResponse : IResponse
    {
        await ValidateAsync(request!, cancellationToken);
        return await _inner.RequestAsync<TRequest, TResponse>(request, cancellationToken);
    }

    public async Task<TResponse> RequestAsync<TRequest, TResponse>(IReceiveContext<TRequest> context, CancellationToken cancellationToken)
        where TRequest : IRequest
        where TResponse : IResponse
    {
        await ValidateAsync(context.Message!, cancellationToken);
        return await _inner.RequestAsync<TRequest, TResponse>(context, cancellationToken);
    }

    public IAsyncEnumerable<TResponse> CreateStream<TRequest, TResponse>(IReceiveContext<TRequest> context, CancellationToken cancellationToken)
        where TRequest : IMessage
        where TResponse : IResponse
    {
        return _inner.CreateStream<TRequest, TResponse>(context, cancellationToken);
    }

    public IAsyncEnumerable<TResponse> CreateStream<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IMessage
        where TResponse : IResponse
    {
        return _inner.CreateStream<TRequest, TResponse>(request, cancellationToken);
    }

    public void Dispose()
    {
        (_inner as IDisposable)?.Dispose();
    }

    private async Task ValidateAsync(object message, CancellationToken cancellationToken)
    {
        if (message is not IRequest)
        {
            return;
        }

        var messageType = message.GetType();
        var validatorType = typeof(IValidator<>).MakeGenericType(messageType);
        var validators = _serviceProvider.GetServices(validatorType).Cast<object>().ToList();

        if (validators.Count == 0)
        {
            return;
        }

        foreach (var validator in validators)
        {
            var validationResult = await InvokeValidateAsync(validator, message, messageType, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }

    private static async Task<ValidationResult> InvokeValidateAsync(object validator, object message, Type messageType, CancellationToken cancellationToken)
    {
        var method = validator.GetType().GetMethod("ValidateAsync", BindingFlags.Public | BindingFlags.Instance, [messageType, typeof(CancellationToken)]);
        if (method == null)
        {
            throw new InvalidOperationException($"Validator '{validator.GetType().FullName}' does not expose ValidateAsync for '{messageType.FullName}'.");
        }

        var task = (Task)method.Invoke(validator, [message, cancellationToken])!;
        await task.ConfigureAwait(false);

        var resultProperty = task.GetType().GetProperty("Result");
        if (resultProperty?.GetValue(task) is not ValidationResult validationResult)
        {
            throw new InvalidOperationException($"Validator '{validator.GetType().FullName}' returned an unexpected result type.");
        }

        return validationResult;
    }
}