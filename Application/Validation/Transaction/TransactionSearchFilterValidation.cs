using Application.Abstractions.TransactionAggregate.Search;
using FluentValidation;

namespace Application.Validation.Transaction;

public class TransactionSearchFilterValidation : AbstractValidator<TransactionSearchFilter>
{
    public TransactionSearchFilterValidation()
    {
        this.RuleFor(x => x.PageNumber)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .When(x => x.PageSize is not null)
            .WithMessage(x => $"You need to define {nameof(x.PageNumber)} >= 0 when {nameof(x.PageSize)} is defined.");

        this.RuleFor(x => x.PageSize)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .When(x => x.PageNumber is not null)
            .WithMessage(x => $"You need to define {nameof(x.PageSize)} >= 0 when {nameof(x.PageNumber)} is defined.");
    }
}
