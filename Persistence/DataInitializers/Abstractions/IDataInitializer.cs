namespace Persistence.DataInitializers.Abstractions;

public interface IDataInitializer
{
    public Task InitializeAsync(CancellationToken cancellationToken = default);
}
