using Application.Interfaces.IServices;
using Domain.DTOs;
using Domain.DTOs.Review;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/reviews")]
	public class ReviewController : ControllerBase
	{
		private readonly IReviewService _reviewService;
		private readonly ILogger<ReviewController> _logger;

		public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
		{
			_reviewService = reviewService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllReviews()
		{
			try
			{
				var reviews = await _reviewService.GetAll();
				return Ok(reviews);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all reviews: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetReviewById(int id)
		{
			try
			{
				var review = await _reviewService.Get(id);
				if (review == null)
				{
					return NotFound("Review not found.");
				}
				return Ok(review);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get review by id {id}: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddReview([FromBody] AddReviewDTO reviewDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var addedReview = await _reviewService.Add(reviewDto);
				return Ok(addedReview);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add review: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database.");
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewDTO reviewDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var updatedReview = await _reviewService.Update(reviewDto);
				return Ok(updatedReview);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update review: {ex.Message}");
				return BadRequest(new { Message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteReview(int id)
		{
			try
			{
				var deletedReview = await _reviewService.Delete(id);
				if (deletedReview == null)
					return NotFound("Review not found.");

				return Ok(deletedReview);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete review: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
			}
		}
	}
}
