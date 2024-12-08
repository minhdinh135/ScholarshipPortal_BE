using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Subscription : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public decimal Amount { get; set; }
    
    public int NumberOfServices { get; set; }
    
    public int ValidMonths { get; set; }
    
    public ICollection<Account>? Accounts { get; set; }
}