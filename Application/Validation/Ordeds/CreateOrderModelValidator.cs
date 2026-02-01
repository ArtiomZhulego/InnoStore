using Application.Abstractions.OrderAggregate.Models;
using FluentValidation;

namespace Application.Validation.Ordeds;

public sealed class CreateOrderModelValidator : AbstractValidator<CreateOrderModel>
{
    public CreateOrderModelValidator()
    {
        this.RuleFor(item => item.UserId)
            .NotEmpty();

        this.RuleFor(item => item.ProductSizeId)
            .NotEmpty();
    }
}