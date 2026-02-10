using Domain.Abstractions;

namespace Persistence.DatabaseManagers;

public sealed class DatabaseTransactionManager(InnoStoreContext context) : IDatabaseTransactionManager
{
    public async Task BeginAsync(CancellationToken cancellationToken = default)
    {
        if (context.Database.CurrentTransaction is null) return;
        await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (context.Database.CurrentTransaction is null) return;
        await context.Database.CurrentTransaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (context.Database.CurrentTransaction is null) return;
        await context.Database.CurrentTransaction.RollbackAsync(cancellationToken);
    }
}
