using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.ScholarshipProgram;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/scholarship-programs")]
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
        // var allScholarshipPrograms = await _scholarshipProgramService.GetAllScholarshipPrograms();
        var allScholarshipPrograms = await _scholarshipProgramService.GetAllPrograms(listOptions);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all scholarship programs successfully",
            allScholarshipPrograms));
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchScholarships([FromQuery] ScholarshipSearchOptions scholarshipSearchOptions)
    {
        var response = await _scholarshipProgramService.SearchScholarships(scholarshipSearchOptions);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Search successfully", response));
    }

    [HttpGet("suggest")]
    public async Task<IActionResult> SuggestScholarships([FromQuery] string input)
    {
        var response = await _scholarshipProgramService.SuggestScholarships(input);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Suggest successfully", response));
    }

    [HttpGet("by-funder-id/{id}")]
    public async Task<IActionResult> GetAllScholarshipProgramsByFunderId([FromRoute] int id)
    {
        var allScholarshipPrograms = await _scholarshipProgramService.GetScholarshipProgramsByFunderId(id);

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

    [HttpGet("paginated")]
    public async Task<IActionResult> GetScholarshipPrograms([FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = default, [FromQuery] string sortOrder = default)
    {
        var scholarshipPrograms =
            await _scholarshipProgramService.GetScholarshipPrograms(pageIndex, pageSize, sortBy, sortOrder);

        return Ok(
            new ApiResponse(StatusCodes.Status200OK, "Get scholarship programs successfully", scholarshipPrograms));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetScholarshipProgramById([FromRoute] int id)
    {
        try
        {
            var scholarshipProgram = await _scholarshipProgramService.GetScholarshipProgramById(id);

            // var userId = HttpContext.User.FindFirst("id")?.Value;
            // if(userId == null)
            //     return Ok(new ApiResponse(StatusCodes.Status200OK, "Unauthorized", scholarshipProgram));
            // Console.WriteLine(userId);
            // if(scholarshipProgram.FunderId != int.Parse(userId))
            //     return Ok(new ApiResponse(StatusCodes.Status200OK, "Unauthorized", scholarshipProgram));

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

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get scholarship prograom applications successfully",
            applications));
    }

    [HttpPost]
    public async Task<IActionResult> CreateScholarshipProgram(
        [FromBody] CreateScholarshipProgramRequest createScholarshipProgramRequest)
    {
        var result =
            await _scholarshipProgramService.CreateScholarshipProgram(createScholarshipProgramRequest);

        if (result == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Create scholarship program failed"));

        return Created("api/scholarship-programs/" + result, new ApiResponse(
            StatusCodes.Status201Created, $"Create scholarship program successfully with id:{result}",
            new { Id = result }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateScholarshipProgram([FromRoute] int id,
        [FromBody] UpdateScholarshipProgramRequest updateScholarshipProgramRequest)
    {
        try
        {
            await _scholarshipProgramService.UpdateScholarshipProgram(id, updateScholarshipProgramRequest);
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update scholarship program successfully"));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}/image")]
    public async Task<IActionResult> UpdateScholarshipProgramImage(int id, IFormFile file)
    {
        try
        {
            await _scholarshipProgramService.UploadScholarshipProgramImage(id, file);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Upload image successfully", null));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest,
                "Error uploading program image: " + e.Message, null));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScholarshipProgram([FromRoute] int id)
    {
        var deletedScholarshipProgram = await _scholarshipProgramService.DeleteScholarshipProgramById(id);

        if (deletedScholarshipProgram == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Delete scholarship program failed",
                null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Delete scholarship program successfully",
            deletedScholarshipProgram));
    }
}