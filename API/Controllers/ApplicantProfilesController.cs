using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ApplicantProfilesController : ControllerBase
	{
		private readonly IApplicantProfileService _applicantProfileService;
		private readonly ILogger<ApplicantProfilesController> _logger;

		public ApplicantProfilesController(IApplicantProfileService applicantProfileService, ILogger<ApplicantProfilesController> logger)
		{
			_applicantProfileService = applicantProfileService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllApplicantProfiles()
		{
			try
			{
				var profiles = await _applicantProfileService.GetAll();
				return Ok(profiles);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetApplicantProfileById(int id)
		{
			try
			{
				var profile = await _applicantProfileService.Get(id);
				if (profile == null) return NotFound("Applicant profile not found.");
				return Ok(profile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get applicant profile by id {id}: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddApplicantProfile([FromBody] AddApplicantProfileDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var addedProfile = await _applicantProfileService.Add(dto);
				return Ok(addedProfile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add applicant profile: {ex.Message}");
				return StatusCode(500, "Error adding data to the database.");
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateApplicantProfile( [FromBody] UpdateApplicantProfileDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var updatedProfile = await _applicantProfileService.Update(dto);
				return Ok(updatedProfile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update applicant profile: {ex.Message}");
				return BadRequest(new { Message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteApplicantProfile(int id)
		{
			try
			{
				var deletedProfile = await _applicantProfileService.Delete(id);
				if (deletedProfile == null) return NotFound("Applicant profile not found.");

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
