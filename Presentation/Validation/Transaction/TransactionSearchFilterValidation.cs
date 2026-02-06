using Application.Abstractions.Constants;
using Application.Abstractions.TransactionAggregate.Search;
using FluentValidation;

namespace Presentation.Validation.Transaction;

public class TransactionSearchFilterValidation : AbstractValidator<TransactionSearchFilter>
{
    public TransactionSearchFilterValidation()
    {
        this.RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage($"You need to define {nameof(TransactionSearchFilter.UserId)}");

        this.RuleFor(x => x.PageNumber)
            .NotEmpty()
            .GreaterThanOrEqualTo(TransactionConstants.DefaultSearchPageNumber)
            .When(x => x.PageSize is not null)
            .WithMessage($"You need to define {nameof(TransactionSearchFilter.PageNumber)} >= {TransactionConstants.DefaultSearchPageNumber} when {nameof(TransactionSearchFilter.PageSize)} is defined.");

        this.RuleFor(x => x.PageSize)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .When(x => x.PageNumber is not null)
            .WithMessage($"You need to define {nameof(TransactionSearchFilter.PageSize)} >= 0 when {nameof(TransactionSearchFilter.PageNumber)} is defined.");
    }
}
