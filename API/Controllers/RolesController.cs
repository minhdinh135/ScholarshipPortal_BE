using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.IServices;
using Domain.DTOs;
using Domain.Entities;
using Domain.DTOs.Role;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RolesController : ControllerBase
	{
		private readonly IRolesService _roleService;
		private readonly ILogger<RolesController> _logger;

		public RolesController(IRolesService roleService, ILogger<RolesController> logger)
		{
      _roleService = roleService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var profiles = await _roleService.GetAll();
				return Ok(profiles);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var profile = await _roleService.Get(id);
				if (profile == null) return NotFound("Account not found.");
				return Ok(profile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get applicant profile by id {id}: {ex.Message}");
				return StatusCode(500, "Error retrieving data from the database.");
			}
		}

		[HttpPost("Add")]
		public async Task<IActionResult> Add([FromBody] RoleAddDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var addedProfile = await _roleService.Add(dto);
				return Ok(addedProfile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add applicant profile: {ex.Message}");
				return StatusCode(500, "Error adding data to the database.");
			}
		}

		[HttpPut]
		public async Task<IActionResult> Update( [FromBody] RoleUpdateDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var updatedProfile = await _roleService.Update(dto);
				return Ok(updatedProfile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update applicant profile: {ex.Message}");
				return BadRequest(new { Message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var deletedProfile = await _roleService.Delete(id);
				if (deletedProfile == null) return NotFound("Account not found.");

				return Ok(deletedProfile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete applicant profile: {ex.Message}");
				return StatusCode(500, "Error deleting data from the database.");
			}
		}
	}
}
