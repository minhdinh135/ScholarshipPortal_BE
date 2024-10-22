using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.DTOs.Authentication;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.ExternalServices.Token;
public class JwtService : ITokenService{
  public JwtDTO CreateToken(IConfiguration config, Account account, string role = RoleEnum.APPLICANT){
    //create claims details based on the user information
    var claims = new[] {
      new Claim(JwtRegisteredClaimNames.Sub,
          config["Jwt:Subject"]),
          new Claim(JwtRegisteredClaimNames.Jti
              , Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.Iat
              , DateTime.UtcNow.ToString()),
          new Claim("id", account.Id.ToString()),
          new Claim("username", account.Username.ToString()),
          new Claim("email", account.Email),
          new Claim("role", role),
    };
    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(config["Jwt:Key"]));
    var signIn = new SigningCredentials(
        key, SecurityAlgorithms.HmacSha256);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddDays(int.Parse(config["JWT:ExpireInDays"])),
      SigningCredentials = signIn,
      Issuer = config["JWT:Issuer"],
      Audience = config["JWT:Audience"]
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var jwt = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
    
    // return new JwtDTO{Token = tokenHandler.WriteToken(jwt)};
    return new JwtDTO(tokenHandler.WriteToken(jwt));
  }
}
