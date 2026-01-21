using Domain.Abstractions;

namespace Persistence.Transactions;

public sealed class TransactionManager(InnoStoreContext context) : ITransactionManager
{
    public async Task BeginAsync(CancellationToken cancellationToken = default)
    {
        await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        var transaction = context.Database.CurrentTransaction;

        if (transaction is null)
            return;

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        var transaction = context.Database.CurrentTransaction;

        if (transaction is null)
            return;

        await transaction.RollbackAsync(cancellationToken);
    }
}
