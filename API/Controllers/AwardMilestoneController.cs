using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.AwardMilestone;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route(UriConstant.AWARD_MILESTONE_BASE_URI)]
public class AwardMilestoneController : ControllerBase
{
    private readonly IAwardMilestoneService _awardMilestoneService;

    public AwardMilestoneController(IAwardMilestoneService awardMilestoneService)
    {
        _awardMilestoneService = awardMilestoneService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAwardMilestones()
    {
        var allCategories = await _awardMilestoneService.GetAll();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all award milestones successfully", allCategories));
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetAwardMilestones([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = "Id", [FromQuery] string sortOrder = "asc")
    {
        var categories = await _awardMilestoneService.GetAll(pageIndex, pageSize, sortBy, sortOrder);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get award milestones successfully", categories));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAwardMilestoneById([FromRoute] int id)
    {
        var category = await _awardMilestoneService.Get(id);

        if (category == null)
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Award milestone not found", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get award milestone successfully", category));
    }

    [HttpGet("by-scholarship/{id}")]
    public async Task<IActionResult> GetAwardMilestoneByScholarshipId([FromRoute] int id)
    {
        var category = await _awardMilestoneService.GetByScholarshipId(id);

        if (category == null)
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Award milestone not found", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get award milestone successfully", category));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAwardMilestone([FromBody] CreateAwardMilestoneDto dto)
    {
        var createdCategory = await _awardMilestoneService.Add(dto);

        if (createdCategory == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Create award milestone failed", null));

        return Created("/api/award-milestones/" + createdCategory.Id,
            new ApiResponse(StatusCodes.Status201Created, "Create award milestone successfully", createdCategory));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAwardMilestone([FromRoute] int id, [FromBody] UpdateAwardMilestoneDto dto)
    {
        var updatedCategory = await _awardMilestoneService.Update(id, dto);

        if (updatedCategory == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Update award milestone failed", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Update award milestone successfully", updatedCategory));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAwardMilestone([FromRoute] int id)
    {
        var deletedCategory = await _awardMilestoneService.Delete(id);

        if (deletedCategory == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Delete award milestone failed", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Delete award milestone successfully", deletedCategory));
    }
}
