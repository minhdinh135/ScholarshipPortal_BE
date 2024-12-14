using Application.Interfaces.IServices;
using Infrastructure.ExternalServices.Email;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IEmailService _emailService;

    public TestController(ILogger<TestController> logger, ICloudinaryService cloudinaryService,
        IEmailService emailService)
    {
        _logger = logger;
        _cloudinaryService = cloudinaryService;
        _emailService = emailService;
    }

    [HttpPost("upload-image")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        try
        {
            var profiles = await _cloudinaryService.UploadImage(file);
            return Ok(new { url = profiles });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to upload: {ex.Message}");
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("upload-file")]
    public async Task<IActionResult> UploadFile(List<IFormFile> files)
    {
		try
		{
			var uploadResults = new List<string>();

			foreach (var file in files)
			{
				var url = await _cloudinaryService.UploadRaw(file);
				uploadResults.Add(url);
			}

			return Ok(new { urls = uploadResults });
		}
		catch (Exception ex)
		{
			_logger.LogError($"Failed to upload files: {ex.Message}");
			return StatusCode(500, new { message = ex.Message });
		}
	}

    [HttpDelete("delete-image/{publicId}")]
    public async Task<IActionResult> DeleteImage(string publicId)
    {
        try
        {
            var profiles = await _cloudinaryService.DeleteImage(publicId);
            return Ok(new { message = profiles });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete("delete-file/{publicId}")]
    public async Task<IActionResult> DeleteFile(string publicId)
    {
        try
        {
            var profiles = await _cloudinaryService.DeleteFile(publicId);
            return Ok(new { message = profiles });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("SendMail")]
    public async Task<IActionResult> SendMail(
        [FromForm] string receiver,
        [FromForm] string subject,
        [FromForm] string message,
        [FromForm] EmailFile? attachment = null)
    {
        if (string.IsNullOrWhiteSpace(receiver) || string.IsNullOrWhiteSpace(subject) ||
            string.IsNullOrWhiteSpace(message))
        {
            return BadRequest("Recipient, subject, and message are required.");
        }

        try
        {
            await _emailService.SendEmailAsync(receiver, subject, message, attachment.file);
            return Ok("Email sent successfully!");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}