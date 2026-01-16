using Application.Abstractions.ProductAggregate;
using FluentValidation;

namespace Application.Validation;

public class CreateProductValidator : AbstractValidator<CreateProductModel>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(x => x.ProductGroupId)
            .NotEmpty().WithMessage("ProductGroupId is required.");
        RuleForEach(x => x.Localizations)
            .SetValidator(new ProductLocalizationValidator());
    }
}
