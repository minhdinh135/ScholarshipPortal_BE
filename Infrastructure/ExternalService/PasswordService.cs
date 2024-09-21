namespace Infrastructure.ExternalService;
public class PasswordService{
  // Function to hash a password
  public static string HashPassword(string password)
  {
    return BCrypt.Net.BCrypt.HashPassword(password);
  }

  // Function to check password validity
  public static bool VerifyPassword(string password, string hashedPassword)
  {
    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
  }
}
