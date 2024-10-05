using Application.Interfaces.IRepositories;
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
		private readonly IGenericRepository<ApplicantProfile> _applicantProfileRepo;
		private readonly ILogger<ApplicantProfilesController> _logger;

		public ApplicantProfilesController(IGenericRepository<ApplicantProfile> applicantProfileRepo, ILogger<ApplicantProfilesController> logger)
		{
			_applicantProfileRepo = applicantProfileRepo;
			_logger = logger;
		}

		// Get all applicant profiles
		[HttpGet]
		public async Task<IActionResult> GetAllApplicantProfiles()
		{
			try
			{
				var profiles = await _applicantProfileRepo.GetAll(x => x.Include(p => p.Applicant).Include(p => p.Achievements));
				return Ok(profiles);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		// Get applicant profile by ID
		[HttpGet("{id}")]
		public async Task<IActionResult> GetApplicantProfileById(int id)
		{
			try
			{
				var profile = await _applicantProfileRepo.GetById(id);
				if (profile == null)
				{
					return NotFound("Applicant profile not found.");
				}
				return Ok(profile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get applicant profile by id {id}: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		// Add new applicant profile
		[HttpPost("Add")]
		public async Task<IActionResult> AddApplicantProfile([FromBody] ApplicantProfileDTO profileDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var newProfile = new ApplicantProfile
				{
					FirstName = profileDto.FirstName,
					LastName = profileDto.LastName,
					BirthDate = profileDto.BirthDate,
					Gender = profileDto.Gender,
					Nationality = profileDto.Nationality,
					Ethnicity = profileDto.Ethnicity,
					Avatar = profileDto.Avatar,
					ApplicantId = profileDto.ApplicantId
				};

				var addedProfile = await _applicantProfileRepo.Add(newProfile);
				return Ok(addedProfile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add applicant profile: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database.");
			}
		}

		// Update existing applicant profile
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateApplicantProfile(int id, [FromBody] ApplicantProfileDTO profileDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var profile = await _applicantProfileRepo.GetById(id);
				if (profile == null)
					return NotFound("Applicant profile not found.");

				profile.FirstName = profileDto.FirstName;
				profile.LastName = profileDto.LastName;
				profile.BirthDate = profileDto.BirthDate;
				profile.Gender = profileDto.Gender;
				profile.Nationality = profileDto.Nationality;
				profile.Ethnicity = profileDto.Ethnicity;
				profile.Avatar = profileDto.Avatar;
				profile.ApplicantId = profileDto.ApplicantId;

				var updatedProfile = await _applicantProfileRepo.Update(profile);
				return Ok(updatedProfile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update applicant profile: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
			}
		}

		// Delete applicant profile by ID
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteApplicantProfile(int id)
		{
			try
			{
				var deletedProfile = await _applicantProfileRepo.DeleteById(id);
				if (deletedProfile == null)
					return NotFound("Applicant profile not found.");

				return Ok(deletedProfile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete applicant profile: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
			}
		}
	}
}
