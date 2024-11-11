using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IStripeService
{
    Task<string> CreatePayment(string priceId);
    Task<string> CreateInvoice(string stripeCustomerId, decimal amount, Dictionary<string, string> metaData = default);
    Task<object> Pay(int amount);
    Task<List<object>> GetAllProducts();
    Task<string> CreateStripeCustomer(Account account, decimal balance);
    Task<object> GetCustomer(string stripeCustomerId);
    Task<string> UpdateCustomerBalance(string stripeCustomerId, decimal balance);
}