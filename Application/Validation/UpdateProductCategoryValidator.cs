using Application.Abstractions.ProductCategoryAggregate;
using FluentValidation;

namespace Presentation.Validation;

public class UpdateProductCategoryValidator : AbstractValidator<UpdateProductCategoryModel>
{
    public UpdateProductCategoryValidator()
    {
        RuleForEach(x => x.Localizations)
            .SetValidator(new ProductCategoryLocalizationValidator());
    }
}
