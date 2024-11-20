namespace Domain.DTOs.Payment;

public class AddTransactionDto
{
    public decimal Amount { get; set; }
    
    public string Description { get; set; }
    
    public string PaymentMethod { get; set; }
    
    public string TransactionId { get; set; }
    
    public int SenderId { get; set; }
    
    public int ReceiverId { get; set; }
}