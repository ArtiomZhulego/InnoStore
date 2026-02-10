using Application.Abstractions.OrderAggregate.Models;
using FluentValidation;

namespace Presentation.Validation.Orders;

internal sealed class CancelOrderModelValidator : AbstractValidator<CancelOrderModel>
{
    public CancelOrderModelValidator()
    {
        this.RuleFor(model => model.OrderId)
            .NotEmpty();

        this.RuleFor(model => model.RevertedByUserId)
            .NotEmpty();
    }
}