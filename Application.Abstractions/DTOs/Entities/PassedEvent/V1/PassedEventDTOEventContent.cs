namespace Application.Abstractions.DTOs.Entities.PassedEvent.V1;

public record class PassedEventDTOEventContent
{
    public required Guid Id { get; set; }

    public required string Title { get; set; }

    public required DateTime StartDate { get; set; }

    public required PassedEventDTOEventContentEventType Type { get; set; }

    public required PassedEventDTOEventContentSpeaker[] Speakers { get; set; }

    public required PassedEventDTOEventContentAssistent[] Assistents { get; set; }
}
