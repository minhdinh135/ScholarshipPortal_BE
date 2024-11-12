using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Account;
using Domain.DTOs.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;
    private readonly IAccountService _accountService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly AdminAccount _adminAccount;

    public AuthService(ITokenService tokenService,
        IPasswordService passwordService,
        IAccountService accountService,
        IConfiguration configuration, IMapper mapper, IOptions<AdminAccount> adminAccount)
    {
        _tokenService = tokenService;
        _passwordService = passwordService;
        _accountService = accountService;
        _configuration = configuration;
        _mapper = mapper;
        _adminAccount = adminAccount.Value;
    }

    public async Task<JwtDto> Login(LoginDto login)
    {
        if (login.Email == _adminAccount.Email && login.Password == _adminAccount.Password)
        {
            var token = _tokenService.CreateToken(_configuration, login.Email, RoleEnum.ADMIN);

            return token;
        }
        
        var users = await _accountService.GetAll();
        var userLogin = users.Where(u => u.Username == login.Email || u.Email == login.Email).FirstOrDefault();
        if (userLogin == null)
        {
            throw new Exception("Email or User name not found");
        }

        // var roles = await _roleService.GetAll();
        // roles = roles.Where(r => r.Id == userLogin.RoleId).ToList();
        // if(roles.Count() == 0) throw new Exception("Account doesn't have any role");

        if (_passwordService.VerifyPassword(login.Password, userLogin.HashedPassword))
        {
                JwtDto token = _tokenService.CreateToken(_configuration, userLogin, userLogin.RoleName);
                return token;
        }

        throw new Exception("Wrong password");
    }

    public async Task<JwtDto> Register(RegisterDto registerDto)
    {
        //check if email exist
        var users = await _accountService.GetAll();
        var existEmail = users.Where(u => u.Email == registerDto.Email).ToList();
        if (existEmail.Count() > 0)
            throw new Exception("Email has already been registered to system");
        
        //check if user exist
        var existUsername = users.Where(u => u.Username == registerDto.Username).ToList();
        if (existUsername.Count() > 0)
            throw new Exception("Username has already been registered to system");        
        
        //check if phone exist
        var existPhoneNumber = users.Where(u => u.PhoneNumber == registerDto.PhoneNumber).ToList();
        if (existPhoneNumber.Count() > 0)
            throw new Exception("Phone number has already been registered to system");
        
        var user = await _accountService.AddAccount(registerDto);
        JwtDto token = _tokenService.CreateToken(_configuration, user, user.RoleName);

        return token;
    }


    public async Task<(JwtDto jwt, bool isNewUser)> GoogleAuth(UserInfo userInfo)
    {
        //check if user exist
        var users = await _accountService.GetAll();
        users = users.Where(u => u.Email == userInfo.Email).ToList();
        if (users.Count() > 0)
        {
            JwtDto jwt = _tokenService.CreateToken(_configuration, users.FirstOrDefault(), RoleEnum.APPLICANT);
            return (jwt, false);
        }
        
        var userDto = new RegisterDto {
            Username = userInfo.Name,
            PhoneNumber = "",
            Email = userInfo.Email,
            Password = _passwordService.GeneratePassword(),
            LoginWithGoogle = true,
            Address = "",
            AvatarUrl = userInfo.Picture,
            RoleId = 5,
            Status = StatusEnum.ACTIVE
        };
        var user = await _accountService.AddAccount(userDto);
        JwtDto jwt1 = _tokenService.CreateToken(_configuration, user, RoleEnum.APPLICANT);

        return (jwt1, true);
    }
}
