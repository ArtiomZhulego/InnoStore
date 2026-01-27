using Domain.Entities;

namespace Domain.Abstractions;

public interface ITransactionRepository
{
    public Task AddRangeAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken);
}
