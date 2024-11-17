namespace Domain.DTOs.Payment;

public class CheckoutSessionRequest
{
    public string Email { get; set; }
    
    public decimal Amount { get; set; }
}