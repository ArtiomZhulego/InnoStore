using Application.Abstractions.ProductCategoryAggregate;
using FluentValidation;
using Presentation.Constants;

namespace Presentation.Validation;

public class ProductCategoryLocalizationValidator : AbstractValidator<ProductCategoryLocalizationModel>
{
    public ProductCategoryLocalizationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product group name is required.")
            .MaximumLength(ValidationConstants.ProductCategoryNameMaxLength).WithMessage("Product group name must not exceed 200 characters.");
        RuleFor(x => x.LanguageISOCode)
            .NotEmpty().WithMessage("Language ISO code is required.")
            .Length(ValidationConstants.LocalizationIsoMaxLenght).WithMessage("Language ISO code must be exactly 2 characters.");
    }
}
