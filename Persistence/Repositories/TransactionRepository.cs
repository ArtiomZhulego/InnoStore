using Domain.Abstractions;
using Domain.Entities;

namespace Persistence.Repositories;

internal sealed class TransactionRepository(InnoStoreContext context) : ITransactionRepository
{
    public async Task AddRangeAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken)
    {
        await context.Transactions.AddRangeAsync(transactions, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
