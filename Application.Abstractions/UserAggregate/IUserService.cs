namespace Application.Abstractions.UserAggregate;

public interface IUserService
{
    Task<UserBalanceDTO> GetUserBalanceAsync(Guid userId, CancellationToken cancellationToken = default);
}
