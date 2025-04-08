using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Transaction : BaseEntity
{
    public decimal Amount { get; set; }
    
    [MaxLength(100)]
    public string PaymentMethod { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    [MaxLength(100)]
    public string TransactionId { get; set; }
    
    public DateTime TransactionDate { get; set; }
    
    [MaxLength(100)]
    public string Status { get; set; }
    
    public int WalletSenderId { get; set; }
    
    public Wallet WalletSender { get; set; }
    
    public int WalletReceiverId { get; set; }
    
    public Wallet WalletReceiver { get; set; }
}