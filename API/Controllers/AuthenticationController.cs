using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ExternalService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly IGenericService<User, UserAddDTO, UserUpdateDTO> _userService;
    private readonly IGenericService<Role, RoleAddDTO, RoleUpdateDTO> _roleService;

    private readonly IConfiguration _configuration;

    public AuthenticationController(JwtService jwtService, 
        IGenericService<User, UserAddDTO, UserUpdateDTO> userService,
        IGenericService<Role, RoleAddDTO, RoleUpdateDTO> roleService,
        IConfiguration configuration)
    {
      _jwtService = jwtService;
      _userService = userService;
      _roleService = roleService;
      _configuration = configuration;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO login)
    {
      var users = await _userService.GetAll();
      var userLogin = users.Where(u => u.Email == login.Email).FirstOrDefault();
      if(userLogin == null) return Unauthorized(new { Message = "Email not found" });
      var roles = await _roleService.GetAll();
      // roles = roles.Where(r => r.UserId == userLogin.Id).ToList();
      if(PasswordService.VerifyPassword(login.Password, userLogin.HashedPassword)){
        if(roles.Count() == 0) 
        {
          await _roleService.Add(new RoleAddDTO { 
              Name = RoleEnum.APPLICANT,
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
              Status = "Active"
          });
          JwtDTO token = JwtService.CreateJwt(_configuration, userLogin);
          return Ok(token);
        }
        else if(roles.Any(r => r.Name == RoleEnum.ADMIN))
        {
          JwtDTO token = JwtService.CreateJwt(_configuration, userLogin, RoleEnum.ADMIN);
          return Ok(token);
        }
        else if(roles.Any(r => r.Name == RoleEnum.APPLICANT))
        {
          JwtDTO token = JwtService.CreateJwt(_configuration, userLogin);
          return Ok(token);
        }
      }
      return Unauthorized(new { Message = "Wrong password" }); 
    }

    
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDTO register)
    {
      try{
        if(!ModelState.IsValid){
          var error = ErrorHandler.GetErrorMessage(ModelState);
          return BadRequest(new {Message = error});
        }
        //check if role exist
        var roles = await _roleService.GetAll();
        roles = roles.Where(r => r.Name == RoleEnum.APPLICANT).ToList();
        if(roles.Count() == 0){
          await _roleService.Add(new RoleAddDTO { 
              Name = RoleEnum.APPLICANT,
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
              Status = "Active"
          });
        }
        //Get roles
        roles = await _roleService.GetAll();
        roles = roles.Where(r => r.Name == RoleEnum.APPLICANT).ToList();

        var userDTO = new UserAddDTO {
          Email = register.Email,
          UserName = register.UserName,
          FullName = register.FullName,
          PhoneNumber = register.PhoneNumber,
          HashedPassword = PasswordService.HashPassword(register.Password),
          Address = register.Address,
          Avatar = register.Avatar,
          Gender = register.Gender,
          RoleId = roles.FirstOrDefault().Id.Value,
          CreatedAt = DateTime.Now,
          UpdatedAt = DateTime.Now,
          Status = "Active"
        };
        var user = await _userService.Add(userDTO);
        JwtDTO token = JwtService.CreateJwt(_configuration, user);
        
        return Ok(token);
      }
      catch(Exception ex){
        return BadRequest(new {Message = ex.Message});
      }
    }
}

public class ErrorHandler{
  public static string? GetErrorMessage(ModelStateDictionary modelState){
    foreach (var modelStateEntry in modelState)
    {
      var errors = modelStateEntry.Value.Errors;
      return errors.FirstOrDefault()?.ErrorMessage;
    }
    return null;
  }
}
