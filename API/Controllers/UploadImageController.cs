using Domain.DTOs.UploadImage;
using Infrastructure.ExternalServices.Cloudinary;
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

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadImageDTO file)
    {
      try
      {
        var profiles = await _cloudinaryService.UploadImage(file);
        return Ok(profiles);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("delete/{publicId}")]
    public async Task<IActionResult> Delete(string publicId)
    {
      try
      {
        var profiles = await _cloudinaryService.DeleteImage(publicId);
        return Ok(profiles);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
        return StatusCode(500, ex.Message);
      }
    }
  }
}


