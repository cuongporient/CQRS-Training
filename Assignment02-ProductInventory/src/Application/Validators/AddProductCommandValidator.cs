using Assignment02_ProductInventory.Application.Commands;
using Assignment02_ProductInventory.Application.Interfaces;
using FluentValidation;

namespace Assignment02_ProductInventory.Application.Validators;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator(IProductRepository repository)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must not be empty")
            .MaximumLength(200);

        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("Sku must not be empty")
            .MaximumLength(100)
            .Must(sku => !repository.SkuExists(sku))
            .WithMessage(x => $"Sku '{x.Sku}' already exists");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock must be >= 0");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be > 0");
    }
}
