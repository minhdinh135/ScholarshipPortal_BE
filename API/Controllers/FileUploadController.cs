using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/file-upload")]
public class FileUploadController : ControllerBase
{
    private readonly ILogger<FileUploadController> _logger;
    private readonly ICloudinaryService _cloudinaryService;

    public FileUploadController(ILogger<FileUploadController> logger, ICloudinaryService cloudinaryService)
    {
        _logger = logger;
        _cloudinaryService = cloudinaryService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFiles(IFormFileCollection files)
    {
        try
        {
            var imageUrls = await _cloudinaryService.UploadFiles(files);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Upload files successfully", imageUrls));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}