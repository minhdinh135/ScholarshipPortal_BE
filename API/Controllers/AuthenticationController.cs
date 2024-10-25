using Application.Interfaces.IServices;
using Domain.DTOs.Authentication;
using Infrastructure.ExternalServices.Google;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenService _jwtService;
    private readonly IAccountService _accountService;
    private readonly IRoleService _roleService;
    private readonly IAuthService _authService;
    private readonly INotificationService _notificationService;
    private static readonly Dictionary<string, string> _otpStore = new();
    private static readonly Random _random = new();
    private readonly IPasswordService _passwordService;
    private readonly IEmailService _emailService;

    private readonly IConfiguration _configuration;
    private readonly GoogleService _googleService;

    public AuthenticationController(ITokenService jwtService,
        IAccountService userService,
        IRoleService roleService,
        IConfiguration configuration,
        IAuthService authService,
        INotificationService notificationService,
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
        _notificationService = notificationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto login)
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
             var (jwt, isNewUser) = await _authService.GoogleAuth(userInfo);
             return Redirect("http://localhost:5173/login-google?result=success&isNewUser=" + isNewUser + "&jwt=" +
                             jwt.Token);
             //return Ok(jwt);
         }
         catch (Exception ex)
         {
             return Redirect("http://localhost:5173/login-google?result=fail");
         //    return BadRequest(new { Message = ex.Message });
         }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto register)
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
}
