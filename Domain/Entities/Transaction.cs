namespace Domain.Entities;

public class Transaction : BaseEntity
{
    public decimal? Amount { get; set; }
    
    public string? PaymentMethod { get; set; }
    
    public string? Description { get; set; }
    
    public string? TransactionId { get; set; }
    
    public DateTime? TransactionDate { get; set; }
    
    public string? Status { get; set; }
    
    public int? WalletSenderId { get; set; }
    
    public Wallet? WalletSender { get; set; }
    
    public int? WalletReceiverId { get; set; }
    
    public Wallet? WalletReceiver { get; set; }
}