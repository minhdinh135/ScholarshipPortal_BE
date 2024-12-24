using Application.Interfaces.IServices;
using Domain.DTOs.Authentication;
using Infrastructure.ExternalServices.Google;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IGoogleService _googleService;

    public AuthenticationController(IAuthService authService,
        IGoogleService googleService)
    {
        _authService = authService;
        _googleService = googleService;
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

    [HttpGet("auth-google-mobile")]
    public async Task<IActionResult> AuthGoogleMobile()
    {
        var url = _googleService.BuildMobileGoogleOauthUrl();
        return Ok(new { Url = url });
    }

    [HttpGet("google/callback")]
    public async Task<IActionResult> Callback([FromQuery] string? code)
    {
        if (string.IsNullOrEmpty(code))
        {
            //return Redirect("http://localhost:5173/login-google?result=fail");
            //return Redirect("https://scholarship-portal-nu.vercel.app/login-google?result=fail");
            return BadRequest("Authorization code is missing.");
        }

        var token = await _googleService.ExchangeCodeForToken(code);
        var userInfo = await _googleService.GetUserInfo(token);
        try
        {
            var (jwt, isNewUser) = await _authService.GoogleAuth(userInfo);
            //return Redirect("http://localhost:5173/login-google?result=success&isNewUser=" + isNewUser + "&jwt=" +
            //   jwt.Token);
            return Redirect("https://scholarship-portal-nu.vercel.app/login-google?result=success&isNewUser=" +
                            isNewUser + "&jwt=" +
                            jwt.Token);
            //return Ok(jwt);
        }
        catch (Exception ex)
        {
            //return Redirect("http://localhost:5173/login-google?result=fail");
            return Redirect("https://scholarship-portal-nu.vercel.app/login-google?result=fail");
            //return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("google/callback/mobile")]
    public async Task<IActionResult> CallbackMobile([FromQuery] string? code)
    {
        if (string.IsNullOrEmpty(code))
        {
            //return Redirect("com.scholarship://login-google?result=fail");
            return BadRequest("Authorization code is missing.");
        }

        var token = await _googleService.ExchangeCodeForTokenMobile(code);
        var userInfo = await _googleService.GetUserInfo(token);
        try
        {
            var (jwt, isNewUser) = await _authService.GoogleAuth(userInfo);
            return Redirect("com.scholarship://login-google?result=success&isNewUser=" + isNewUser + "&jwt=" +
                            jwt.Token);
            //return Ok(jwt);
        }
        catch (Exception ex)
        {
            return Redirect("com.scholarship://login-google?result=fail");
            //return BadRequest(new { Message = ex.Message });
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