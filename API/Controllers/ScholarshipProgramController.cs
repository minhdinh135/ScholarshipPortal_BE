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

    public ScholarshipProgramController(IScholarshipProgramService scholarshipProgramService)
    {
        _scholarshipProgramService = scholarshipProgramService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllScholarshipPrograms()
    {
        var allScholarshipPrograms = await _scholarshipProgramService.GetAllScholarshipPrograms();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all scholarship programs successfully",
            allScholarshipPrograms));
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
        var scholarshipProgram = await _scholarshipProgramService.GetScholarshipProgramById(id);
        var userId = HttpContext.User.FindFirst("id")?.Value;
        if(userId == null)
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Unauthorized", scholarshipProgram));
        //Console.WriteLine(userId);
        if(scholarshipProgram.FunderId != int.Parse(userId))
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Unauthorized", scholarshipProgram));

        if (scholarshipProgram == null)
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Scholarship program not found", null));


        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get scholarship program successfully", scholarshipProgram));
    }

    [HttpPost]
    public async Task<IActionResult> CreateScholarshipProgram(
        [FromBody] CreateScholarshipProgramRequest createScholarshipProgramRequest)
    {
        var createdScholarshipProgram =
            await _scholarshipProgramService.CreateScholarshipProgram(createScholarshipProgramRequest);

        if (createdScholarshipProgram == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Create scholarship program failed",
                null));

        return Created("api/scholarship-programs/" + createdScholarshipProgram.Id, new ApiResponse(
            StatusCodes.Status200OK, "Create scholarship program successfully",
            createdScholarshipProgram));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateScholarshipProgram([FromRoute] int id,
        [FromBody] UpdateScholarshipProgramRequest updateScholarshipProgramRequest)
    {
        var updatedScholarshipProgram =
            await _scholarshipProgramService.UpdateScholarshipProgram(id, updateScholarshipProgramRequest);

        if (updatedScholarshipProgram == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Update scholarship program failed",
                null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Update scholarship program successfully",
            updatedScholarshipProgram));
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
