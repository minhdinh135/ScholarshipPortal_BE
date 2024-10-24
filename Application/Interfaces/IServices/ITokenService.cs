using Domain.DTOs.Account;
using Domain.DTOs.Authentication;
using Microsoft.Extensions.Configuration;

namespace Application.Interfaces.IServices;

public interface ITokenService
{
    JwtDto CreateToken(IConfiguration config, AccountDto account, string? role);
}