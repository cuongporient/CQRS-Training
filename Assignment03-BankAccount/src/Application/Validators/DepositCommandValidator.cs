using FluentValidation;
using Assignment03_BankAccount.Application.Commands;

namespace Assignment03_BankAccount.Application.Validators;

public class DepositCommandValidator : AbstractValidator<DepositCommand>
{
    public DepositCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("Account ID is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Deposit amount must be greater than zero.");
    }
}
