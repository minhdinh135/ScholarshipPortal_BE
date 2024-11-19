namespace Domain.DTOs.Payment;

public class CheckoutSessionRequest
{
    public int? AccountSenderId { get; set; }
    
    public int? AccountReceiverId { get; set; }
    public decimal Amount { get; set; }
}