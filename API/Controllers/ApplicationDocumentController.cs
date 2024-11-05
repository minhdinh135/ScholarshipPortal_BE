using Application.Interfaces.IServices;
using Domain.DTOs.Application;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/application-documents")]
	public class ApplicationDocumentController : ControllerBase
	{
		private readonly IApplicationDocumentService _applicationDocumentService;
		private readonly ILogger<ApplicationDocumentController> _logger;

		public ApplicationDocumentController(IApplicationDocumentService applicationDocumentService,
                ILogger<ApplicationDocumentController> logger)
		{
            _applicationDocumentService = applicationDocumentService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var profiles = await _applicationDocumentService.GetAll();
				return Ok(profiles);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

        [HttpGet("paginated")]
        public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = default, [FromQuery] string sortOrder = default)
        {
            var categories = await _applicationDocumentService.GetAll(pageIndex, pageSize, sortBy, sortOrder);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicantions successfully", categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var profile = await _applicationDocumentService.Get(id);
                if (profile == null) return NotFound("Application not found.");
                return Ok(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get applicant profile by id {id}: {ex.Message}");
                return StatusCode(500, "Error retrieving data from the database.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddApplicationDocumentDto dto)
        {
            try
            {
                var addedProfile = await _applicationDocumentService.Add(dto);
                return Ok(addedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add applicant profile: {ex.Message}");
                return StatusCode(500, "Error adding data to the database.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateApplicationDocumentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedProfile = await _applicationDocumentService.Update(id, dto);
                return Ok(updatedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update applicant profile: {ex.Message}");
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedProfile = await _applicationDocumentService.Delete(id);
                if (deletedProfile == null) return NotFound("Application not found.");

                return Ok(deletedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete applicant profile: {ex.Message}");
                return StatusCode(500, "Error deleting data from the database.");
            }
        }

	}
}
