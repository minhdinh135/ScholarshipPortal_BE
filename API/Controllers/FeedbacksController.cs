using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.DTOs;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FeedbacksController : ControllerBase
	{
		private readonly IGenericRepository<Feedback> _feedbackRepo;
		private readonly ILogger<FeedbacksController> _logger;

		public FeedbacksController(IGenericRepository<Feedback> feedbackRepo, ILogger<FeedbacksController> logger)
		{
			_feedbackRepo = feedbackRepo;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllFeedbacks()
		{
			try
			{
				var feedbacks = await _feedbackRepo.GetAll(
					x => x.Include(f => f.Funder).Include(f => f.Provider)
				);
				return Ok(feedbacks);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all feedbacks: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetFeedbackById(Guid id)
		{
			try
			{
				var feedback = await _feedbackRepo.Get(id);
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
		public async Task<IActionResult> AddFeedback([FromBody] FeedbackDTO feedbackDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var newFeedback = new Feedback
				{
					Content = feedbackDto.Content,
					Rating = feedbackDto.Rating,
					FeedbackDate = feedbackDto.FeedbackDate,
					FunderId = feedbackDto.FunderId,
					ProviderId = feedbackDto.ProviderId
				};

				var addedFeedback = await _feedbackRepo.Add(newFeedback);
				return Ok(addedFeedback);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add feedback: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database.");
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateFeedback(Guid id, [FromBody] FeedbackDTO feedbackDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var feedback = await _feedbackRepo.Get(id);
				if (feedback == null)
					return NotFound("Feedback not found.");

				feedback.Content = feedbackDto.Content;
				feedback.Rating = feedbackDto.Rating;
				feedback.FeedbackDate = feedbackDto.FeedbackDate;
				feedback.FunderId = feedbackDto.FunderId;
				feedback.ProviderId = feedbackDto.ProviderId;

				var updatedFeedback = await _feedbackRepo.Update(feedback);
				return Ok(updatedFeedback);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update feedback: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFeedback(Guid id)
		{
			try
			{
				var deletedFeedback = await _feedbackRepo.Delete(id);
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
