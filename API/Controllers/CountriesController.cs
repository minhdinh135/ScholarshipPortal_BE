using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	public class CountriesController : ControllerBase
	{
		private readonly IGenericRepository<Country> _countryRepo;
		private readonly ILogger<CountriesController> _logger;

		public CountriesController(IGenericRepository<Country> countryRepo, ILogger<CountriesController> logger)
		{
			_countryRepo = countryRepo;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCountries()
		{
			try
			{
				var countries = await _countryRepo.GetAll();
				return Ok(countries);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all countries: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCountryById(int id)
		{
			try
			{
				var country = await _countryRepo.GetById(id);
				if (country == null)
				{
					return NotFound("Country not found.");
				}
				return Ok(country);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get country by id {id}: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddCountry([FromBody] CountryDTO countryDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var newCountry = new Country
				{
					Name = countryDto.Name,
					Code = countryDto.Code
				};

				var addedCountry = await _countryRepo.Add(newCountry);
				return Ok(addedCountry);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add country: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database.");
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCountry(int id, [FromBody] CountryDTO countryDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var country = await _countryRepo.GetById(id);
				if (country == null)
					return NotFound("Country not found.");

				country.Name = countryDto.Name;
				country.Code = countryDto.Code;

				var updatedCountry = await _countryRepo.Update(country);
				return Ok(updatedCountry);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update country: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCountry(int id)
		{
			try
			{
				var deletedCountry = await _countryRepo.DeleteById(id);
				if (deletedCountry == null)
					return NotFound("Country not found.");

				return Ok(deletedCountry);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete country: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
			}
		}
	}
}
