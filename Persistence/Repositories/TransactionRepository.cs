using Domain.Abstractions;
using Domain.Entities;
using Domain.Queries;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class TransactionRepository(InnoStoreContext context) : ITransactionRepository
{
    public async Task AddRangeAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken)
    {
        await context.Transactions.AddRangeAsync(transactions, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Transaction[]> GetByFilterAsync(TransactionSearchFilter filter, CancellationToken cancellationToken)
    {
        var query = context.Transactions.AsNoTracking()
            .Where(x => x.UserId == filter.UserId);

        if (filter.PageNumber is not null &&  filter.PageSize is not null)
        {
            query = query.Skip(filter.PageSize.Value * (filter.PageNumber.Value - 1))
                .Take(filter.PageNumber.Value);
        }

        var result = await query.ToArrayAsync(cancellationToken);
        return result;
    }
}
