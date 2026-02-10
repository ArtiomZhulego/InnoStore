namespace Domain.Exceptions;

public class InsufficientFundsException : Exception
{
    public Guid UserId { get; }
    public decimal RequiredAmount { get; }
    public decimal AvailableAmount { get; }

    public InsufficientFundsException(Guid userId, decimal requiredAmount, decimal availableAmount)
        : base($"User {userId} has insufficient funds. Required: {requiredAmount}, Available: {availableAmount}.")
    {
        UserId = userId;
        RequiredAmount = requiredAmount;
        AvailableAmount = availableAmount;
    }
}