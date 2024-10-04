using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.DTOs;
using Domain.DTOs.Account;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountRepo;
		private readonly ILogger<AccountController> _logger;

		public AccountController(IAccountService accountRepo, ILogger<AccountController> logger)
		{
            _accountRepo = accountRepo;
			_logger = logger;
		}

		// Get all applicant profiles
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var accounts = await _accountRepo.GetAll();
				return Ok(accounts);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all applicant profiles: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		// Get applicant profile by ID
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var profile = await _accountRepo.Get(id);
				if (profile == null)
				{
					return NotFound("Applicant profile not found.");
				}
				return Ok(profile);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get applicant profile by id {id}: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		// Add new applicant profile
		[HttpPost("Add")]
		public async Task<IActionResult> Add([FromBody] AccountAddDTO dto)
		{
			try
			{
				var account = await _accountRepo.Add(dto);
				return Ok(account);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add applicant profile: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database.");
			}
		}

		// Update existing applicant profile
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromBody] AccountUpdateDTO dto)
		{
            try
			{
				var account = await _accountRepo.Get(dto.Id);
				if (account == null)
					return NotFound("Applicant profile not found.");

                var updatedAccount = await _accountRepo.Update(account);
				return Ok(updatedAccount);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update applicant profile: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
			}
		}

		// Delete applicant profile by ID
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var deletedAccount = await _accountRepo.Delete(id);
				if (deletedAccount == null)
					return NotFound("Applicant profile not found.");

				return Ok(deletedAccount);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete applicant profile: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
			}
		}
	}
}
