using Newtonsoft.Json;

namespace Domain.DTOs.Google;
public class TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}
