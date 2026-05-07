using Assignment02_ProductInventory.Application.Queries;
using FluentValidation;

namespace Assignment02_ProductInventory.Application.Validators;

public class GetLowStockQueryValidator : AbstractValidator<GetLowStockQuery>
{
    public GetLowStockQueryValidator()
    {
        RuleFor(x => x.Threshold)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Threshold must be >= 0");
    }
}
