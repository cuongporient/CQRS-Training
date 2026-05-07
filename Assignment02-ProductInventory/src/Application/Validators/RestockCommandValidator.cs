using Assignment02_ProductInventory.Application.Commands;
using Assignment02_ProductInventory.Application.Interfaces;
using FluentValidation;

namespace Assignment02_ProductInventory.Application.Validators;

public class RestockCommandValidator : AbstractValidator<RestockCommand>
{
    public RestockCommandValidator(IProductRepository repository)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId must not be empty")
            .Must(id => repository.GetById(id) is not null)
            .WithMessage(x => $"Product with id '{x.ProductId}' not found")
            .Must(id =>
            {
                var product = repository.GetById(id);
                return product is not null && product.IsActive;
            })
            .WithMessage("Product must be active to restock");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be > 0");
    }
}
