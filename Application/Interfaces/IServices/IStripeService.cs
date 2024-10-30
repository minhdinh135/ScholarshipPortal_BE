namespace Application.Interfaces.IServices;

public interface IStripeService
{
    Task<string> CreatePayment(string priceId);
    Task<object> Pay(int amount);
    Task<List<object>> GetAllProducts();
}