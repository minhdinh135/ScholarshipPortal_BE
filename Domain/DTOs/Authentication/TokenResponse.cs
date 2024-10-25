using Newtonsoft.Json;

namespace Domain.DTOs.Authentication;
public class TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}
