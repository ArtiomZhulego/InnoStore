namespace Domain.Abstractions;

public interface IDatabaseTransactionManager
{
    Task<IDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}

public interface IDatabaseTransaction : IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}