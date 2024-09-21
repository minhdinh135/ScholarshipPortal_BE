namespace Domain.DTOs;
public class LoginDTO{
  public string Email { get; set; }
  public string Password { get; set; }
}

public class RegisterDTO{
  public string Email { get; set; }
  public string? PhoneNumber { get; set; }
  public string UserName { get; set; }
  public string FullName { get; set; }
  public string? Address { get; set; }
  public string Gender { get; set; }
  public string? Avatar { get; set; }
  public string Password { get; set; }
}
