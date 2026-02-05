namespace Application.Abstractions.UserAggregate;

public interface IUserService
{
    Task<decimal> GetCurrentScoresAmountAsync(Guid userId, CancellationToken cancellationToken = default);
}
