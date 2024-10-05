using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Domain.DTOs.University;
using Microsoft.EntityFrameworkCore;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UniversitiesController : ControllerBase
	{
		private readonly IGenericRepository<University> _universityRepo;
		private readonly ILogger<UniversitiesController> _logger;

		public UniversitiesController(IGenericRepository<University> universityRepo, ILogger<UniversitiesController> logger)
		{
			_universityRepo = universityRepo;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUniversities()
		{
			try
			{
				var universities = await _universityRepo.GetAll(x => x.Include(u => u.Country));
				return Ok(universities);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all universities: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUniversityById(int id)
		{
			try
			{
				var university = await _universityRepo.GetById(id);
				if (university == null)
				{
					return NotFound("University not found.");
				}
				return Ok(university);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get university by id {id}: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddUniversity([FromBody] UniversityDTO universityDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var newUniversity = new University
				{
					Name = universityDto.Name,
					Description = universityDto.Description,
					City = universityDto.City,
					CountryId = universityDto.CountryId
				};

				var addedUniversity = await _universityRepo.Add(newUniversity);
				return Ok(addedUniversity);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add university: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database.");
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUniversity(int id, [FromBody] UniversityDTO universityDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var university = await _universityRepo.GetById(id);
				if (university == null)
					return NotFound("University not found.");

				university.Name = universityDto.Name;
				university.Description = universityDto.Description;
				university.City = universityDto.City;
				university.CountryId = universityDto.CountryId;

				var updatedUniversity = await _universityRepo.Update(university);
				return Ok(updatedUniversity);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update university: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUniversity(int id)
		{
			try
			{
				var deletedUniversity = await _universityRepo.DeleteById(id);
				if (deletedUniversity == null)
					return NotFound("University not found.");

				return Ok(deletedUniversity);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete university: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
			}
		}
	}
}
