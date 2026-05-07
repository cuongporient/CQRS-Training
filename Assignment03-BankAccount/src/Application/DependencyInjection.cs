using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Assignment03_BankAccount.Application.Behaviors;
using Assignment03_BankAccount.Application.Commands;
using Assignment03_BankAccount.Application.Queries;
using Assignment03_BankAccount.Application.Validators;

namespace Assignment03_BankAccount.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator();

        // Register the validation pipeline behavior (open generic — applied to all request types)
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // Register validators
        services.AddScoped<IValidator<CreateAccountCommand>, CreateAccountCommandValidator>();
        services.AddScoped<IValidator<DepositCommand>, DepositCommandValidator>();
        services.AddScoped<IValidator<WithdrawCommand>, WithdrawCommandValidator>();
        services.AddScoped<IValidator<GetTransactionHistoryQuery>, GetTransactionHistoryQueryValidator>();

        return services;
    }
}
