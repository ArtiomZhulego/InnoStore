namespace Domain.Entities;

public sealed class PassedEventParticipant
{
    public required string Email { get; init; }

    public required PassedEventParticipantRole Role { get; init; }
}
