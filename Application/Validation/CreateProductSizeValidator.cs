using Application.Abstractions.ProductSizeAggregate;
using FluentValidation;

namespace Application.Validation;

public class CreateProductSizeValidator : AbstractValidator<CreateProductSizeModel>
{
    public CreateProductSizeValidator()
    {
        RuleForEach(x => x.Localizations)
            .SetValidator(new ProductSizeLocalizationValidator());
    }
}
