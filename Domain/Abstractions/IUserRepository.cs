using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository
{
    Task<IEnumerable<int>> GetUserIdsAsync(CancellationToken cancellationToken);
    
    Task AddRangeAsync(IEnumerable<User> users, CancellationToken cancellationToken);
}