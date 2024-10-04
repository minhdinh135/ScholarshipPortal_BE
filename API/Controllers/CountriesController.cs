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
		private readonly ICountriesService _countryService;
		private readonly ILogger<CountriesController> _logger;

		public CountriesController(ICountriesService countryService, ILogger<CountriesController> logger)
		{
			_countryService = countryService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCountries()
		{
			try
			{
				var countries = await _countryService.GetAll();
				return Ok(countries);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all countries: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCountryById(int id)
		{
			try
			{
				var country = await _countryService.Get(id);
				if (country == null) return NotFound("Country not found.");
				return Ok(country);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get country by id {id}: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddCountry([FromBody] AddCountryDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var addedCountry = await _countryService.Add(dto);
				return Ok(addedCountry);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add country: {ex.Message}");
				return StatusCode(500, "Error adding data to the database.");
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCountry([FromBody] UpdateCountryDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var updatedCountry = await _countryService.Update(dto);
				return Ok(updatedCountry);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update country: {ex.Message}");
				return BadRequest(new {message = ex.Message});
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCountry(int id)
		{
			try
			{
				var deletedCountry = await _countryService.Delete(id);
				if (deletedCountry == null) return NotFound("Country not found.");

				return Ok(deletedCountry);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete country: {ex.Message}");
				return StatusCode(500, "Error deleting data from the database.");
			}
		}
	}
}
