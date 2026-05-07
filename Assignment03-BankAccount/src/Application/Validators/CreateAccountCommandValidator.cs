using FluentValidation;
using Assignment03_BankAccount.Application.Commands;

namespace Assignment03_BankAccount.Application.Validators;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Owner)
            .NotEmpty().WithMessage("Owner name is required.")
            .MaximumLength(100).WithMessage("Owner name must not exceed 100 characters.");

        RuleFor(x => x.InitialBalance)
            .GreaterThanOrEqualTo(0).WithMessage("Initial balance must be non-negative.");
    }
}
