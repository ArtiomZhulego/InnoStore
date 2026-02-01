using Domain.Abstractions;

namespace Persistence.DatabaseManagers;

public sealed class DatabaseTransactionManager(InnoStoreContext context) : IDatabaseTransactionManager
{
    public async ValueTask BeginAsync(CancellationToken cancellationToken = default)
    {
        if (context.Database.CurrentTransaction is null) return;
        await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async ValueTask CommitAsync(CancellationToken cancellationToken = default)
    {
        if (context.Database.CurrentTransaction is null) return;
        await context.Database.CurrentTransaction.CommitAsync(cancellationToken);
    }

    public async ValueTask RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (context.Database.CurrentTransaction is null) return;
        await context.Database.CurrentTransaction.RollbackAsync(cancellationToken);
    }
}
