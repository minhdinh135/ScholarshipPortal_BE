using Domain.DTOs.Payment;

namespace Application.Interfaces.IServices;

public interface IPaymentService
{
    Task<string> CreateInvoice(InvoiceRequest invoiceRequest);

    Task TransferMoney(TransferRequest transferRequest);
}