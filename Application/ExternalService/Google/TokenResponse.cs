using Newtonsoft.Json;

namespace Application.ExternalService.Google;
public class TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}
