namespace InnoStore.CampusInegration;

public sealed class KafkaCampusConfiguration
{
    internal const string KafkaSection = "KAFKA";

    public required string Server { get; set; }

    public required string GroupId { get; set; }

    public required string EventsTopic { get; set; }

    public required string EventsDlqTopic { get; set; }
}
