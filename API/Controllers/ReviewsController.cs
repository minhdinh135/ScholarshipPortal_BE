using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ReviewsController : ControllerBase
	{
		private readonly IReviewsService _reviewRepo;
		private readonly ILogger<ReviewsController> _logger;

		public ReviewsController(IReviewsService reviewRepo, ILogger<ReviewsController> logger)
		{
			_reviewRepo = reviewRepo;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllReviews()
		{
			try
			{
				var reviews = await _reviewRepo.GetAll();
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
				var review = await _reviewRepo.Get(id);
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
				var addedReview = await _reviewRepo.Add(reviewDto);
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
				var updatedReview = await _reviewRepo.Update(reviewDto);
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
				var deletedReview = await _reviewRepo.Delete(id);
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
