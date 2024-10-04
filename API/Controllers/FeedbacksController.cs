using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.DTOs;
using Application.Interfaces.IServices;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FeedbacksController : ControllerBase
	{
		private readonly IFeedbacksService _feedbackService;
		private readonly ILogger<FeedbacksController> _logger;

		public FeedbacksController(IFeedbacksService feedbackService, ILogger<FeedbacksController> logger)
		{
			_feedbackService = feedbackService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllFeedbacks()
		{
			try
			{
				var feedbacks = await _feedbackService.GetAll();
				return Ok(feedbacks);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all feedbacks: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetFeedbackById(int id)
		{
			try
			{
				var feedback = await _feedbackService.Get(id);
				if (feedback == null)
				{
					return NotFound("Feedback not found.");
				}
				return Ok(feedback);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get feedback by id {id}: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddFeedback([FromBody] AddFeedbackDTO feedbackDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var addedFeedback = await _feedbackService.Add(feedbackDto);
				return Ok(addedFeedback);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add feedback: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database.");
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateFeedback([FromBody] UpdateFeedbackDTO feedbackDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{

				var updatedFeedback = await _feedbackService.Update(feedbackDto);
				return Ok(updatedFeedback);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update feedback: {ex.Message}");
				return BadRequest(new { Message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFeedback(int id)
		{
			try
			{
				var deletedFeedback = await _feedbackService.Delete(id);
				if (deletedFeedback == null)
					return NotFound("Feedback not found.");

				return Ok(deletedFeedback);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete feedback: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
			}
		}
	}
}
