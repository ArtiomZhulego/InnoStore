using Application.Abstractions.ProductAggregate;
using FluentValidation;

namespace Presentation.Validation;

public class UpdateProductValidator : AbstractValidator<UpdateProductModel>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
        RuleFor(x => x.ProductCategoryId)
            .NotEmpty().WithMessage($"{nameof(UpdateProductModel.ProductCategoryId)} is required.");
        RuleForEach(x => x.Localizations)
            .SetValidator(new ProductLocalizationValidator());
    }
}
