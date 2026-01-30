namespace Application.Abstractions.DTOs.Entities.PassedEvent.V1;

public sealed record PassedEventDTOEventContent
{
    public required Guid Id { get; init; }

    public required string Title { get; init; }

    public required DateTime StartDate { get; init; }

    public required PassedEventDTOEventContentEventType Type { get; init; }

    public required PassedEventDTOEventContentSpeaker[] Speakers { get; init; }

    public required PassedEventDTOEventContentAssistent[] Assistents { get; init; }
}
