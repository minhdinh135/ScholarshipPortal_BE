namespace Domain.DTOs;
public class UserAddDTO : BaseAddDTO
{
  public string UserName { get; set; }

  public string FullName { get; set; }

  public string PhoneNumber { get; set; }

  public string Email { get; set; }

  public string HashedPassword { get; set; }

  public string Address { get; set; }

  public string Avatar { get; set; }

  public string Gender { get; set; }

  public Guid RoleId { get; set; }
}

public class UserUpdateDTO : BaseUpdateDTO
{
  public string UserName { get; set; }

  public string FullName { get; set; }

  public string PhoneNumber { get; set; }

  public string Email { get; set; }

  public string HashedPassword { get; set; }

  public string Address { get; set; }

  public string Avatar { get; set; }

  public string Gender { get; set; }

  public Guid RoleId { get; set; }
}
