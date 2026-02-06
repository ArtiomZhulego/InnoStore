using Application.Abstractions.TransactionAggregate;
using Application.Abstractions.TransactionAggregate.Search;
using Application.Extensions;
using Application.Mappers;
using Domain.Abstractions;

namespace Application.Services;

internal sealed class TransactionService(
    ITransactionRepository transactionRepository
) : ITransactionService
{
    public async Task<TransactionDTO[]> GetTransactionsAsync(TransactionSearchFilter filter, CancellationToken cancellationToken)
    {
        var domainFilter = filter.ToDomainQuery();
        var transactions = await transactionRepository.GetByFilterAsync(domainFilter, cancellationToken);
        var transactionDTOs = transactions.Select(x => x.ToDTO()).ToArray();
        return transactionDTOs;
    }
}
