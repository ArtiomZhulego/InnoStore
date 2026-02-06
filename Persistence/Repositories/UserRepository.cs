using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class UserRepository(InnoStoreContext context) : IUserRepository
{
    public async Task<IEnumerable<int>> GetUserHrmIdsAsync(CancellationToken cancellationToken)
    {
        return await context.Users.Select(x => x.HrmId).ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<User> users, CancellationToken cancellationToken)
    { 
        await context.Users.AddRangeAsync(users, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<User[]> GetAllByHrmIdsAsync(int[] hrmIds, CancellationToken cancellationToken)
    {
        var users = await context.Users
            .AsNoTracking()
            .Where(x => hrmIds.Contains(x.HrmId))
            .ToArrayAsync(cancellationToken);

        return users;
    }

    public async Task<decimal> GetCurrentScoresAmountAsync(Guid id, CancellationToken cancellationToken)
    {
        var scoresAmount = await context.Transactions
            .Where(x => x.UserId == id)
            .SumAsync(x => x.Amount);

        return scoresAmount;
    }

    public async Task<bool> AnyAsync(Guid userId, CancellationToken cancellationToken)
    {
        var isExistedUser = await context.Users.AnyAsync(x => x.Id == userId, cancellationToken);
        return isExistedUser;
    }
}