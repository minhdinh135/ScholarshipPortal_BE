namespace Application.Interfaces.IServices;

public interface IStripeService
{
    Task<string> CreatePayment(string priceId);
    Task<List<object>> GetAllProducts();
}