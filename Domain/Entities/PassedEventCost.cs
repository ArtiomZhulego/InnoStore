namespace Domain.Entities;

public sealed class PassedEventCost
{
    public required PassedEventType EventType { get; set; }

    public required decimal Amount { get; set; }
}
