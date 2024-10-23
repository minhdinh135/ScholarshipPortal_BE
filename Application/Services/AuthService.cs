using Application.Helper;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Account;
using Domain.DTOs.Authentication;
using Domain.DTOs.Google;
using Domain.DTOs.Role;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;
    private readonly IGenericService<Account, AccountAddDTO, AccountUpdateDTO> _userService;
    private readonly IGenericService<Role, RoleAddDTO, RoleUpdateDTO> _roleService;

    private readonly IConfiguration _configuration;

    public AuthService(ITokenService tokenService,
        IPasswordService passwordService,
        IGenericService<Account, AccountAddDTO, AccountUpdateDTO> userService,
        IGenericService<Role, RoleAddDTO, RoleUpdateDTO> roleService,
        IConfiguration configuration)
    {
        _tokenService = tokenService;
        _passwordService = passwordService;
        _userService = userService;
        _roleService = roleService;
        _configuration = configuration;
    }

    public async Task<JwtDTO> Login(LoginDTO login)
    {
        if (!ErrorHandler.Validate(login, out var validationResults))
        {
            var error = ErrorHandler.GetErrorMessage(validationResults);
            throw new Exception(error);
        }

        var users = await _userService.GetAll();
        var userLogin = users.Where(u => u.Username == login.Email || u.Email == login.Email).FirstOrDefault();
        if (userLogin == null)
        {
            throw new Exception("Email or User name not found");
        }

        var roles = await _roleService.GetAll();
        roles = roles.Where(r => r.Id == userLogin.RoleId).ToList();
        if(roles.Count() == 0) throw new Exception("Account doesn't have any role");
        if (_passwordService.VerifyPassword(login.Password, userLogin.HashedPassword))
        {
            if (roles.Any(r => r.Name == RoleEnum.ADMIN))
            {
                JwtDTO token = _tokenService.CreateToken(_configuration, userLogin, RoleEnum.ADMIN);
                return token;
            }
            else if (roles.Any(r => r.Name == RoleEnum.APPLICANT))
            {
                JwtDTO token = _tokenService.CreateToken(_configuration, userLogin, RoleEnum.APPLICANT);
                return token;
            }
            else if (roles.Any(r => r.Name == RoleEnum.FUNDER))
            {
                JwtDTO token = _tokenService.CreateToken(_configuration, userLogin, RoleEnum.FUNDER);
                return token;
            }
            else if (roles.Any(r => r.Name == RoleEnum.PROVIDER))
            {
                JwtDTO token = _tokenService.CreateToken(_configuration, userLogin, RoleEnum.PROVIDER);
                return token;
            }
        }

        throw new Exception("Wrong password");
    }

    public async Task<JwtDTO> Register(RegisterDTO register, string role = RoleEnum.APPLICANT)
    {
        if (!ErrorHandler.Validate(register, out var validationResults))
        {
            var error = ErrorHandler.GetErrorMessage(validationResults);
            throw new Exception(error);
        }

        //check if email exist
        var users = await _userService.GetAll();
        var existEmail = users.Where(u => u.Email == register.Email).ToList();
        if (existEmail.Count() > 0)
            throw new Exception("Email has already been registered to system");
        //check if user exist
        /*var existUsername = users.Where(u => u.Username == register.Username).ToList();
        if (existUsername.Count() > 0)
            throw new Exception("Username has already been registered to system");*/
        //check if phone exist
        var existPhoneNumber = users.Where(u => u.PhoneNumber == register.PhoneNumber).ToList();
        if (existPhoneNumber.Count() > 0)
            throw new Exception("Phone number has already been registered to system");
        //check if role exist
        var roles = await _roleService.GetAll();
        roles = roles.Where(r => r.Name == role).ToList();
        if (roles.Count() == 0)
        {
            await _roleService.Add(new RoleAddDTO
            {
                Name = role,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = StatusEnum.ACTIVE
            });
        }

        //Get roles
        roles = await _roleService.GetAll();
        roles = roles.Where(r => r.Name == role).ToList();

        var userDTO = new AccountAddDTO
        {
            Username = register.Username,
            PhoneNumber = register.PhoneNumber,
            Email = register.Email,
            HashedPassword = _passwordService.HashPassword(register.Password),
            Address = register.Address,
            AvatarUrl = register.Avatar,
            LoginWithGoogle = false,
            RoleId = roles.FirstOrDefault()?.Id,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Status = StatusEnum.ACTIVE
        };
        var user = await _userService.Add(userDTO);
        JwtDTO token = _tokenService.CreateToken(_configuration, user, role);

        return token;
    }


    public async Task<(JwtDTO jwt, bool isNewUser)> GoogleAuth(UserInfo userInfo)
    {
        if (!ErrorHandler.Validate(userInfo, out var validationResults))
        {
            var error = ErrorHandler.GetErrorMessage(validationResults);
            throw new Exception(error);
        }

        //check if user exist
        var users = await _userService.GetAll();
        users = users.Where(u => u.Email == userInfo.Email).ToList();
        if (users.Count() > 0)
        {
            JwtDTO jwt = _tokenService.CreateToken(_configuration, users.FirstOrDefault(), RoleEnum.APPLICANT);
            return (jwt, false);
        }

        //check if role exist
        var roles = await _roleService.GetAll();
        roles = roles.Where(r => r.Name == RoleEnum.APPLICANT).ToList();
        if (roles.Count() == 0)
        {
            await _roleService.Add(new RoleAddDTO{
                Name = RoleEnum.APPLICANT,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = StatusEnum.ACTIVE
            });
        }

        //Get roles
        roles = await _roleService.GetAll();
        roles = roles.Where(r => r.Name == RoleEnum.APPLICANT).ToList();

        var userDTO = new AccountAddDTO{
            Username = userInfo.Name,
            PhoneNumber = "",
            Email = userInfo.Email,
            HashedPassword = _passwordService.HashPassword(_passwordService.GeneratePassword()),
            Address = "",
            AvatarUrl = userInfo.Picture,
            LoginWithGoogle = true,
            RoleId = roles.FirstOrDefault()?.Id,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Status = StatusEnum.ACTIVE
        };
        var user = await _userService.Add(userDTO);
        JwtDTO jwt1 = _tokenService.CreateToken(_configuration, user, RoleEnum.APPLICANT);

        return (jwt1, true);
    }
}
