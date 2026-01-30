using Domain.Entities;

namespace Domain.Abstractions;

public interface ITransactionRepository
{
    public Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
    public Task AddRangeAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken = default);
}
