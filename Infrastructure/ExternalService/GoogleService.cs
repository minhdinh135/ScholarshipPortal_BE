using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Infrastructure.ExternalService;
public class GoogleService
{
  private readonly string _clientId;
  private readonly string _clientSecret;
  private readonly string _redirectUri;

  public GoogleService(string clientId, string clientSecret, string redirectUri)
  {
    _clientId = clientId;
    _clientSecret = clientSecret;
    _redirectUri = redirectUri;
  }
  public string BuildGoogleOauthUrl()
  {
    return $"https://accounts.google.com/o/oauth2/v2/auth?client_id={_clientId}&scope=email%20profile%20openid&redirect_uri={_redirectUri}&response_type=code";
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

public class TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}

public class UserInfo
{
  public string Id { get; set; }

  public string Email { get; set; }

  [JsonProperty("verified_email")]
  public string VerifiedEmail { get; set; }

  public string Name { get; set; }

  [JsonProperty("given_name")]
  public string GivenName { get; set; }

  [JsonProperty("family_name")]
  public string FamilyName { get; set; }

  public string Picture { get; set; }
}
