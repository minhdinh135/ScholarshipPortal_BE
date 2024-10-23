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
    private readonly ILogger<ApplicantController> _logger;

    public ApplicantController(IApplicantService applicantService, ILogger<ApplicantController> logger)
    {
        _applicantService = applicantService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllApplicants()
    {
        var applicants = await _applicantService.GetAll();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicants successfully", applicants));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicant(int id)
    {
        var applicant = await _applicantService.Get(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get applicant successfully", applicant));
    }

    [HttpPost]
    public async Task<IActionResult> AddApplicantProfile(AddApplicantProfileDto addApplicantProfileDto)
    {
        try
        {
            var addedProfile = await _applicantService.Add(addApplicantProfileDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Add applicant profile successfully", addedProfile));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Add applicant profile failed", null));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApplicantProfile(int id, UpdateApplicantProfileDto updateApplicantProfileDto)
    {
        try
        {
            var updatedProfile = await _applicantService.Update(id, updateApplicantProfileDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update applicant profile successfully",
                updatedProfile));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Update applicant profile failed",
                null));
        }
    }
}