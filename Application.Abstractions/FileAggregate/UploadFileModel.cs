namespace Application.Abstractions.FileAggregate;

public sealed class UploadFileModel
{
    public required string FileName { get; set; }
    public required string FileExtension { get; set; }
    public required Stream Stream { get; set; }
}
