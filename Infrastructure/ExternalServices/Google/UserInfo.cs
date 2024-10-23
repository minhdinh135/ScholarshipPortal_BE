using Newtonsoft.Json;

namespace Infrastructure.ExternalServices.Google;
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
