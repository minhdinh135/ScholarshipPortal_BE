namespace Application.Interfaces.IServices;

public interface IPasswordService
{
    string GeneratePassword();
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}