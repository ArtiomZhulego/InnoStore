namespace Domain.Entities;

public sealed class PassedEvent
{
    public Guid Id { get; set; }

    public required Guid EventId { get; init; }

    public required string Name { get; init; }

    public required PassedEventType EventType { get; init; }

    public required ICollection<PassedEventParticipant> Participants { get; init; }

    public bool IsProcessed { get; set; }
}
