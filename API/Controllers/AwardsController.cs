using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.DTOs;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AwardsController : ControllerBase
	{
		private readonly IGenericRepository<Award> _awardRepo;
		private readonly ILogger<AwardsController> _logger;

		public AwardsController(IGenericRepository<Award> awardRepo, ILogger<AwardsController> logger)
		{
			_awardRepo = awardRepo;
			_logger = logger;
		}

		// Get all awards
		[HttpGet]
		public async Task<IActionResult> GetAllAwards()
		{
			try
			{
				var awards = await _awardRepo.GetAll(x => x.Include(a => a.Application));
				return Ok(awards);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all awards: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		// Get award by ID
		[HttpGet("{id}")]
		public async Task<IActionResult> GetAwardById(Guid id)
		{
			try
			{
				var award = await _awardRepo.Get(id);
				if (award == null)
				{
					return NotFound("Award not found.");
				}
				return Ok(award);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get award by id {id}: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		// Add new award
		[HttpPost("Add")]
		public async Task<IActionResult> AddAward([FromBody] AwardDTO awardDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var newAward = new Award
				{
					Description = awardDto.Description,
					Amount = awardDto.Amount,
					Image = awardDto.Image,
					AwardedDate = awardDto.AwardedDate,
					ApplicationId = awardDto.ApplicationId
				};

				var addedAward = await _awardRepo.Add(newAward);
				return Ok(addedAward);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add award: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database.");
			}
		}

		// Update existing award
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAward(Guid id, [FromBody] AwardDTO awardDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var award = await _awardRepo.Get(id);
				if (award == null)
					return NotFound("Award not found.");

				award.Description = awardDto.Description;
				award.Amount = awardDto.Amount;
				award.Image = awardDto.Image;
				award.AwardedDate = awardDto.AwardedDate;
				award.ApplicationId = awardDto.ApplicationId;

				var updatedAward = await _awardRepo.Update(award);
				return Ok(updatedAward);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update award: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
			}
		}

		// Delete award by ID
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAward(Guid id)
		{
			try
			{
				var deletedAward = await _awardRepo.Delete(id);
				if (deletedAward == null)
					return NotFound("Award not found.");

				return Ok(deletedAward);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete award: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
			}
		}
	}
}
