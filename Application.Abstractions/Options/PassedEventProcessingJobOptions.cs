namespace Application.Abstractions.Options;

public sealed record PassedEventProcessingJobOptions
{
    public required int PassedEventProcessingJobDurationInMilliseconds {  get; init; }

    public required int PassedEventProcessingBatchCount {  get; init; }
}
