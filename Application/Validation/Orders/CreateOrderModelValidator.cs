using Application.Abstractions.OrderAggregate.Models;
using Domain.Abstractions;
using FluentValidation;

namespace Application.Validation.Orders;

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