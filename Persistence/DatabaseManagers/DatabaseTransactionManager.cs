using Domain.Abstractions;

namespace Persistence.DatabaseManagers;

public sealed class DatabaseTransactionManager(InnoStoreContext context) : IDatabaseTransactionManager
{
    private int _transactionDepth = 0;
    
    public async Task<IDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transactionDepth++;

        if (_transactionDepth == 1)
        {
            await context.Database.BeginTransactionAsync(cancellationToken);
        }

        return new DatabaseTransactionWrapper(this);
    }

    private async Task CommitInternalAsync(CancellationToken cancellationToken)
    {
        if (_transactionDepth > 0) _transactionDepth--;

        if (_transactionDepth == 0 && context.Database.CurrentTransaction is not null)
        {
            await context.Database.CurrentTransaction.CommitAsync(cancellationToken);
        }
    }

    private async Task RollbackInternalAsync(CancellationToken cancellationToken)
    {
        if (context.Database.CurrentTransaction is not null)
        {
            await context.Database.CurrentTransaction.RollbackAsync(cancellationToken);
        }
        _transactionDepth = 0;
    }
    
    private sealed class DatabaseTransactionWrapper(DatabaseTransactionManager manager) : IDatabaseTransaction
    {
        private bool _isCommitted;
        private bool _isDisposed;

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(IDatabaseTransaction));
            
            await manager.CommitInternalAsync(cancellationToken);
            _isCommitted = true;
        }

        public async ValueTask DisposeAsync()
        {
            if (_isDisposed) return;

            if (!_isCommitted)
            {
                await manager.RollbackInternalAsync(CancellationToken.None);
            }

            _isDisposed = true;
        }
    }
}
