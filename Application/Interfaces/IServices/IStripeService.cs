﻿using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IStripeService
{
    Task<string> CreatePayment(string priceId);
    Task<string> CreateInvoice(string stripeCustomerId, decimal amount);
    Task<object> Pay(int amount);
    Task<List<object>> GetAllProducts();
    Task<string> CreateStripeCustomer(Account account, decimal balance);
    Task<string> UpdateCustomerBalance(string stripeCustomerId, decimal balance);
}