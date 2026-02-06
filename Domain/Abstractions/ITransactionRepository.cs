using Domain.Entities;
using Domain.Queries;

namespace Domain.Abstractions;

public interface ITransactionRepository
{
    public Task AddRangeAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken);

    public Task<Transaction[]> GetByFilterAsync(TransactionSearchFilter filter, CancellationToken cancellationToken);
}
