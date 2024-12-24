using System.Net.Http.Headers;
using Application.Interfaces.IServices;
using Domain.DTOs.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.ExternalServices.Google;

public class GoogleService : IGoogleService
{
    private readonly GoogleSettings _googleSettings;

    public GoogleService(IOptions<GoogleSettings> googleSettings)
    {
        _googleSettings = googleSettings.Value;
    }

    public string BuildGoogleOauthUrl()
    {
        return
            $"https://accounts.google.com/o/oauth2/v2/auth?client_id={_googleSettings.ClientId}&scope=email%20profile%20openid&redirect_uri={_googleSettings.RedirectUri}&response_type=code";
    }

    public string BuildMobileGoogleOauthUrl()
    {
        return
            $"https://accounts.google.com/o/oauth2/v2/auth?client_id={_googleSettings.ClientId}&scope=email%20profile%20openid&redirect_uri={_googleSettings.RedirectMobileUri}&response_type=code";
    }

    public async Task<string> ExchangeCodeForToken(string authorizationCode)
    {
        using (var httpClient = new HttpClient())
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type",
                    "authorization_code"),
                new KeyValuePair<string, string>("code", authorizationCode),
                new KeyValuePair<string, string>("client_id", _googleSettings.ClientId),
                new KeyValuePair<string,
                    string>("client_secret", _googleSettings.ClientSecret),
                new KeyValuePair<string, string>("redirect_uri", _googleSettings.RedirectUri)
            });

            var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token",
                content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse
                = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
            return tokenResponse.AccessToken;
        }
    }

    public async Task<string> ExchangeCodeForTokenMobile(string authorizationCode)
    {
        using (var httpClient = new HttpClient())
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type",
                    "authorization_code"),
                new KeyValuePair<string, string>("code", authorizationCode),
                new KeyValuePair<string, string>("client_id", _googleSettings.ClientId),
                new KeyValuePair<string,
                    string>("client_secret", _googleSettings.ClientSecret),
                new KeyValuePair<string, string>("redirect_uri", _googleSettings.RedirectUri)
            });

            var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token",
                content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse
                = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
            return tokenResponse.AccessToken;
        }
    }

    public async Task<UserInfo> GetUserInfo(string accessToken)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                accessToken);

            var response = await httpClient.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(responseContent);
            return userInfo;
        }
    }
}