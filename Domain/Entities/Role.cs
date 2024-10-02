namespace Domain.Entities;

public class Role : BaseEntity
{
  public string? Name { get; set; }

  public ICollection<Account>? Accounts { get; set; }
}
