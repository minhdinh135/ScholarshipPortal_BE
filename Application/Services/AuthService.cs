using Application.ExternalService;
using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs;
using Domain.Entities;
using Application.ErrorHandles;
using Microsoft.Extensions.Configuration;
using Application.ExternalService.Google;

namespace Application.Services;
public class AuthService
{
  private readonly JwtService _jwtService;
  private readonly IGenericService<User, UserAddDTO, UserUpdateDTO> _userService;
  private readonly IGenericService<Role, RoleAddDTO, RoleUpdateDTO> _roleService;

  private readonly IConfiguration _configuration;

  public AuthService(JwtService jwtService, 
      IGenericService<User, UserAddDTO, UserUpdateDTO> userService,
      IGenericService<Role, RoleAddDTO, RoleUpdateDTO> roleService,
      IConfiguration configuration)
  {
    _jwtService = jwtService;
    _userService = userService;
    _roleService = roleService;
    _configuration = configuration;
  }

  public async Task<JwtDTO> Login(LoginDTO login) {
    if (!ErrorHandler.Validate(login, out var validationResults))
    {
        var error = ErrorHandler.GetErrorMessage(validationResults);
        throw new Exception(error);
    }
    var users = await _userService.GetAll();
    var userLogin = users.Where(u => u.Email == login.Email).FirstOrDefault();
    if(userLogin == null) {
      throw new Exception("Email not found");
    }
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
        return token;
      }
      else if(roles.Any(r => r.Name == RoleEnum.ADMIN))
      {
        JwtDTO token = JwtService.CreateJwt(_configuration, userLogin, RoleEnum.ADMIN);
        return token;
      }
      else if(roles.Any(r => r.Name == RoleEnum.APPLICANT))
      {
        JwtDTO token = JwtService.CreateJwt(_configuration, userLogin);
        return token;
      }
    }
    throw new Exception("Wrong password");
  }
  
  public async Task<JwtDTO> Register(RegisterDTO register) {
    if (!ErrorHandler.Validate(register, out var validationResults))
    {
        var error = ErrorHandler.GetErrorMessage(validationResults);
        throw new Exception(error);
    }
    //check if user exist
    var users = await _userService.GetAll();
    users = users.Where(u => u.Email == register.Email).ToList();
    if(users.Count() > 0) 
      throw new Exception("Email already exist");
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

    return token;
  }


  public async Task<JwtDTO> GoogleAuth(UserInfo userInfo) {
    if (!ErrorHandler.Validate(userInfo, out var validationResults))
    {
        var error = ErrorHandler.GetErrorMessage(validationResults);
        throw new Exception(error);
    }
    //check if user exist
    var users = await _userService.GetAll();
    users = users.Where(u => u.Email == userInfo.Email).ToList();
    if(users.Count() > 0) {
      JwtDTO jwt = JwtService.CreateJwt(_configuration, users.FirstOrDefault());
      return jwt; 
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
      Email = userInfo.Email,
            UserName = userInfo.Name,
            FullName = userInfo.Name,
            PhoneNumber = "",
            HashedPassword = PasswordService.HashPassword(PasswordService.GeneratePassword()),
            Address = "",
            Avatar = userInfo.Picture,
            Gender = "",
            RoleId = roles.FirstOrDefault().Id.Value,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Status = "Active"
    };
    var user = await _userService.Add(userDTO);
    JwtDTO jwt1 = JwtService.CreateJwt(_configuration, user);

    return jwt1;
  }
}
