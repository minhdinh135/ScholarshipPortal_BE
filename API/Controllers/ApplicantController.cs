using Application.Exceptions;
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
    private readonly IApplicationService _applicationService;

    public ApplicantController(ILogger<ApplicantController> logger, IApplicantService applicantService, IApplicationService applicationService)
    {
        _logger = logger;
        _applicantService = applicantService;
        _applicationService = applicationService;
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

    [HttpGet("{applicantId}/applications")]
    public async Task<IActionResult> GetApplicationsByApplicantId(int applicantId)
    {
        var applications = await _applicationService.GetApplicationsByApplicantId(applicantId);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applications successfully", applications));
    }

    [HttpGet("by-applicantId-and-scholarshipId")]
    public async Task<IActionResult> GetApplicationsByApplicantIdAndScholarshipId([FromQuery]int applicantId,
            [FromQuery]int scholarshipId)
    {
        var applications = await _applicationService.GetApplicationsByApplicantId(applicantId);
        applications = applications.Where(x => x.ScholarshipProgramId == scholarshipId).ToList();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applications successfully", applications));
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
}
