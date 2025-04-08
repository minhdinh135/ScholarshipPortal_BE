namespace Domain.DTOs.Payment;

public class CheckoutSessionRequest
{
    public int SenderId { get; set; }
    
    public int ReceiverId { get; set; }
    
    public decimal Amount { get; set; }
    
    public string Description { get; set; }
}