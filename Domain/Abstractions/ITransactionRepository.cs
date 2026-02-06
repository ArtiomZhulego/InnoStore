using Domain.Entities;
using Domain.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Abstractions;

public interface ITransactionRepository
{
    public Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);

    public Task AddRangeAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken);

    public Task<Transaction[]> GetByFilterAsync(TransactionSearchFilter filter, CancellationToken cancellationToken);
}
