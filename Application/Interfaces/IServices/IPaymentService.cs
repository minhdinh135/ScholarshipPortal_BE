using Domain.DTOs.Payment;
using Domain.DTOs.Transaction;
using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IPaymentService
{
    Task<string> CreateInvoice(InvoiceRequest invoiceRequest);
    Task<CheckoutSessionResponse> CreateCheckoutSession(TransferRequest transferRequest);
    Task AddTransaction(decimal amount, string paymentMethod, string description, string transactionId, int senderId, int receiverId);

    Task TransferMoney(TransferRequest transferRequest);
	Task<List<Transaction>> GetTransactionsByWalletSenderIdAsync(int walletSenderId);
	Task<List<Transaction>> GetTransactionsByWalletUserIdAsync(int walletUserId);
	Task<List<Transaction>> GetAllTransactionsAsync();
}