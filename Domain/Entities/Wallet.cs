namespace Domain.Entities;

public class Wallet : BaseEntity
{
    public string? BankAccountName { get; set; }
    
    public string? BankAccountNumber { get; set; }
    
    public decimal? Balance { get; set; }
    
    public int? AccountId { get; set; }
    
    public Account? Account { get; set; }
    
    public ICollection<Transaction>? SenderTransactions { get; set; }
    
    public ICollection<Transaction>? ReceiverTransactions { get; set; }
}