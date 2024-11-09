namespace Domain.DTOs.Payment;

public class InvoiceRequest
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
}