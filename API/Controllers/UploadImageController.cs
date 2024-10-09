using Domain.DTOs.UploadImage;
using Infrastructure.ExternalServices.Cloudnary;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UploadImageController : ControllerBase
	{
		private readonly CloudinaryService _cloudinaryService;
		private readonly ILogger<UploadImageController> _logger;

		public UploadImageController(CloudinaryService cloudinaryService, ILogger<UploadImageController> logger)
		{
      _cloudinaryService = cloudinaryService;
			_logger = logger;
		}

    [HttpPost("upload-image")]
    public async Task<IActionResult> Upload([FromForm] UploadImageDTO file)
    {
      try
      {
        var profiles = await _cloudinaryService.UploadImage(file);
        return Ok(new {url = profiles});
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to upload: {ex.Message}");
        return StatusCode(500, new{message=ex.Message});
      }
    }

    [HttpPost("upload-file")]
    public async Task<IActionResult> UploadFile([FromForm] UploadImageDTO file)
    {
      try
      {
        var profiles = await _cloudinaryService.UploadRaw(file);
        return Ok(new {url = profiles});
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to upload: {ex.Message}");
        return StatusCode(500, new{message=ex.Message});
      }
    }

    [HttpDelete("delete-image/{publicId}")]
    public async Task<IActionResult> DeleteImage(string publicId)
    {
      try
      {
        var profiles = await _cloudinaryService.DeleteImage(publicId);
        return Ok(new {message = profiles });
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
        return StatusCode(500, new{message=ex.Message});
      }
    }

    [HttpDelete("delete-file/{publicId}")]
    public async Task<IActionResult> DeleteFile(string publicId)
    {
      try
      {
        var profiles = await _cloudinaryService.DeleteFile(publicId);
        return Ok(new {message = profiles });
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
        return StatusCode(500, new{message=ex.Message});
      }
    }
  }
}


