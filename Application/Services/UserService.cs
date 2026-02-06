using Application.Abstractions.UserAggregate;
using Domain.Abstractions;

namespace Application.Services;

internal sealed class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserBalanceDTO> GetUserBalanceAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var scoresAmount = await userRepository.GetCurrentScoresAmountAsync(userId, cancellationToken);
        var userBalanceDTO = new UserBalanceDTO
        {
            Amount = scoresAmount,
        };

        return userBalanceDTO;
    }
}
