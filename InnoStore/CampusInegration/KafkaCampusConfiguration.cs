namespace InnoStore.CampusInegration;

public sealed class KafkaCampusConfiguration
{
    public required string KafkaServer { get; set; }

    public required string KafkaGroupId { get; set; }

    public required string KafkaEventsTopic { get; set; }

    public required string KafkaEventsDlqTopic { get; set; }
}
