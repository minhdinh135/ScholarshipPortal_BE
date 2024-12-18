using System.Net.Http.Headers;
using Domain.DTOs.Authentication;
using Newtonsoft.Json;

namespace Infrastructure.ExternalServices.Google;
public class GoogleService
{
  private readonly string _clientId;
  private readonly string _clientSecret;
  private readonly string _redirectUri;
  private readonly string _redirectMobileUri;

  public GoogleService(string clientId, string clientSecret, string redirectUri,
      string redirectMobileUri)
  {
    _clientId = clientId;
    _clientSecret = clientSecret;
    _redirectUri = redirectUri;
    _redirectMobileUri = redirectMobileUri;
  }
  public string BuildGoogleOauthUrl()
  {
    return $"https://accounts.google.com/o/oauth2/v2/auth?client_id={_clientId}&scope=email%20profile%20openid&redirect_uri={_redirectUri}&response_type=code";
  }

  public string BuildMobileGoogleOauthUrl()
  {
    return $"https://accounts.google.com/o/oauth2/v2/auth?client_id={_clientId}&scope=email%20profile%20openid&redirect_uri={_redirectMobileUri}&response_type=code";
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
          new KeyValuePair<string, string>("client_id", _clientId),
          new KeyValuePair<string,  
          string>("client_secret", _clientSecret),
          new KeyValuePair<string, string>("redirect_uri", _redirectUri)
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
          new KeyValuePair<string, string>("client_id", _clientId),
          new KeyValuePair<string,  
          string>("client_secret", _clientSecret),
          new KeyValuePair<string, string>("redirect_uri", _redirectMobileUri)
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

  public async Task<Domain.DTOs.Authentication.UserInfo> GetUserInfo(string accessToken)
  {
    using (var httpClient = new HttpClient())
    {
      httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",  
          accessToken);

      var response = await httpClient.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");  

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStringAsync();
      var userInfo = JsonConvert.DeserializeObject<Domain.DTOs.Authentication.UserInfo>(responseContent);
      return userInfo;
    }
  }
}
