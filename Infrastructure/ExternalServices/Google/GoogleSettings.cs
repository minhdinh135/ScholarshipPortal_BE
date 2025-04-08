namespace Infrastructure.ExternalServices.Google;

public class GoogleSettings
{
    public string ClientId { get; set; }
    
    public string ClientSecret { get; set; }
    
    public string RedirectUri { get; set; }
    
    public string RedirectMobileUri { get; set; }
}