namespace Domain.Entities;

public class Transaction : BaseEntity
{
    public decimal? Amount { get; set; }
    
    public string? PaymentMethod { get; set; }
    
    public string? Description { get; set; }
    
    public string? TransactionId { get; set; }
    
    public DateTime? TransactionDate { get; set; }
    
    public string? Status { get; set; }
    
    public int? SenderId { get; set; }
    
    public Account? Sender { get; set; }
    
    public int? ReceiverId { get; set; }
    
    public Account? Receiver { get; set; }
}