using Application.Abstractions.ProductSizeAggregate;
using FluentValidation;
using Presentation.Constants;

namespace Presentation.Validation;

public class ProductSizeLocalizationValidator : AbstractValidator<ProductSizeLocalizationModel>
{
    public ProductSizeLocalizationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product size name is required.")
            .MaximumLength(ValidationConstants.ProductSizeNameMaxLength).WithMessage("Product size name must not exceed 200 characters.");
        RuleFor(x => x.LanguageISOCode)
            .NotEmpty().WithMessage("Language ISO code is required.")
            .Length(ValidationConstants.LocalizationIsoMaxLenght).WithMessage("Language ISO code must be exactly 2 characters.");
    }
}
