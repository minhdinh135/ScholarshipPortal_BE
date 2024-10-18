using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.Account;
using Domain.DTOs.Authentication;
using Domain.DTOs.Role;
using Domain.Entities;
using Infrastructure.ExternalServices.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenService _jwtService;
    private readonly IAccountsService _accountService;
    private readonly IRoleService _roleService;
    private readonly IAuthService _authService;
	private static readonly Dictionary<string, string> _otpStore = new();
	private static readonly Random _random = new();
	private readonly IPasswordService _passwordService;
	private readonly IEmailService _emailService;

	private readonly IConfiguration _configuration;
    private readonly GoogleService _googleService;

    public AuthenticationController(ITokenService jwtService,
        IAccountsService userService,
        IRoleService roleService,
        IConfiguration configuration,
        IAuthService authService,
        IPasswordService passwordService,
        IEmailService emailService,
        GoogleService googleService)
    {
        _jwtService = jwtService;
		_accountService = userService;
        _roleService = roleService;
        _authService = authService;
        _passwordService = passwordService;
        _emailService = emailService;
        _configuration = configuration;
        _googleService = googleService;
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO login)
    {
        try
        {
            var token = await _authService.Login(login);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("test-role-admin")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleService.GetAll();
        return Ok(roles);
    }

    [Authorize(Roles = RoleEnum.APPLICANT)]
    [HttpGet("test-role-applicant")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _accountService.GetAll();
        return Ok(users);
    }

    [HttpGet("auth-google")]
    public async Task<IActionResult> AuthGoogle()
    {
        var url = _googleService.BuildGoogleOauthUrl();
        return Ok(new { Url = url });
    }

    [HttpGet("google/callback")]
    public async Task<IActionResult> Callback([FromQuery] string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            return Redirect("http://localhost:5173/login-google?result=fail");
            //return BadRequest("Authorization code is missing.");
        }

        var token = await _googleService.ExchangeCodeForToken(code);
        var userInfo = await _googleService.GetUserInfo(token);
        try
        {
            var jwt = await _authService.GoogleAuth(userInfo);
            return Redirect("http://localhost:5173/login-google?result=success&jwt=" + jwt.Token);
            //return Ok(jwt);
        }
        catch (Exception ex)
        {
            return Redirect("http://localhost:5173/login-google?result=fail");
            //return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDTO register)
    {
        try
        {
            var token = await _authService.Register(register);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("Register-admin")]
    public async Task<IActionResult> RegisterAdmin(RegisterDTO register)
    {
        try
        {
            var token = await _authService.Register(register, RoleEnum.ADMIN);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("Register-funder")]
    public async Task<IActionResult> RegisterFunder(RegisterDTO register)
    {
        try
        {
            var token = await _authService.Register(register, RoleEnum.FUNDER);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("Register-provider")]
    public async Task<IActionResult> RegisterProvider(RegisterDTO register)
    {
        try
        {
            var token = await _authService.Register(register, RoleEnum.PROVIDER);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("ChangePassword")]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(new { Message = "Invalid data" });
		}

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

		// Update password
		user.HashedPassword = _passwordService.HashPassword(model.NewPassword);
		await _accountService.Update(new AccountUpdateDTO
		{
			Id = user.Id,
			Username = user.Username,
			Email = user.Email,
			FullName = user.FullName,
			PhoneNumber = user.PhoneNumber,
			HashedPassword = user.HashedPassword,
			Address = user.Address,
			Avatar = user.Avatar,
			RoleId = user.RoleId,
			Status = user.Status,
			CreatedAt = user.CreatedAt,
			UpdatedAt = user.UpdatedAt,
		});

		try
		{
			await _emailService.SendEmailAsync(user.Email, "Password Changed", "Change password successfully. Thank you!");
		}
		catch (Exception ex)
		{
			return Ok(new { Message = "Password changed but failed to send email notification." });
		}

		return Ok(new { Message = "Password changed successfully!" });
	}

	[HttpPost("ForgotPassword")]
	public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
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

	[HttpPost("VerifyOtp")]
	public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDTO model)
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

	[HttpPost("ResetPassword")]
	public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
	{
		var users = await _accountService.GetAll();
		var user = users.FirstOrDefault(u => u.Email == model.Email);

		if (user == null)
		{
			return BadRequest(new { Message = "User not found." });
		}

		user.HashedPassword = _passwordService.HashPassword(model.NewPassword);
		await _accountService.Update(new AccountUpdateDTO
		{
			Id = user.Id,
			Username = user.Username,
			Email = user.Email,
			FullName = user.FullName,
			PhoneNumber = user.PhoneNumber,
			HashedPassword = user.HashedPassword,
			Address = user.Address,
			Avatar = user.Avatar,
			RoleId = user.RoleId,
			Status = user.Status,
			CreatedAt = user.CreatedAt,
			UpdatedAt = user.UpdatedAt,
		});

		await _emailService.SendEmailAsync(user.Email, "Password Reset Successful", "Your password has been reset successfully!");

		return Ok(new { Message = "Password reset successfully!" });
	}
}
