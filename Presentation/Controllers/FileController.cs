using Application.Abstractions.FileAggregate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.Files.Controller)]
public class FileController(IFileService fileService) : ControllerBase
{
    [HttpPost(PathConstants.Files.Upload, Name = "uploadFile")]
    [ProducesResponseType(typeof(UploadFileResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> UploadFileAsync([FromForm] UploadFileModel request, CancellationToken cancellationToken)
    {
        var result = await fileService.UploadFileAsync(request, cancellationToken);
        return Ok(result);
    }
}
