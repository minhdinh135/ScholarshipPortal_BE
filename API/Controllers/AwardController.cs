using Application.Interfaces.IServices;
using Domain.DTOs;
using Domain.DTOs.Award;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/awards")]
	public class AwardController : ControllerBase
	{
		private readonly IAwardService _awardService;
		private readonly ILogger<AwardController> _logger;

		public AwardController(IAwardService awardService, ILogger<AwardController> logger)
		{
			_awardService = awardService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAwards()
		{
			try
			{
				var awards = await _awardService.GetAll();
				return Ok(awards);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all awards: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAwardById(int id)
		{
			try
			{
				var award = await _awardService.Get(id);
				if (award == null) return NotFound("Award not found.");
				return Ok(award);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get award by id {id}: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddAward([FromBody] AddAwardDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var addedAward = await _awardService.Add(dto);
				return Ok(addedAward);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add award: {ex.Message}");
				return StatusCode(500, "Error adding data to the database.");
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateAward([FromBody] UpdateAwardDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{


				var updatedAward = await _awardService.Update(dto);
				return Ok(updatedAward);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update award: {ex.Message}");
				return BadRequest(new { Message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAward(int id)
		{
			try
			{
				var deletedAward = await _awardService.Delete(id);
				if (deletedAward == null) return NotFound("Award not found.");

				return Ok(deletedAward);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete award: {ex.Message}");
				return StatusCode(500, "Error deleting data from the database.");
			}
		}
	}
}
