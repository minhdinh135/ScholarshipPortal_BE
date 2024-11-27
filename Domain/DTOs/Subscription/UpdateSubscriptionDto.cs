namespace Domain.DTOs.Subscription;

public class UpdateSubscriptionDto
{
    public string Description { get; set; }

    public decimal Amount { get; set; }

    public int NumberOfServices { get; set; }

    public int ValidMonths { get; set; }
}