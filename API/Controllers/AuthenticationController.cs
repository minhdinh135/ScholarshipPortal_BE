using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs;
using Domain.Entities;
using Application.ExternalService;
using Application.ExternalService.Google;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.ErrorHandles;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly IGenericService<User, UserAddDTO, UserUpdateDTO> _userService;
    private readonly IGenericService<Role, RoleAddDTO, RoleUpdateDTO> _roleService;
    private readonly AuthService _authService;

    private readonly IConfiguration _configuration;
    private readonly GoogleService _googleService;

    public AuthenticationController(JwtService jwtService, 
        IGenericService<User, UserAddDTO, UserUpdateDTO> userService,
        IGenericService<Role, RoleAddDTO, RoleUpdateDTO> roleService,
        IConfiguration configuration,
        AuthService authService,
        GoogleService googleService)
    {
      _jwtService = jwtService;
      _userService = userService;
      _roleService = roleService;
      _configuration = configuration;
      _googleService = googleService;
      _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO login)
    {
      try{
        
        var token = await _authService.Login(login);
        return Ok(token);
      }
      catch(Exception ex){
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
      var users = await _userService.GetAll();
      return Ok(users);
    }

    [HttpGet("auth-google")]
    public async Task<IActionResult> AuthGoogle()
    {
      var url = _googleService.BuildGoogleOauthUrl();
      return Ok(new {Url = url});
    }

    [HttpGet("google/callback")]
    public async Task<IActionResult> Callback([FromQuery] string code)
    {
      if (string.IsNullOrEmpty(code))
      {
        return BadRequest("Authorization code is missing.");
      }
      var token = await _googleService.ExchangeCodeForToken(code);
      var userInfo = await _googleService.GetUserInfo(token);
      try{
        var jwt = await _authService.GoogleAuth(userInfo);
        return Ok(jwt);
      }
      catch(Exception ex){
        return BadRequest(new {Message = ex.Message});
      }
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDTO register)
    {
      try{
        var token = await _authService.Register(register);
        return Ok(token);
      }
      catch(Exception ex){
        return BadRequest(new {Message = ex.Message});
      }
    }
}
