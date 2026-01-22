using Application.Abstractions.ProductSizeAggregate;
using FluentValidation;

namespace Application.Validation;

public class CreateProductSizeValidator : AbstractValidator<CreateProductSizeModel>
{
    public CreateProductSizeValidator()
    {
        RuleFor(x => x.Size)
            .NotEmpty().WithMessage("Size is required.");
        RuleForEach(x => x.Localizations)
            .SetValidator(new ProductSizeLocalizationValidator());
    }
}
