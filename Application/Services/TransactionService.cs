using Application.Abstractions.TransactionAggregate;
using Application.Contsants;
using Application.Extensions;
using Application.Mappers;
using Domain.Abstractions;
using FluentValidation;

namespace Application.Services;

internal sealed class TransactionService(
    IValidator<TransactionSearchFilter> validator,
    ITransactionRepository transactionRepository
) : ITransactionService
{
    public async Task<TransactionDTO[]> GetTransactionsAsync(TransactionSearchFilter filter, CancellationToken cancellationToken)
    {
        await validator.EnsureValidAsync(filter, cancellationToken);
        var domainFilter = filter.ToDomainQuery();

        if (domainFilter.PageSize is null && domainFilter.PageNumber is null)
        {
            domainFilter.PageSize = TransactionConstants.DefaultSearchPageSize;
            domainFilter.PageNumber = TransactionConstants.DefaultSearchPageNumber;
        }

        var transactions = await transactionRepository.GetByFilterAsync(domainFilter, cancellationToken);
        var transactionDTOs = transactions.Select(x => x.ToDTO()).ToArray();
        return transactionDTOs;
    }
}
