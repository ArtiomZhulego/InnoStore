namespace Application.Abstractions.DTOs.Entities;

public record PassedEventDTO
{
    public required Guid EventId { get; init; }

    public required string Name { get; init; }

    public required PassedEventDTOType EventType { get; init; }

    public required PassedEventParticipantDTO[] Participants { get; init; }
}
