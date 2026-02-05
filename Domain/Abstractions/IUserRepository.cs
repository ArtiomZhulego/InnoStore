using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository
{
    Task<IEnumerable<int>> GetUserHrmIdsAsync(CancellationToken cancellationToken);
    
    Task AddRangeAsync(IEnumerable<User> users, CancellationToken cancellationToken);

    Task<User[]> GetAllByHrmIdsAsync(int[] hrmIds, CancellationToken cancellationToken);
    Task<bool> AnyAsync(Guid userId, CancellationToken cancellationToken);
}