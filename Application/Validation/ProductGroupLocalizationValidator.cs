using Application.Abstractions.ProductGroupAggregate;
using FluentValidation;

namespace Application.Validation;

public class ProductGroupLocalizationValidator : AbstractValidator<ProductGroupLocalizationModel>
{
    public ProductGroupLocalizationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product group name is required.")
            .MaximumLength(200).WithMessage("Product group name must not exceed 200 characters.");
        RuleFor(x => x.LanguageISOCode)
            .NotEmpty().WithMessage("Language ISO code is required.")
            .Length(2).WithMessage("Language ISO code must be exactly 2 characters.");
    }
}
