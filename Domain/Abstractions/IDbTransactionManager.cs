namespace Domain.Abstractions;

public interface IDatabaseTransactionManager
{
    void BeginSerializable();

    Task BeginAsync(CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);
}
