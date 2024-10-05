using Application.Interfaces.IRepositories;
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
		private readonly IGenericRepository<Review> _reviewRepo;
		private readonly ILogger<ReviewsController> _logger;

		public ReviewsController(IGenericRepository<Review> reviewRepo, ILogger<ReviewsController> logger)
		{
			_reviewRepo = reviewRepo;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllReviews()
		{
			try
			{
				var reviews = await _reviewRepo.GetAll(
					x => x.Include(r => r.Provider).Include(r => r.Application)
				);
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
				var review = await _reviewRepo.GetById(id);
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
		public async Task<IActionResult> AddReview([FromBody] ReviewDTO reviewDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var newReview = new Review
				{
					Comment = reviewDto.Comment,
					Score = reviewDto.Score,
					ReviewedDate = reviewDto.ReviewedDate,
					ProviderId = reviewDto.ProviderId,
					ApplicationId = reviewDto.ApplicationId
				};

				var addedReview = await _reviewRepo.Add(newReview);
				return Ok(addedReview);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add review: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database.");
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDTO reviewDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var review = await _reviewRepo.GetById(id);
				if (review == null)
					return NotFound("Review not found.");

				review.Comment = reviewDto.Comment;
				review.Score = reviewDto.Score;
				review.ReviewedDate = reviewDto.ReviewedDate;
				review.ProviderId = reviewDto.ProviderId;
				review.ApplicationId = reviewDto.ApplicationId;

				var updatedReview = await _reviewRepo.Update(review);
				return Ok(updatedReview);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update review: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteReview(int id)
		{
			try
			{
				var deletedReview = await _reviewRepo.DeleteById(id);
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
