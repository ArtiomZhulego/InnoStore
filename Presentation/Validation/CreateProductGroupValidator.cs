using Application.Abstractions.ProductGroupAggregate;
using FluentValidation;

namespace Presentation.Validation;

public class CreateProductGroupValidator : AbstractValidator<CreateProductGroupModel>
{
    public CreateProductGroupValidator()
    {
        RuleForEach(x => x.Localizations)
            .SetValidator(new ProductGroupLocalizationValidator());
    }
}
