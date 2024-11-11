using Domain.DTOs.Payment;
using Domain.DTOs.Transaction;
using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IPaymentService
{
    Task<string> CreateInvoice(InvoiceRequest invoiceRequest);

    Task TransferMoney(TransferRequest transferRequest);
	Task<List<Transaction>> GetTransactionsByWalletSenderIdAsync(int walletSenderId);
}