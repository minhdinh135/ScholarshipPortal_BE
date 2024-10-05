using Domain.DTOs.Authentication;
using Domain.DTOs.Google;

namespace Application.Interfaces.IServices;

public interface IAuthService
{
    Task<JwtDTO> Login(LoginDTO loginDto);

    Task<JwtDTO> Register(RegisterDTO registerDto);

    Task<JwtDTO> GoogleAuth(UserInfo userInfo);
}