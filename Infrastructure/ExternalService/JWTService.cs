using System.Security.Claims;
using Domain.Entities;
using Domain.DTOs;
using Domain.Constants;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.ExternalService;
public class JwtService{
  public static JwtDTO CreateJwt(IConfiguration config, User user, string role = RoleEnum.APPLICANT){
    //create claims details based on the user information
    var claims = new[] {
      new Claim(JwtRegisteredClaimNames.Sub,
          config["Jwt:Subject"]),
          new Claim(JwtRegisteredClaimNames.Jti
              , Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.Iat
              , DateTime.UtcNow.ToString()),
          new Claim("id", user.Id.ToString()),
          new Claim("email", user.Email),
          new Claim("role", role),
    };
    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(config["Jwt:Key"]));
    var signIn = new SigningCredentials(
        key, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(
        config["Jwt:Issuer"],
        config["Jwt:Audience"],
        claims,
        expires: DateTime.UtcNow.AddMinutes(10),
        signingCredentials: signIn);

    string Token = new JwtSecurityTokenHandler().WriteToken(token);
    return new JwtDTO{Token = Token};
  }
}
