using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Domain.DTOs.University;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.IServices;
using System.Linq.Expressions;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UniversitiesController : ControllerBase
	{
		private readonly IUniversityService _universityService;
		private readonly ILogger<UniversitiesController> _logger;

		public UniversitiesController(IUniversityService universityService, ILogger<UniversitiesController> logger)
		{
			_universityService = universityService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUniversities()
		{
			try
			{
				var universities = await _universityService.GetAll();
				return Ok(universities);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all universities: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUniversityById(int id)
		{
			try
			{
				var university = await _universityService.Get(id);
				if (university == null) return NotFound("University not found.");
				return Ok(university);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get university by id {id}: {ex.Message}");
				return BadRequest(new { Message = ex.Message });
			}
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddUniversity([FromBody] AddUniversityDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var addedUniversity = await _universityService.Add(dto);
				return Ok(addedUniversity);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add university: {ex.Message}");
				return StatusCode(500, "Error adding data to the database.");
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateUniversity([FromBody] UpdateUniversityDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var updatedUniversity = await _universityService.Update(dto);
				return Ok(updatedUniversity);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update university: {ex.Message}");
				return BadRequest(new { Message = ex.Message });
				//return StatusCode(500, "Error updating data in the database.");
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUniversity(int id)
		{
			try
			{
				var deletedUniversity = await _universityService.Delete(id);
				if (deletedUniversity == null) return NotFound("University not found.");

				return Ok(deletedUniversity);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete university: {ex.Message}");
				return StatusCode(500, "Error deleting data from the database.");
			}
		}
	}
}
