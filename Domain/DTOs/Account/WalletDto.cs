namespace Domain.DTOs.Account;

public class WalletDto
{
    public int? Id { get; set; }
    
    public string? BankAccountName { get; set; }

    public string? BankAccountNumber { get; set; }

    public decimal Balance { get; set; }
    
    public string? StripeCustomerId { get; set; }

    public int? AccountId { get; set; }
}