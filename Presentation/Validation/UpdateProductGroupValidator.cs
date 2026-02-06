using Application.Abstractions.ProductGroupAggregate;
using FluentValidation;

namespace Presentation.Validation;

public class UpdateProductGroupValidator : AbstractValidator<UpdateProductGroupModel>
{
    public UpdateProductGroupValidator()
    {
        RuleForEach(x => x.Localizations)
            .SetValidator(new ProductGroupLocalizationValidator());
    }
}
