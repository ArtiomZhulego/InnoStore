namespace Domain.ValueModels;

public enum TransactionType : byte
{
    Unspecified = 0,
    AddForParticipatingInEvent = 1,
    Pay = 2,
    Refund = 3,
}