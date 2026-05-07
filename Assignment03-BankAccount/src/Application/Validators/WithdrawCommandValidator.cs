using FluentValidation;
using Assignment03_BankAccount.Application.Commands;

namespace Assignment03_BankAccount.Application.Validators;

public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
{
    public WithdrawCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("Account ID is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Withdrawal amount must be greater than zero.");
    }
}
