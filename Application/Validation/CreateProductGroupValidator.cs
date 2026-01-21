using Application.Abstractions.ProductGroupAggregate;
using FluentValidation;

namespace Application.Validation;

public class CreateProductGroupValidator : AbstractValidator<CreateProductGroupModel>
{
    public CreateProductGroupValidator()
    {
        RuleForEach(x => x.Localizations)
            .SetValidator(new ProductGroupLocalizationValidator());
    }
}
