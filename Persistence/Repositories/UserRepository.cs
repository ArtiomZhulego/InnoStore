using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public sealed class UserRepository(InnoStoreContext context) : IUserRepository
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
            .AsQueryable()
            .AsNoTracking()
            .Where(x => hrmIds.Contains(x.HrmId))
            .ToArrayAsync(cancellationToken);

        return users;
    }
}