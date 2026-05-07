using FluentValidation;
using Assignment03_BankAccount.Application.Queries;

namespace Assignment03_BankAccount.Application.Validators;

public class GetTransactionHistoryQueryValidator : AbstractValidator<GetTransactionHistoryQuery>
{
    public GetTransactionHistoryQueryValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("Account ID is required.");
    }
}
