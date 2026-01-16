namespace Persistence.Interceptors;

using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

public abstract class BaseChangedInterceptor :
    DbTransactionInterceptor,
    ISaveChangesInterceptor
{
    private bool _isExplicitTransaction;

    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DefineTransactionType(eventData.Context!.Database.CurrentTransaction);
        OnSavingChangesAction(eventData.Context!);
        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DefineTransactionType(eventData.Context!.Database.CurrentTransaction);
        OnSavingChangesAction(eventData.Context!);
        return ValueTask.FromResult(result);
    }

    public void SaveChangesFailed(DbContextErrorEventData eventData) => ResetState();

    public Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        ResetState();
        return Task.CompletedTask;
    }

    public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (!_isExplicitTransaction)
        {
            AfterSaveChangesAction(eventData.Context!, CancellationToken.None).GetAwaiter().GetResult();
        }

        return result;
    }

    public async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (!_isExplicitTransaction)
        {
            await AfterSaveChangesAction(eventData.Context!, cancellationToken);
        }

        return result;
    }

    public override Task TransactionFailedAsync(
        DbTransaction transaction,
        TransactionErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        ResetState();
        return Task.CompletedTask;
    }

    public override async Task TransactionCommittedAsync(
        DbTransaction transaction,
        TransactionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (_isExplicitTransaction)
        {
            await AfterSaveChangesAction(eventData.Context!, cancellationToken);
        }
    }

    protected virtual void ResetState()
    {
        _isExplicitTransaction = false;
    }

    protected abstract void OnSavingChangesAction(DbContext context);

    protected virtual Task AfterSaveChangesAction(DbContext context, CancellationToken cancellationToken) => Task.CompletedTask;

    private void DefineTransactionType(IDbContextTransaction? transaction) =>
        _isExplicitTransaction = transaction != null;
}
