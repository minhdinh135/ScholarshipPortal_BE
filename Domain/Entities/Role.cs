namespace Domain.Entities;

public partial class Role : BaseEntity
{
  public string Name { get; set; }

  public List<User> Users { get; set; }
}
