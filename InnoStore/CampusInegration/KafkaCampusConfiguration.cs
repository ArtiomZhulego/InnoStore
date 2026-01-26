namespace InnoStore.CampusInegration;

public sealed class KafkaCampusConfiguration
{
    public required string Server { get; set; }

    public required string GroupId { get; set; }

    public required string PassedEventTopic { get; set; }

    public required string DeadLetterQueueTopic { get; set; }
}
