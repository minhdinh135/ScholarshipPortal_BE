using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Constants;
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

    public async Task<CheckoutSessionResponse> CreateCheckoutSession(CheckoutSessionRequest checkoutSessionRequest)
    {
        try
        {
            var senderAccount = await _accountService.GetAccount(checkoutSessionRequest.SenderId);
            var checkoutResponse =
                await _stripeService.CreateCheckoutSession(senderAccount.Email, checkoutSessionRequest);

            return checkoutResponse;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task AddTransaction(AddTransactionDto addTransactionDto)
    {
        try
        {
            var senderWallet = await _accountService.GetWalletByUserId(addTransactionDto.SenderId);
            var receiverWallet = await _accountService.GetWalletByUserId(addTransactionDto.ReceiverId);

            if (senderWallet == null || receiverWallet == null)
                throw new ServiceException("Wallet has not been created");

            Transaction transaction = new Transaction
            {
                Amount = addTransactionDto.Amount,
                PaymentMethod = addTransactionDto.PaymentMethod,
                Description = addTransactionDto.Description,
                TransactionId = addTransactionDto.TransactionId,
                WalletSenderId = (int)senderWallet.Id,
                WalletReceiverId = (int)receiverWallet.Id,
                TransactionDate = DateTime.Now,
                Status = TransactionStatusEnum.Successful.ToString()
            };
            await _transactionRepository.Add(transaction);
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

            if (transferRequest.PaymentMethod == PaymentMethodEnum.Wallet.ToString())
            {
                if (senderWallet.Balance < transferRequest.Amount)
                {
                    throw new ServiceException("Sender wallet balance is less than the transfer amount");
                }

                senderWallet.Balance -= transferRequest.Amount;
                receiverWallet.Balance += transferRequest.Amount;

                await _accountService.UpdateWalletBalance(transferRequest.SenderId, senderWallet.Balance);
                await _accountService.UpdateWalletBalance(transferRequest.ReceiverId, receiverWallet.Balance);
            }

            var createdTransaction = _mapper.Map<Transaction>(transferRequest);
            createdTransaction.WalletSenderId = (int)senderWallet.Id;
            createdTransaction.WalletReceiverId = (int)receiverWallet.Id;

            await _transactionRepository.Add(createdTransaction);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<List<Transaction>> GetTransactionsByWalletSenderIdAsync(int walletSenderId)
    {
        try
        {
            return await _transactionRepository.GetTransactionsByWalletSenderIdAsync(walletSenderId);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<List<Transaction>> GetTransactionsByWalletUserIdAsync(int walletUserId)
    {
        try
        {
            return await _transactionRepository.GetTransactionsByWalletUserIdAsync(walletUserId);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<List<Transaction>> GetAllTransactionsAsync()
    {
        try
        {
            return await _transactionRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            throw new ServiceException("Error fetching transactions: " + e.Message);
        }
    }
}