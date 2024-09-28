namespace Application.ExternalService;
public class PasswordService{
  //Generate random password 10 chars
  public static string GeneratePassword(){
    return BCrypt.Net.BCrypt.GenerateSalt(10);
  }
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
