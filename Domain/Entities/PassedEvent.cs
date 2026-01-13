namespace Domain.Entities;

public sealed class PassedEvent
{
    public required Guid Id { get; init; }

    public required PassedEventType EventType { get; init; }

    public required PassedEventParticipant[] Participants { get; init; }
}
