using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.Account;
using Domain.DTOs.Authentication;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route(UriConstant.ACCOUNT_BASE_URI)]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;
    private readonly IPasswordService _passwordService;
    private readonly IEmailService _emailService;
    private static readonly Dictionary<string, string> _otpStore = new();
    private static readonly Random _random = new();

    public AccountController(ILogger<AccountController> logger, IAccountService accountService,
        IPasswordService passwordService, IEmailService emailService)
    {
        _accountService = accountService;
        _logger = logger;
        _passwordService = passwordService;
        _emailService = emailService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var profiles = await _accountService.GetAll();
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
            var profile = await _accountService.GetAccount(id);
            if (profile == null) return NotFound("Account not found.");
            return Ok(profile);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get applicant profile by id {id}: {ex.Message}");
            return StatusCode(500, "Error retrieving data from the database.");
        }
    }

    [HttpGet("applied-to-scholarship/{scholarshipId}")]
    public async Task<IActionResult> GetAllAppliedToScholarship(int scholarshipId, [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = default, [FromQuery] string sortOrder = default)
    {
        try
        {
            var profile =
                await _accountService.GetAllAppliedToScholarship(scholarshipId, pageIndex, pageSize, sortBy, sortOrder);
            if (profile == null) return NotFound("Account not found.");
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get successfully", profile));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get applicant profile by id {scholarshipId}: {ex.Message}");
            return StatusCode(500, "Error retrieving data from the database.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAccount([FromBody] RegisterDto dto)
    {
        try
        {
            var addedProfile = await _accountService.AddAccount(dto);
            return Ok(addedProfile);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to add applicant profile: {ex.Message}");
            return StatusCode(500, "Error adding data to the database.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountDto dto)
    {
        try
        {
            var updatedProfile = await _accountService.UpdateAccount(id, dto);
            return Ok(updatedProfile);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update applicant profile: {ex.Message}");
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("{id}/change-password")]
    public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto model)
    {
        var users = await _accountService.GetAll();
        var user = users.FirstOrDefault(u => u.Email == model.Email);
        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }

        if (!_passwordService.VerifyPassword(model.OldPassword, user.HashedPassword))
        {
            return Unauthorized(new { Message = "Old password is incorrect" });
        }

        if (model.NewPassword.Length < 6)
        {
            return BadRequest(new { Message = "New password must be at least 6 characters long" });
        }

        user.HashedPassword = _passwordService.HashPassword(model.NewPassword);
        
        try
        {
            await _accountService.ChangePassword(id, model.NewPassword);
            await _emailService.SendEmailAsync(user.Email, "Password Changed",
                "Change password successfully. Thank you!");
        }
        catch (Exception ex)
        {
            return Ok(new { Message = "Password changed but failed to send email notification." });
        }

        return Ok(new { Message = "Password changed successfully!" });
    }

    [HttpPost("{id}/change-avatar")]
    public async Task<IActionResult> ChangeAvatar(int id, [FromForm] ChangeAvatarDTO dto)
    {
        try
        {
            var success = await _accountService.UpdateAvatar(id, dto.File);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update avatar successfully", success));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to add applicant profile: {ex.Message}");
            return StatusCode(500, "Error adding data to the database.");
        }
    }


    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
    {
        var users = await _accountService.GetAll();
        var user = users.FirstOrDefault(u => u.Email == model.Email);

        if (user == null)
        {
            return BadRequest(new { Message = "Incorrect Email!" });
        }

        var otp = _random.Next(100000, 999999).ToString();
        _otpStore[user.Email] = otp;

        await _emailService.SendEmailAsync(user.Email, "Your OTP Code", $"Your OTP code is: {otp}");

        return Ok(new { Message = "OTP has been sent to your email." });
    }

	[HttpPost("send-otp")]
	public async Task<IActionResult> SendOtp([FromBody] SendOtpDto model)
	{
		try
		{
			var accountExists = await _accountService.CheckEmailExistsAsync(model.Email);
			if (accountExists)
			{
				return BadRequest(new { Message = "This email already exists." });
			}

			var otp = _random.Next(100000, 999999).ToString();
			_otpStore[model.Email] = otp;

			await _emailService.SendEmailAsync(model.Email, "Your OTP Code", $"Your OTP code is: {otp}");

			return Ok(new { Message = "OTP has been sent to your email." });
		}
		catch (Exception ex)
		{
			_logger.LogError($"Failed to send OTP: {ex.Message}");
			return StatusCode(500, new { Message = "Error sending OTP." });
		}
	}

	[HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto model)
    {
        if (_otpStore.TryGetValue(model.Email, out var storedOtp))
        {
            if (storedOtp == model.Otp)
            {
                _otpStore.Remove(model.Email);
                return Ok(new { Message = "OTP verified. You can now change your password." });
            }
            else
            {
                return BadRequest(new { Message = "Incorrect OTP!" });
            }
        }

        return BadRequest(new { Message = "OTP not found for this email." });
    }

    [HttpPost("{id}/reset-password")]
    public async Task<IActionResult> ResetPassword(int id, [FromBody] ResetPasswordDto model)
    {
        var users = await _accountService.GetAll();
        var user = users.FirstOrDefault(u => u.Email == model.Email);

        if (user == null)
        {
            return BadRequest(new { Message = "User not found." });
        }

        user.HashedPassword = _passwordService.HashPassword(model.NewPassword);
        await _accountService.UpdateAccount(id, new UpdateAccountDto
        {
            Username = user.Username,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            HashedPassword = user.HashedPassword,
            Address = user.Address,
            AvatarUrl = user.AvatarUrl,
            RoleId = user.RoleId,
            Status = user.Status,
            //RoleName = user.RoleName,
            LoginWithGoogle = user.LoginWithGoogle,
        });

        await _emailService.SendEmailAsync(user.Email, "Password Reset Successful",
            "Your password has been reset successfully!");

        return Ok(new { Message = "Password reset successfully!" });
    }

	[HttpGet("wallets")]
	public async Task<IActionResult> GetAllWallets()
	{
		try
		{
			var wallets = await _accountService.GetAllWallets();

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all wallets successfully", wallets));
		}
		catch (ServiceException e)
		{
			return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
		}
	}


	[HttpGet("{id}/wallet")]
    public async Task<IActionResult> GetAccountWallet(int id)
    {
        try
        {
            var wallet = await _accountService.GetWalletByUserId(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get wallet successfully", wallet));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost("{id}/wallet")]
    public async Task<IActionResult> CreateWallet(int id, CreateWalletDto createWalletDto)
    {
        try
        {
            var createdWallet = await _accountService.CreateWallet(id, createWalletDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Create wallet successfully", createdWallet));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

	[HttpPut("{id}/wallet/bank-information")]
	public async Task<IActionResult> UpdateWalletBankInformation(int id, [FromBody] UpdateWalletBankInformationDto dto)
	{
		try
		{
			var updatedWallet = await _accountService.UpdateWalletBankInformation(id, dto);

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Wallet bank information updated successfully", updatedWallet));
		}
		catch (Exception ex)
		{
			_logger.LogError($"Failed to update wallet bank information for user {id}: {ex.Message}");
			return StatusCode(500, "Error updating wallet bank information.");
		}
	}

    [HttpPut("{id}/wallet")]
    public async Task<IActionResult> UpdateWalletBalance(int id, UpdateWalletBalanceDto updateWalletBalanceDto)
    {
        try
        {
            var updatedWallet = await _accountService.UpdateWalletBalance(id, updateWalletBalanceDto.Balance);
    
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update wallet balance successfully", updatedWallet));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}
