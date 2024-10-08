using Application.Helper;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.Criteria;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/criteria")]
public class CriteriaController : ControllerBase
{
    private readonly ICriteriaService _criteriaService;

    public CriteriaController(ICriteriaService criteriaService)
    {
        _criteriaService = criteriaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCriteria()
    {
        var allCriteria = await _criteriaService.GetAllCriteria();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all criteria successfully", allCriteria));
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetCriteria([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = default, [FromQuery] string sortOrder = default)
    {
        var criteria = _criteriaService.GetCriteria(pageIndex, pageSize, sortBy, sortOrder);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get criteria successfully", criteria));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCriteriaById([FromRoute] int id)
    {
        var criteria = await _criteriaService.GetCriteriaById(id);

        if (criteria == null)
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Criteria not found", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get criteria successfully", criteria));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCriteria([FromBody] CreateCriteriaRequest createCriteriaRequest,
        [FromServices] IValidator<CreateCriteriaRequest> validator)
    {
        ValidationResult validationResult = validator.Validate(createCriteriaRequest);

        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ModelStateHelper.AddErrors(validationResult);

            return ValidationProblem(modelStateDictionary);
        }

        var createdCriteria = await _criteriaService.CreateCriteria(createCriteriaRequest);

        if (createdCriteria == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Create criteria failed", null));

        return Created("api/criteria/" + createdCriteria.Id,
            new ApiResponse(StatusCodes.Status201Created, "Create criteria successfully", createdCriteria));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCriteria([FromRoute] int id,
        [FromBody] UpdateCriteriaRequest updateCriteriaRequest,
        [FromServices] IValidator<UpdateCriteriaRequest> validator)
    {
        ValidationResult validationResult = validator.Validate(updateCriteriaRequest);

        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ModelStateHelper.AddErrors(validationResult);

            return ValidationProblem(modelStateDictionary);
        }

        var updatedCriteria = await _criteriaService.UpdateCriteria(id, updateCriteriaRequest);

        if (updatedCriteria == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Update criteria failed", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Update criteria successfully", updatedCriteria));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCriteria([FromRoute] int id)
    {
        var deletedCriteria = await _criteriaService.DeleteCriteriaById(id);

        if (deletedCriteria == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Delete criteria failed",
                deletedCriteria));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Delete criteria successfully", deletedCriteria));
    }
}