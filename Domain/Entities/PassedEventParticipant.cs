namespace Domain.Entities;

public sealed class PassedEventParticipant
{
    public required int HrmId { get; init; }

    public required string Email { get; init; }

    public required PassedEventParticipantRole Role { get; init; }

    public required Guid PassedEventId { get; init; }
}
