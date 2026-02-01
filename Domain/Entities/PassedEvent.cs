using Domain.ValueModels;

namespace Domain.Entities;

public sealed class PassedEvent
{
    public Guid Id { get; init; }

    public required Guid EventId { get; init; }

    public required string Name { get; init; }

    public required PassedEventType EventType { get; init; }

    public bool IsProcessed { get; set; }
    
    public required ICollection<PassedEventParticipant> Participants { get; init; }
}
