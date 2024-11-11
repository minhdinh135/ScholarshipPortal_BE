using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Payment;
using Domain.Entities;

namespace Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IMapper _mapper;
    private readonly IStripeService _stripeService;
    private readonly IAccountService _accountService;
    private readonly ITransactionRepository _transactionRepository;

    public PaymentService(IMapper mapper, IStripeService stripeService, IAccountService accountService,
        ITransactionRepository transactionRepository)
    {
        _mapper = mapper;
        _stripeService = stripeService;
        _accountService = accountService;
        _transactionRepository = transactionRepository;
    }

    public async Task<string> CreateInvoice(InvoiceRequest invoiceRequest)
    {
        try
        {
            var account = await _accountService.GetWalletByUserId(invoiceRequest.AccountId);
            var invoiceUrl = await _stripeService.CreateInvoice(account.StripeCustomerId, invoiceRequest.Amount,
                new Dictionary<string, string>()
                {
                    { "accountId", invoiceRequest.AccountId.ToString() }
                });

            return invoiceUrl;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task TransferMoney(TransferRequest transferRequest)
    {
        try
        {
            var senderWallet = await _accountService.GetWalletByUserId(transferRequest.SenderId);
            var receiverWallet = await _accountService.GetWalletByUserId(transferRequest.ReceiverId);

            if (senderWallet.Balance < transferRequest.Amount)
            {
                throw new ServiceException("Sender wallet's balance is less than the transfer amount");
            }

            senderWallet.Balance -= transferRequest.Amount;
            receiverWallet.Balance += transferRequest.Amount;

            var createdTransaction = _mapper.Map<Transaction>(transferRequest);
            createdTransaction.WalletSenderId = senderWallet.Id;
            createdTransaction.WalletReceiverId = receiverWallet.Id;
            await _transactionRepository.Add(createdTransaction);

            await _accountService.UpdateWalletBalance(transferRequest.SenderId, senderWallet.Balance);
            await _accountService.UpdateWalletBalance(transferRequest.ReceiverId, receiverWallet.Balance);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}