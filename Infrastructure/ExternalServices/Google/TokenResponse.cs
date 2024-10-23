using Newtonsoft.Json;

namespace Infrastructure.ExternalServices.Google;
public class TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}
