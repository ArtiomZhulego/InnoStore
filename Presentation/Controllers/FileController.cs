using Application.Abstractions.FileAggregate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.Files.Controller)]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost(PathConstants.Files.Upload)]
    [ProducesResponseType(typeof(UploadFileResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> UploadFileAsync([FromForm] UploadFileModel request, CancellationToken cancellationToken)
    {
        var result = await _fileService.UploadFileAsync(request, cancellationToken);
        return Ok(result);
    }
}
