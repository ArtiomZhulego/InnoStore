namespace Domain.Abstractions;

public interface IDatabaseTransactionManager
{
    ValueTask BeginAsync(CancellationToken cancellationToken = default);

    ValueTask CommitAsync(CancellationToken cancellationToken = default);

    ValueTask RollbackAsync(CancellationToken cancellationToken = default);
}
