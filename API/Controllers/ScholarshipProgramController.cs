using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.ScholarshipProgram;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route(UriConstant.SCHOLARSHIP_PROGRAM_BASE_URI)]
public class ScholarshipProgramController : ControllerBase
{
    private readonly IScholarshipProgramService _scholarshipProgramService;
    private readonly IApplicationService _applicationService;

    public ScholarshipProgramController(IScholarshipProgramService scholarshipProgramService,
        IApplicationService applicationService)
    {
        _scholarshipProgramService = scholarshipProgramService;
        _applicationService = applicationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllScholarshipPrograms([FromQuery] ListOptions listOptions)
    {
        var result = await _scholarshipProgramService.GetAllPrograms(listOptions);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get scholarship programs successfully",
            result));
    }

    [HttpGet("count")]
    public async Task<IActionResult> CountScholarshipPrograms()
    {
        var allScholarshipPrograms = await _scholarshipProgramService.GetAllScholarshipPrograms();
        var count = allScholarshipPrograms.Count();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all scholarship programs successfully",
            count));
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchScholarships([FromQuery] ScholarshipSearchOptions scholarshipSearchOptions)
    {
        try
        {
            var result = await _scholarshipProgramService.SearchScholarshipPrograms(scholarshipSearchOptions);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Search scholarships successfully", result));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("by-funder-id/{id}")]
    public async Task<IActionResult> GetAllScholarshipProgramsByFunderId([FromQuery] ListOptions listOptions, int id)
    {
        var allScholarshipPrograms = await _scholarshipProgramService.GetScholarshipProgramsByFunderId(listOptions, id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all scholarship programs successfully",
            allScholarshipPrograms));
    }

    [HttpGet("by-major-id/{id}")]
    public async Task<IActionResult> GetAllScholarshipProgramsByMajorId([FromRoute] int id)
    {
        var allScholarshipPrograms = await _scholarshipProgramService.GetScholarshipProgramsByMajorId(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all scholarship programs successfully",
            allScholarshipPrograms));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetScholarshipProgramById([FromRoute] int id)
    {
        try
        {
            var scholarshipProgram = await _scholarshipProgramService.GetScholarshipProgramById(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get scholarship program successfully",
                scholarshipProgram));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("{id}/applications")]
    public async Task<IActionResult> GetAllScholarshipProgramApplications(int id)
    {
        var applications = await _applicationService.GetApplicationsByScholarshipProgramId(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get scholarship program applications successfully",
            applications));
    }

    [HttpGet("{id}/experts")]
    public async Task<IActionResult> GetAllScholarshipProgramExperts(int id)
    {
        var experts = await _scholarshipProgramService.GetScholarshipProgramExperts(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get scholarship program experts successfully",
            experts));
    }

    [HttpPost]
    public async Task<IActionResult> CreateScholarshipProgram(
        CreateScholarshipProgramRequest createScholarshipProgramRequest)
    {
        try
        {
            var result =
                await _scholarshipProgramService.CreateScholarshipProgram(createScholarshipProgramRequest);

            if (result == null)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest,
                    "Create scholarship program failed"));

            return Created("api/scholarship-programs/" + result, new ApiResponse(
                StatusCodes.Status201Created, $"Create scholarship program successfully with id:{result}",
                new { Id = result }));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost("{id}/experts")]
    public async Task<IActionResult> AssignExpertsToScholarshipProgram(int id, AssignExpertsToProgramRequest request)
    {
        try
        {
            await _scholarshipProgramService.AssignExpertsToScholarshipProgram(id, request.ExpertIds);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Assign experts to scholarship program successfully"));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}/experts")]
    public async Task<IActionResult> RemoveExpertsFromScholarshipProgram(int id, RemoveExpertsFromScholarshipProgramRequest request)
    {
        try
        {
            await _scholarshipProgramService.RemoveExpertsFromScholarshipProgram(id, request.ExpertIds);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Remove experts from scholarship program successfully"));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateScholarshipProgram(int id,
        UpdateScholarshipProgramRequest updateScholarshipProgramRequest)
    {
        try
        {
            var result = await _scholarshipProgramService.UpdateScholarshipProgram(id, updateScholarshipProgramRequest);
            return Ok(new ApiResponse(StatusCodes.Status200OK,
                $"Update scholarship program with id:{result} successfully", result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeScholarshipProgramStatus(int id,
        ChangeScholarshipProgramStatusRequest request)
    {
        try
        {
            await _scholarshipProgramService.ChangeScholarshipProgramStatus(id, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK,
                $"Change status for scholarship program with ID: {id} successfully", id));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    // [HttpPut("update-status/{id}")]
    // public async Task<IActionResult> UpdateScholarshipProgramStatus([FromRoute] int id,
    //     [FromBody] string status)
    // {
    //     try
    //     {
    //         await _scholarshipProgramService.UpdateScholarshipProgramStatus(id, status);
    //         return Ok(new ApiResponse(StatusCodes.Status200OK, "Update scholarship program successfully"));
    //     }
    //     catch (ServiceException e)
    //     {
    //         return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
    //     }
    // }
}