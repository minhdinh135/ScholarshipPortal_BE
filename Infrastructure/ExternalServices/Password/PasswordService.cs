using Application.Interfaces.IServices;

namespace Infrastructure.ExternalServices.Password;

public class PasswordService : IPasswordService
{
    public string GeneratePassword()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(10);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}