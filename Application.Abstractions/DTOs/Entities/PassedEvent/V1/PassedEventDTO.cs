namespace Application.Abstractions.DTOs.Entities.PassedEvent.V1;

public record PassedEventDTO
{
    public required PassedEventDTOEventContent EventContent { get; set; }
}
