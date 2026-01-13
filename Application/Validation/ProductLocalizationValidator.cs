using Application.Abstractions.ProductAggregate;
using FluentValidation;

namespace Application.Validation;

public class ProductLocalizationValidator : AbstractValidator<ProductLocalizationModel>
{
    public ProductLocalizationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(200).WithMessage("Product name must not exceed 200 characters.");
        RuleFor(x => x.LanguageISOCode)
            .NotEmpty().WithMessage("Language ISO code is required.")
            .Length(2).WithMessage("Language ISO code must be exactly 2 characters.");
    }
}
