using Application.Interfaces.IServices;
using Domain.DTOs.Major;
using Infrastructure.ExternalServices.Email;
using Infrastructure.ExternalServices.Gemini;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IEmailService _emailService;
    private readonly GeminiService _geminiService;
    private readonly IElasticService<MajorDto> _elasticService;

    public TestController(ILogger<TestController> logger, ICloudinaryService cloudinaryService,
        IEmailService emailService, GeminiService geminiService, IElasticService<MajorDto> elasticService)
    {
        _logger = logger;
        _cloudinaryService = cloudinaryService;
        _emailService = emailService;
        _geminiService = geminiService;
        _elasticService = elasticService;
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

    [HttpPost("prompt")]
    public async Task<IActionResult> GetGeminiResponse([FromBody] ChatRequest request)
    {
        var response = await _geminiService.GetResponseFromGemini(request.Prompt);
        return Ok(new { Response = response });
    }

    [HttpPost("/elastic/index")]
    public async Task<IActionResult> CreateIndex(string indexName)
    {
        await _elasticService.CreateIndex(indexName);
        return Ok($"Index {indexName} created or already exists");
    }

    [HttpPost("/elastic/majors")]
    public async Task<IActionResult> AddMajor([FromBody] MajorDto major)
    {
        var result = await _elasticService.AddOrUpdate(major);

        return result
            ? Ok("Major added or updated successfully")
            : StatusCode(StatusCodes.Status500InternalServerError, "Error adding or updating major");
    }

    [HttpGet("/elastic/majors")]
    public async Task<IActionResult> GetAllMajors()
    {
        var countries = await _elasticService.GetAll();

        return countries != null
            ? Ok(countries)
            : StatusCode(StatusCodes.Status500InternalServerError, "Error getting majors");
    }

    [HttpGet("/elastic/majors/{key}")]
    public async Task<IActionResult> GetMajor(string key)
    {
        var country = await _elasticService.Get(key);

        return country != null ? Ok(country) : NotFound("Major not found");
    }

    [HttpDelete("/elastic/majors/{key}")]
    public async Task<IActionResult> DeleteMajors(string key)
    {
        var result = await _elasticService.Remove(key);

        return result
            ? Ok("Major deleted successfully")
            : StatusCode(StatusCodes.Status500InternalServerError, "Error deleting major");
    }

    [HttpDelete("/elastic/majors")]
    public async Task<IActionResult> DeleteAllMajors()
    {
        var result = await _elasticService.RemoveAll();

        return result > 0
            ? Ok("Delete all majors successfully")
            : StatusCode(StatusCodes.Status500InternalServerError, "Error deleting majors");
    }
}