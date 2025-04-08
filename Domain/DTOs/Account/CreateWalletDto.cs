namespace Domain.DTOs.Account;

public class CreateWalletDto
{
    public string BankAccountName { get; set; }

    public string BankAccountNumber { get; set; }

    public decimal Balance { get; set; }
}