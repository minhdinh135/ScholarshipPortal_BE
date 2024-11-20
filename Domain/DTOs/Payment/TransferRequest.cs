namespace Domain.DTOs.Payment;

public class TransferRequest
{
    public int SenderId { get; set; }
    
    public int ReceiverId { get; set; }
    
    public decimal Amount { get; set; }
    
    public string PaymentMethod { get; set; }
    
    public string Description { get; set; }
}