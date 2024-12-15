﻿using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Applicant;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/applicants")]
public class ApplicantController : ControllerBase
{
    private readonly IApplicantService _applicantService;
    private readonly IApplicationService _applicationService;
    private readonly IPdfService _pdfService;
    private readonly ICloudinaryService _cloudinaryService;

    public ApplicantController(IApplicantService applicantService,
        IApplicationService applicationService, IPdfService pdfService,
        ICloudinaryService cloudinaryService)
    {
        _applicantService = applicantService;
        _applicationService = applicationService;
        _pdfService = pdfService;
        _cloudinaryService = cloudinaryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllApplicants()
    {
        var applicants = await _applicantService.GetAllApplicantProfiles();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicants successfully", applicants));
    }

	[HttpPost("contract")]
	public async Task<IActionResult> GetContract([FromBody] ScholarshipContractRequest request)
	{
		if (request == null || string.IsNullOrEmpty(request.ApplicantName))
		{
			return BadRequest("Invalid input data");
		}

		var pdfBytes = await _pdfService.GenerateScholarshipContractPdf(
			request.ApplicantName,
			request.ScholarshipAmount,
			request.ScholarshipProviderName,
			request.Deadline
		);

		return File(pdfBytes, "application/pdf", "Scholarship_Contract.pdf");
	}

	[HttpPost("contract-uploaded")]
	public async Task<IActionResult> GetContractUploaded([FromBody] ScholarshipContractRequest request)
	{
		if (request == null || string.IsNullOrEmpty(request.ApplicantName))
		{
			return BadRequest("Invalid input data");
		}

		var resultUrl = await _cloudinaryService.CreateAndUploadScholarshipContract(
			request.ApplicantName,
			request.ScholarshipAmount,
			request.ScholarshipProviderName,
			request.Deadline
		);

		return Ok(new ApiResponse(StatusCodes.Status200OK, "Scholarship contract uploaded successfully", resultUrl));
	}


	[HttpGet("{applicantId}")]
    public async Task<IActionResult> GetApplicantProfile(int applicantId)
    {
        var applicant = await _applicantService.GetApplicantProfile(applicantId);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicant successfully", applicant));
    }

    [HttpGet("{id}/profile")]
    public async Task<IActionResult> GetApplicantProfileDetails(int id)
    {
        var applicant = await _applicantService.GetApplicantProfileDetails(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicant successfully", applicant));
    }

    [HttpGet("{applicantId}/applications")]
    public async Task<IActionResult> GetApplicationsByApplicantId(int applicantId)
    {
        var applications = await _applicationService.GetApplicationsByApplicantId(applicantId);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applications successfully", applications));
    }

    [HttpGet("by-applicantId-and-scholarshipId")]
    public async Task<IActionResult> GetApplicationsByApplicantIdAndScholarshipId([FromQuery] int applicantId,
        [FromQuery] int scholarshipId)
    {
        var applications = await _applicationService.GetApplicationsByApplicantId(applicantId);
        applications = applications.Where(x => x.ScholarshipProgramId == scholarshipId).ToList();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applications successfully", applications));
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> AddApplicantProfile(int id, AddApplicantProfileDto addApplicantProfileDto)
    {
        try
        {
            var addedProfile = await _applicantService.AddApplicantProfile(id, addApplicantProfileDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Add applicant profile successfully", addedProfile));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Add applicant profile failed", null));
        }
    }

    [HttpPut("{applicantId}")]
    public async Task<IActionResult> UpdateApplicantProfile(int applicantId,
        UpdateApplicantProfileDto updateApplicantProfileDto)
    {
        try
        {
            var updatedProfile = await _applicantService.UpdateApplicantProfile(applicantId, updateApplicantProfileDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update applicant profile successfully",
                updatedProfile));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Update applicant profile failed",
                null));
        }
    }

    [HttpPut("{applicantId}/profile")]
    public async Task<IActionResult> UpdateApplicantProfileDetails(int applicantId,
        UpdateApplicantProfileDetails updateProfileDetails)
    {
        try
        {
            var updatedProfile = await _applicantService.UpdateApplicantProfileDetails(applicantId, updateProfileDetails);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update applicant profile successfully",
                updatedProfile));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Update applicant profile failed",
                null));
        }
    }

    [HttpPut("{applicantId}/skills")]
    public async Task<IActionResult> UpdateProfileSkills(int applicantId,
        List<UpdateApplicantSkillDto> updateSkillDtos)
    {
        try
        {
            await _applicantService.UpdateProfileSkills(applicantId, updateSkillDtos);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update skills successfuly", null));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
        }
    }

    [HttpGet("{applicantId}/profile/pdf")]
    public async Task<IActionResult> ExportApplicantProfileToPdf(int applicantId)
    {
        try
        {
            var applicantProfile = await _applicantService.GetApplicantProfileDetails(applicantId);
            var pdf = await _applicantService.ExportApplicantProfileToPdf(applicantId);

            return File(pdf, "application/pdf",
                $"ApplicantCV_{applicantProfile.FirstName}{applicantProfile.LastName}.pdf");
        }
        catch (NotFoundException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
        }
    }
}
