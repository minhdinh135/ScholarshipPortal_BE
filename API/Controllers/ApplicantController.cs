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
    private readonly ILogger<ApplicantController> _logger;
    private readonly IApplicantService _applicantService;

    public ApplicantController(ILogger<ApplicantController> logger, IApplicantService applicantService)
    {
        _applicantService = applicantService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllApplicants()
    {
        var applicants = await _applicantService.GetAllApplicantProfiles();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicants successfully", applicants));
    }

    [HttpGet("{applicantId}")]
    public async Task<IActionResult> GetApplicantProfile(int applicantId)
    {
        var applicant = await _applicantService.GetApplicantProfile(applicantId);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicant successfully", applicant));
    }

    [HttpPost]
    public async Task<IActionResult> AddApplicantProfile(AddApplicantProfileDto addApplicantProfileDto)
    {
        try
        {
            var addedProfile = await _applicantService.AddApplicantProfile(addApplicantProfileDto);

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

    // [HttpPost("{applicantId}/achievements")]
    // public async Task<IActionResult> AddProfileAchievements(int applicantId, List<AddAchievementDto> addAchievementDto)
    // {
    //     try
    //     {
    //         var achievementIds = await _applicantService.AddProfileAchievements(applicantId, addAchievementDto);
    //
    //         return Ok(new ApiResponse(StatusCodes.Status200OK,
    //             $"Add achievements successfully with ids:{achievementIds}", achievementIds));
    //     }
    //     catch (NotFoundException e)
    //     {
    //         return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
    //     }
    //     catch (ServiceException e)
    //     {
    //         return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
    //     }
    // }

    [HttpPut("{applicantId}/achievements")]
    public async Task<IActionResult> UpdateProfileAchievements(int applicantId,
        List<UpdateAchievementDto> updateAchievementDtos)
    {
        try
        {
            await _applicantService.UpdateProfileAchievements(applicantId, updateAchievementDtos);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update achievements successfuly", null));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
        }
    }

    // [HttpPost("{applicantId}/skills")]
    // public async Task<IActionResult> AddProfileSkills(int applicantId, List<AddApplicantSkillDto> addSkillDtos)
    // {
    //     try
    //     {
    //         var skillIds = await _applicantService.AddProfileSkills(applicantId, addSkillDtos);
    //
    //         return Ok(new ApiResponse(StatusCodes.Status200OK,
    //             $"Add skills successfully with ids:{skillIds}", skillIds));
    //     }
    //     catch (NotFoundException e)
    //     {
    //         return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
    //     }
    //     catch (ServiceException e)
    //     {
    //         return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
    //     }
    // }

    //[HttpPut("{applicantId}/skills")]
    //public async Task<IActionResult> UpdateProfileSkills(int applicantId,
    //    List<UpdateApplicantSkillDto> updateSkillDtos)
    //{
    //    try
    //    {
    //        await _applicantService.UpdateProfileSkills(applicantId, updateSkillDtos);

    //        return Ok(new ApiResponse(StatusCodes.Status200OK, "Update skills successfuly", null));
    //    }
    //    catch (ServiceException e)
    //    {
    //        return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
    //    }
    //}

    // [HttpPost("{applicantId}/certificates")]
    // public async Task<IActionResult> AddProfileCertificates(int applicantId,
    //     List<AddApplicantCertificateDto> addApplicantCertificateDtos)
    // {
    //     try
    //     {
    //         var certificateIds =
    //             await _applicantService.AddProfileCertificates(applicantId, addApplicantCertificateDtos);
    //
    //         return Ok(new ApiResponse(StatusCodes.Status200OK,
    //             $"Add certificates successfully with ids:{certificateIds}", certificateIds));
    //     }
    //     catch (NotFoundException e)
    //     {
    //         return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
    //     }
    //     catch (ServiceException e)
    //     {
    //         return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
    //     }
    // }

    [HttpPut("{applicantId}/certificates")]
    public async Task<IActionResult> UpdateProfileAchievements(int applicantId,
        List<UpdateApplicantCertificateDto> updateApplicantCertificateDtos)
    {
        try
        {
            await _applicantService.UpdateProfileCertificates(applicantId, updateApplicantCertificateDtos);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update certificates successfuly", null));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
        }
    }

    [HttpPost("certificates/images")]
    public async Task<IActionResult> UploadCertificateImages(IFormFileCollection images)
    {
        try
        {
            List<string> certificateUrls = await _applicantService.UploadCertificateImages(images);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Upload certificate images successfully",
                certificateUrls));
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
            var applicantProfile = await _applicantService.GetApplicantProfile(applicantId);
            var pdf = await _applicantService.ExportApplicantProfileToPdf(applicantId);

            return File(pdf, "application/pdf",
                $"ApplicantCV_{applicantProfile.FirstName}{applicantProfile.LastName}.pdf");
        }
        catch (NotFoundException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
        }
    }

	[HttpPost("{applicantId}/skills")]
	public async Task<IActionResult> AddApplicantSkills(int applicantId, List<AddApplicantSkillDto> addApplicantSkillDtos)
	{

			var addedSkillIds = await _applicantService.AddProfileSkills(applicantId, addApplicantSkillDtos);
			return Ok(new ApiResponse(StatusCodes.Status200OK, "Add applicant skills successfully", addedSkillIds));

	}

	[HttpPut("{applicantId}/skills")]
	public async Task<IActionResult> UpdateApplicantSkills(int applicantId, List<UpdateApplicantSkillDto> updateApplicantSkillDtos)
	{
		try
		{
			await _applicantService.UpdateProfileSkills(applicantId, updateApplicantSkillDtos);
			return Ok(new ApiResponse(StatusCodes.Status200OK, "Update applicant skills successfully", null));
		}
		catch (Exception e)
		{
			return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Update applicant skills failed", null));
		}
	}

	[HttpGet("{applicantId}/skills")]
	public async Task<IActionResult> GetApplicantSkills(int applicantId)
	{
		var skills = await _applicantService.GetSkillsByApplicantId(applicantId);
		return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicant skills successfully", skills));
	}

}