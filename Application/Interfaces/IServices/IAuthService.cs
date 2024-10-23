using Domain.DTOs.Authentication;

namespace Application.Interfaces.IServices;

public interface IAuthService
{
    Task<JwtDto> Login(LoginDto loginDto);

    Task<JwtDto> Register(RegisterDto registerDto);

    Task<(JwtDto jwt, bool isNewUser)> GoogleAuth(UserInfo userInfo);
}
