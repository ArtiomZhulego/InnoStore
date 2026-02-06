using Application.Abstractions.ProductImageAggregate;
using FluentValidation;

namespace Presentation.Validation;

public class CreateProductImageValidator : AbstractValidator<CreateProductImageModel>
{
    public CreateProductImageValidator()
    {
        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Image URL must be a valid URL.");
    }
}
