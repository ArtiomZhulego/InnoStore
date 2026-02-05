using Application.Abstractions.UserAggregate;
using Domain.Abstractions;

namespace Application.Services;

internal sealed class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<decimal> GetCurrentScoresAmountAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var scoresAmount = await userRepository.GetCurrentScoresAmountAsync(userId, cancellationToken);
        return scoresAmount;
    }
}
