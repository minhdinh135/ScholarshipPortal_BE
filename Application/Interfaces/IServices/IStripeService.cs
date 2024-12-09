using Domain.DTOs.Payment;

namespace Application.Interfaces.IServices;

public interface IStripeService
{
    Task<CheckoutSessionResponse> CreateCheckoutSession(string email, CheckoutSessionRequest checkoutSessionRequest);
}