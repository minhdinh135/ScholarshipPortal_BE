using Application.Helper;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.Major;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/majors")]
public class MajorController : ControllerBase
{
    private readonly IMajorService _majorService;

    public MajorController(IMajorService majorService)
    {
        _majorService = majorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMajors()
    {
        var allMajors = await _majorService.GetAllMajors();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all majors successfully", allMajors));
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetMajors([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = default, [FromQuery] string sortOrder = default)
    {
        var majors = await _majorService.GetMajors(pageIndex, pageSize, sortBy, sortOrder);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get majors successfully", majors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMajorById([FromRoute] int id)
    {
        var major = await _majorService.GetMajorById(id);

        if (major == null)
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Major not found", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get major successfully", major));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMajor([FromBody] CreateMajorRequest createMajorRequest,
        [FromServices] IValidator<CreateMajorRequest> validator)
    {
        ValidationResult validationResult = validator.Validate(createMajorRequest);

        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ModelStateHelper.AddErrors(validationResult);

            return ValidationProblem(modelStateDictionary);
        }

        var createdMajor = await _majorService.CreateMajor(createMajorRequest);

        if (createdMajor == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Create major failed", null));

        return Created("/api/majors/" + createdMajor.Id,
            new ApiResponse(StatusCodes.Status201Created, "Create major successfully", createdMajor));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMajor([FromRoute] int id,
        [FromBody] UpdateMajorRequest updateMajorRequest, [FromServices] IValidator<UpdateMajorRequest> validator)
    {
        ValidationResult validationResult = validator.Validate(updateMajorRequest);

        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ModelStateHelper.AddErrors(validationResult);

            return ValidationProblem(modelStateDictionary);
        }

        var updatedMajor = await _majorService.UpdateMajor(id, updateMajorRequest);

        if (updatedMajor == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Update major failed", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Update major successfully", updatedMajor));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMajor([FromRoute] int id)
    {
        var deletedMajor = await _majorService.DeleteMajorById(id);

        if (deletedMajor == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Delete major failed", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Delete major successfully", deletedMajor));
    }
}