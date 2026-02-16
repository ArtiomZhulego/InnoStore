using Application.Abstractions.ProductCategoryAggregate;
using FluentValidation;

namespace Presentation.Validation;

public class CreateProductCategoryValidator : AbstractValidator<CreateProductCategoryModel>
{
    public CreateProductCategoryValidator()
    {
        RuleForEach(x => x.Localizations)
            .SetValidator(new ProductCategoryLocalizationValidator());
    }
}
