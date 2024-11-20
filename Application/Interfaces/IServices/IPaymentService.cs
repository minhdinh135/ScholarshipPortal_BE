using Domain.DTOs.Payment;
using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IPaymentService
{
    Task<CheckoutSessionResponse> CreateCheckoutSession(CheckoutSessionRequest checkoutSessionRequest);
    Task AddTransaction(AddTransactionDto addTransactionDto);

    Task TransferMoney(TransferRequest transferRequest);
	Task<List<Transaction>> GetTransactionsByWalletSenderIdAsync(int walletSenderId);
	Task<List<Transaction>> GetTransactionsByWalletUserIdAsync(int walletUserId);
	Task<List<Transaction>> GetAllTransactionsAsync();
}