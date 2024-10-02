using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.IServices;
using Application.Services;
using Domain.Entities;
using Domain.DTOs.Account;
using Domain.DTOs.Authentication;
using Domain.DTOs.Role;
using Microsoft.Extensions.Configuration;
using Domain.Constants;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Infrastructure.ExternalServices.Token;
using Infrastructure.ExternalServices.Password;
using Xunit.Abstractions;

namespace UnitTest;
public class AuthServiceTests
{
    private readonly AuthService _authService; // Assuming the class containing Login is named AuthService
    private readonly Mock<IGenericService<Account, AccountAddDTO, AccountUpdateDTO>> _userServiceMock;
    private readonly Mock<IGenericService<Role, RoleAddDTO, RoleUpdateDTO>> _roleServiceMock;
    private readonly IConfiguration _configuration;
    private readonly JwtService _jwtService;
    private readonly PasswordService _passwordService;
    private readonly ITestOutputHelper _output;

    public AuthServiceTests(ITestOutputHelper output)
    {
        _userServiceMock = new Mock<IGenericService<Account, AccountAddDTO, AccountUpdateDTO>>();
        _roleServiceMock = new Mock<IGenericService<Role, RoleAddDTO, RoleUpdateDTO>>();
        _jwtService = new JwtService();
        _passwordService = new PasswordService();
        _output = output;

         // Set up common data
        var users = new List<Account>
        {
            new Account { 
                Id = 0,
                Email = "user@example.com",
                HashedPassword = _passwordService.HashPassword("password123"), 
                RoleId = 0 
            },
            new Account {
                Id = 1,
                Email = "admin@example.com",
                HashedPassword = _passwordService.HashPassword("password123"),
                RoleId = 1 
            }
        };

        var roles = new List<Role>
        {
            new Role { Id = 0, Name = RoleEnum.APPLICANT },
            new Role { Id = 1, Name = RoleEnum.ADMIN }
        };
        // Set up configuration to mimic values from appsettings.json
        var inMemorySettings = new Dictionary<string, string> {
            {"Jwt:Key", "Thisiskeyasdasdasdasdasdasdasdasdasdasdasdasda"},
            {"Jwt:Issuer", "server"},
            {"Jwt:Audience", "client"},
            {"Jwt:Subject", "token"},
            {"Jwt:ExpireInDays", "15"}
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings) // Add the in-memory settings
            .Build();

        // Injecting dependencies into AuthService constructor
        _authService = new AuthService(
            _jwtService,
            _passwordService,
            _userServiceMock.Object, 
            _roleServiceMock.Object, 
            _configuration
        );

        // Set up the mocks to return common data
        _userServiceMock.Setup(x => x.GetAll()).ReturnsAsync(users);
        _roleServiceMock.Setup(x => x.GetAll()).ReturnsAsync(roles);
    }

    [Fact]
    public async Task Login_ShouldReturnToken_WhenValidLoginWithApplicantRole()
    {
        // Arrange
        var loginDto = new LoginDTO
        (
            "user@example.com",
            "password123"
        );

        var result = await _authService.Login(loginDto);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameters = new TokenValidationParameters
        {
           ValidAudience = _configuration["Jwt:Audience"],
            ValidIssuer = _configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]
            )),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        };

        SecurityToken validatedToken;
        var principal = tokenHandler.ValidateToken(result.Token, tokenValidationParameters, out validatedToken);

        /*foreach (var claim in principal.Claims)
        {
            _output.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
        }*/
        // Assert
        Assert.NotNull(validatedToken);

        Assert.True(principal.IsInRole(RoleEnum.APPLICANT)); // Check the role claim
    }

    [Fact]
    public async Task Login_ShouldThrowException_WhenInvalidPassword()
    {
        // Arrange
        var loginDto = new LoginDTO
        (
            "user@example.com",
            "wrongpassword"
        );

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () => 
            await _authService.Login(loginDto));

        Assert.Equal("Wrong password", exception.Message);
    }

    [Fact]
    public async Task Login_ShouldThrowException_WhenEmailNotFound()
    {
        // Arrange
        var loginDto = new LoginDTO
        (
            "notfound@example.com",
            "password123"
        );

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () => 
            await _authService.Login(loginDto));

        Assert.Equal("Email not found", exception.Message);
    }

    [Fact]
    public async Task Login_ShouldAddApplicantRole_WhenNoRolesExist()
    {
        // Arrange
        var loginDto = new LoginDTO
        (
            "user@example.com",
            "password123"
        );
        
        _roleServiceMock.Setup(x => x.GetAll())
            .ReturnsAsync(new List<Role>());
        _roleServiceMock.Setup(x => x.Add(It.IsAny<RoleAddDTO>()))
            .Returns(Task.FromResult(It.IsAny<Role>())); // Simulate role addition

        // Act
        var result = await _authService.Login(loginDto);

        // Assert
        Assert.IsType<JwtDTO>(result);
        _roleServiceMock.Verify(x => x.Add(It.IsAny<RoleAddDTO>()), Times.Once);
    }
}

