namespace Domain.DTOs.Payment;

public class CheckoutSessionResponse
{
    public string SessionUrl { get; set; }
    // public string ClientSecret { get; set; }
    
    public string? PublishableKey { get; set; }
}