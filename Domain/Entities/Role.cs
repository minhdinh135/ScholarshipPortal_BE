using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Role : BaseEntity
{
  [MaxLength(100)]
  public string Name { get; set; }

  public ICollection<Account>? Accounts { get; set; }
}
