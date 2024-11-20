using Domain.DTOs.Payment;
using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IStripeService
{
    Task<CheckoutSessionResponse> CreateCheckoutSession(string email, CheckoutSessionRequest checkoutSessionRequest);
}