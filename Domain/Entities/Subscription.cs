namespace Domain.Entities;

public class Subscription : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public decimal? Amount { get; set; }
    public int? NumberOfServices { get; set; }
    
    public int? ValidMonths { get; set; }
    
    public ICollection<Account>? Accounts { get; set; }
}