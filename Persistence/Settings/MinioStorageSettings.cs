namespace Persistence.Settings;

public sealed class MinioStorageSettings
{
    public const string ConfigurationSection = "Minio";

    public required string SpaceUrl { get; init; }
    public required string AccessKey { get; init; }
    public required string SecretKey { get; init; }
    public required string BucketName { get; init; }
    public required string EnvironmentSubFolder { get; init; }
}
