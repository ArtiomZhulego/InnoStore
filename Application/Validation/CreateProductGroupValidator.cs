using Application.Abstractions.ProductGroupAggregate;
using FluentValidation;

namespace Presentation.Validation;

public class CreateProductGroupValidator : AbstractValidator<CreateProductCategoryModel>
{
    public CreateProductGroupValidator()
    {
        RuleForEach(x => x.Localizations)
            .SetValidator(new ProductGroupLocalizationValidator());
    }
}
