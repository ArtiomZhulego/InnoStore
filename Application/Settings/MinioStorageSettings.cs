namespace Application.Settings;

public class MinioStorageSettings
{
    public const string ConfigurationSection = "Minio";

    public required string SpaceUrl { get; set; }
    public required string AccessKey { get; set; }
    public required string SecretKey { get; set; }
    public required string BucketName { get; set; }
    public required string EnvironmentSubFolder { get; set; }
}
