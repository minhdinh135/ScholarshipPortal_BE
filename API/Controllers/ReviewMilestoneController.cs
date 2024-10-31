using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.ReviewMilestone;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/requests")]
public class ReviewMilestoneController : ControllerBase
{
    private readonly IReviewMilestoneService _reviewMilestoneService;

    public ReviewMilestoneController(IReviewMilestoneService reviewMilestoneService)
    {
        _reviewMilestoneService = reviewMilestoneService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReviewMilestones()
    {
        var requests = await _reviewMilestoneService.GetAll();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all review milestones successfully", requests));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReviewMilestoneById(int id)
    {
        try
        {
            var request = await _reviewMilestoneService.GetById(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get review milestone successfully", request));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateReviewMilestone(AddReviewMilestoneDto addRequestDto)
    {
        try
        {
            var createdReviewMilestone = await _reviewMilestoneService.CreateReviewMilestone(addRequestDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Create review milestone successfully", createdReviewMilestone));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReviewMilestone(int id, UpdateReviewMilestoneDto updateRequestDto)
    {
        try
        {
            var updatedReviewMilestone = await _reviewMilestoneService.UpdateReviewMilestone(id, updateRequestDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update review milestone successfully", updatedReviewMilestone));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}
