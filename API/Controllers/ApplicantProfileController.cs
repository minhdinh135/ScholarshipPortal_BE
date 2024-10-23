using Application.Interfaces.IServices;
using Domain.DTOs;
using Domain.DTOs.ApplicantProfile;
using Infrastructure.ExternalServices.ExportPDF;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/applicant-profiles")]
	public class ApplicantProfileController : ControllerBase
	{
		private readonly IApplicantProfileService _applicantProfileService;
		private readonly ILogger<ApplicantProfileController> _logger;
		private readonly IPdfExportService _pdfExportService;

		public ApplicantProfileController(IApplicantProfileService applicantProfileService, ILogger<ApplicantProfileController> logger, IPdfExportService pdfExportService)
		{
			_applicantProfileService = applicantProfileService;
			_logger = logger;
			_pdfExportService = pdfExportService;

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

		[HttpGet("byApplicant/{id}")]
		public async Task<IActionResult> GetAllApplicantProfilesByApplicant(int id)
		{
			try
			{
				var profiles = await _applicantProfileService.GetAll();
				profiles = profiles.Where(x => x.ApplicantId == id);
				return Ok(profiles.FirstOrDefault());
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

		[HttpPost("AddOrUpdateProfile")]
		public async Task<IActionResult> AddOrUpdateApplicantProfile([FromBody] AddApplicantProfileDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var existingProfiles = await _applicantProfileService.GetAll();
				var existingProfile = existingProfiles.Where(x => x.ApplicantId == dto.ApplicantId).FirstOrDefault();
				Console.WriteLine("aaaaa");
				if (existingProfile == null)
				{
					var addedProfile = await _applicantProfileService.Add(dto);
					return Ok(new { Message = "Profile created successfully", Profile = addedProfile });
				}
				else
				{
					var updateDto = new UpdateApplicantProfileDTO
					{
						Id = existingProfile.Id,
						FirstName = dto.FirstName,
						LastName = dto.LastName,
						BirthDate = dto.BirthDate,
						Gender = dto.Gender,
						Nationality = dto.Nationality,
						Ethnicity = dto.Ethnicity,
						ApplicantId = dto.ApplicantId,
					};

					var updatedProfile = await _applicantProfileService.Update(updateDto);
					return Ok(new { Message = "Profile updated successfully", Profile = updatedProfile });
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add or update applicant profile: {ex.Message}");
				return StatusCode(500, "Error processing your request.");
			}
		}

		[HttpGet("{id}/export-pdf")]
		public async Task<IActionResult> ExportApplicantProfileToPdf(int id)
		{
			//try
			//{
				var profile = await _applicantProfileService.Get(id);
				if (profile == null) return NotFound("Applicant profile not found.");

				var pdfStream = _pdfExportService.ExportProfileToPdf(profile);

				return File(pdfStream, "application/pdf", $"ApplicantProfile_{profile.FirstName}_{profile.LastName}.pdf");
			//catch (Exception ex)
			//{
			//	_logger.LogError($"Failed to export applicant profile to PDF: {ex.Message}");
			//	return StatusCode(500, "Error generating PDF.");
			//}
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
