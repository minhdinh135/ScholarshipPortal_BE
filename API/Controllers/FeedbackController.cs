using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Feedback;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route(UriConstant.FEEDBACK_BASE_URI)]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;

    public FeedbackController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFeedbacks()
    {
        var feedbacks = await _feedbackService.GetAllFeedbacks();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all feedabacks successfully", feedbacks));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFeedbackById(int id)
    {
        try
        {
            var feedback = await _feedbackService.GetFeedbackById(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get feedback successfuly", feedback));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

	[HttpPost]
	public async Task<IActionResult> AddFeedback(AddFeedbackDto addFeedbackDto)
	{
		try
		{
			var existingFeedback = await _feedbackService.GetAllFeedbacks();
			if (existingFeedback.Any(f => f.ApplicantId == addFeedbackDto.ApplicantId && f.ServiceId == addFeedbackDto.ServiceId))
			{
				return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "You have already feedback this service!"));
			}

			var addedFeedback = await _feedbackService.AddFeedback(addFeedbackDto);
			return Ok(new ApiResponse(StatusCodes.Status200OK, "Add feedback successfully", addedFeedback));
		}
		catch (ServiceException e)
		{
			return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
		}
	}


	[HttpPut("{id}")]
    public async Task<IActionResult> UpdateFeedback(int id, UpdateFeedbackDto updateFeedbackDto)
    {
        try
        {
            var updatedFeedback = await _feedbackService.UpdateFeedback(id, updateFeedbackDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update feedback successfully", updatedFeedback));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}