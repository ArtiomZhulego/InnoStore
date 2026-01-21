namespace Domain.Entities;

public sealed class PassedEventParticipant
{
    public int Id { get; set; }

    public required int HrmId { get; init; }

    public required PassedEventParticipantRole Role { get; init; }

    public required Guid PassedEventId { get; init; }
}
