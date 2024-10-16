namespace Application.Interfaces.IServices;

public interface IGeminiService
{
    Task<string> GetResponseFromGemini(string prompt);
}