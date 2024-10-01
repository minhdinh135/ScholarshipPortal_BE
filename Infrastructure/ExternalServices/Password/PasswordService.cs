using Application.Interfaces.IServices;

namespace Infrastructure.ExternalServices.Password;

public class PasswordService : IPasswordService
{
    //Generate random password 10 chars
    public string GeneratePassword()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(10);
    }

    // Function to hash a password
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Function to check password validity
    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}