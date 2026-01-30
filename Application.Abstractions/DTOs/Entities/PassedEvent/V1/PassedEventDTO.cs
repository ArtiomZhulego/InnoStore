namespace Application.Abstractions.DTOs.Entities.PassedEvent.V1;

public sealed record PassedEventDTO
{
    public required PassedEventDTOEventContent EventContent { get; init; }
}
