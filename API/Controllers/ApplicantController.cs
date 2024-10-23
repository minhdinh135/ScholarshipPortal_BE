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
    public async Task<IActionResult> GetApplicantPrfile(int applicantId)
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

    [HttpGet("{applicantId}/profile/pdf")]
    public async Task<IActionResult> ExportApplicantProfileToPdf(int applicantId)
    {
        try
        {
            var applicantProfile = await _applicantService.GetApplicantProfile(applicantId);
            var pdf = await _applicantService.ExportApplicantProfileToPdf(applicantId);

            return File(pdf, "application/pdf", $"ApplicantCV_{applicantProfile.FirstName}{applicantProfile.LastName}.pdf");
        }
        catch (NotFoundException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message, null));
        }
    }
}