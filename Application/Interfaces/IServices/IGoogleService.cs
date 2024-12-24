using Domain.DTOs.Authentication;

namespace Application.Interfaces.IServices;

public interface IGoogleService
{
    string BuildGoogleOauthUrl();
    string BuildMobileGoogleOauthUrl();
    Task<string> ExchangeCodeForToken(string authorizationCode);
    Task<string> ExchangeCodeForTokenMobile(string authorizationCode);
    Task<UserInfo> GetUserInfo(string accessToken);
}