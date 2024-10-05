using Domain.DTOs.Authentication;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Interfaces.IServices;

public interface ITokenService
{
    JwtDTO CreateToken(IConfiguration config, Account account, string? role);
}