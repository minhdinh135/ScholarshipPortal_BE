using Domain.DTOs.Payment;
using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IStripeService
{
    Task<CheckoutSessionResponse> CreateCheckoutSession(string email, decimal amount, int senderId, int receiverId);
    Task<string> CreateInvoice(string stripeCustomerId, decimal amount, Dictionary<string, string> metaData = default);
}