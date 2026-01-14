namespace Application.Abstractions.DTOs.Entities;

public class PassedEventParticipantDTO
{
    public required int HrmId { get; init; }

    public required string Email { get; init; }

    public required PassedEventParticipantDTORole Role { get; init; }
}
