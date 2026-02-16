using Application.Abstractions.OrderAggregate.Models;
using FluentValidation;

namespace Presentation.Validation.Orders;

internal sealed class CreateOrderModelValidator : AbstractValidator<CreateOrderModel>
{
    public CreateOrderModelValidator()
    {
        this.RuleFor(model => model.UserId)
            .NotEmpty();

        this.RuleFor(model => model.ProductSizeId)
            .NotEmpty();
    }
}