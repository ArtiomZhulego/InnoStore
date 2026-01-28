using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.FileAggregate;

public sealed class UploadFileModel
{
    public required IFormFile File { get; set; }
}
