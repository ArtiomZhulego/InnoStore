using Domain.Abstractions;

namespace Persistence.DatabaseManagers;

public sealed class DatabaseTransactionManager(InnoStoreContext context) : IDatabaseTransactionManager
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
