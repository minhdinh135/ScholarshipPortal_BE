using Application.Interfaces.IServices;
using Domain.DTOs.Application;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/applications")]
	public class ApplicationController : ControllerBase
	{
		private readonly IApplicationService _applicationService;
		private readonly ILogger<ApplicationController> _logger;

		public ApplicationController(IApplicationService applicationService, ILogger<ApplicationController> logger)
		{
            _applicationService = applicationService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var profiles = await _applicationService.GetAll();
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
            var categories = await _applicationService.GetAll(pageIndex, pageSize, sortBy, sortOrder);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicantions successfully", categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var profile = await _applicationService.Get(id);
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
        public async Task<IActionResult> Add([FromBody] AddApplicationDto dto)
        {
            try
            {
                var addedProfile = await _applicationService.Add(dto);
                return Ok(addedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add applicant profile: {ex.Message}");
                return StatusCode(500, "Error adding data to the database.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateApplicationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedProfile = await _applicationService.Update(id, dto);
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
                var deletedProfile = await _applicationService.Delete(id);
                if (deletedProfile == null) return NotFound("Application not found.");

                return Ok(deletedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete applicant profile: {ex.Message}");
                return StatusCode(500, "Error deleting data from the database.");
            }
        }

        [HttpGet("with-documents-and-account-profile/{id}")]
		public async Task<IActionResult> GetWithDocumentsAndAccount(int id)
		{
			try
			{
				var profile = await _applicationService.GetWithDocumentsAndAccount(id);
				if (profile == null) return NotFound("Application not found.");
				return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicantion successfully", profile));
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get applicant profile by id {id}: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

        [HttpGet("get-by-scholarship/{scholarshipId}")]
		public async Task<IActionResult> GetByScholarship(int scholarshipId)
		{
			try
			{
				var profile = await _applicationService.GetByScholarshipId(scholarshipId);
				if (profile == null) return NotFound("Application not found.");
				return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicantion successfully", profile));
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get applicant profile by id {scholarshipId}: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

		/*[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var profile = await _applicationService.Get(id);
				if (profile == null) return NotFound("Application not found.");
				return Ok(profile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get applicant profile by id {id}: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

		[HttpPost("Add")]
		public async Task<IActionResult> Add([FromBody] AddApplicationDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var addedProfile = await _applicationService.Add(dto);
				return Ok(addedProfile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add applicant profile: {ex.Message}");
				return StatusCode(500, "Error adding data to the database.");
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateApplicationDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var updatedProfile = await _applicationService.Update(id, dto);
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
				var deletedProfile = await _applicationService.Delete(id);
				if (deletedProfile == null) return NotFound("Application not found.");

				return Ok(deletedProfile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete applicant profile: {ex.Message}");
				return StatusCode(500, "Error deleting data from the database.");
			}
		}*/
	}
}
