using Application.Abstractions.ProductAggregate;
using FluentValidation;
using Presentation.Constants;

namespace Presentation.Validation;

public class ProductLocalizationValidator : AbstractValidator<ProductLocalizationModel>
{
    public ProductLocalizationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(ValidationConstants.ProductNameMaxLength).WithMessage("Product name must not exceed 200 characters.");
        RuleFor(x => x.LanguageISOCode)
            .NotEmpty().WithMessage("Language ISO code is required.")
            .Length(ValidationConstants.LocalizationIsoMaxLenght).WithMessage("Language ISO code must be exactly 2 characters.");
    }
}
