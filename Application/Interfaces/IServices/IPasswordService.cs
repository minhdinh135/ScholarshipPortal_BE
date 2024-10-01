namespace Application.Interfaces.IServices;

public interface IPasswordService
{
    public string GeneratePassword();
    public string HashPassword(string password);
    public bool VerifyPassword(string password, string hashedPassword);
}