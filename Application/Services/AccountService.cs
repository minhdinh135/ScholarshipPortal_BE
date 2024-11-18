using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Account;
using Domain.DTOs.Authentication;
using Domain.DTOs.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IWalletRepository _walletRepository;
    private readonly IStripeService _stripeService;
    private readonly ITransactionRepository _transactionRepository;

    public AccountService(
        IAccountRepository accountRepository,
        IMapper mapper,
        IPasswordService passwordService,
        ICloudinaryService cloudinaryService,
        IWalletRepository walletRepository,
        IStripeService stripeService,
        ITransactionRepository transactionRepository
    )
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _passwordService = passwordService;
        _cloudinaryService = cloudinaryService;
        _walletRepository = walletRepository;
        _stripeService = stripeService;
        _transactionRepository = transactionRepository;
    }

    public async Task<AccountDto> AddAccount(RegisterDto dto)
    {
        var entity = _mapper.Map<Account>(dto);
        entity.HashedPassword = _passwordService.HashPassword(dto.Password);

        await _accountRepository.Add(entity);

        return _mapper.Map<AccountDto>(entity);
    }

    public async Task<AccountDto> DeleteAccount(int id)
    {
        var entity = await _accountRepository.GetById(id);
        if (entity == null) return null;
        await _accountRepository.DeleteById(id);
        return _mapper.Map<AccountDto>(entity);
    }

    public async Task<AccountDto> GetAccount(int id)
    {
        var entity = await _accountRepository.GetAccountById(id);
        if (entity == null) return null;
        return _mapper.Map<AccountDto>(entity);
    }

    public async Task<IEnumerable<AccountDto>> GetAll()
    {
        var entities = await _accountRepository.GetAllWithRole();
        return _mapper.Map<IEnumerable<AccountDto>>(entities);
    }

    public async Task<PaginatedList<AccountDto>> GetAll(int pageIndex, int pageSize, string sortBy,
        string sortOrder)
    {
        var categories = await _accountRepository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

        return _mapper.Map<PaginatedList<AccountDto>>(categories);
    }

    public async Task<PaginatedList<AccountDto>> GetAllAppliedToScholarship(int scholarshipId, int pageIndex,
        int pageSize, string sortBy,
        string sortOrder)
    {
        var accounts =
            await _accountRepository.GetAllAppliedToScholarship(scholarshipId, pageIndex, pageSize, sortBy,
                sortOrder);

        return _mapper.Map<PaginatedList<AccountDto>>(accounts);
    }

    public async Task<AccountDto> UpdateAccount(int id, UpdateAccountDto dto)
    {
        var university = await _accountRepository.GetAll();
        var exist = university.Any(u => u.Id == id);
        if (!exist) throw new Exception("Account not found.");
        var entity = _mapper.Map<Account>(dto);
        await _accountRepository.Update(entity);
        return _mapper.Map<AccountDto>(entity);
    }

    public async Task<bool> UpdateAvatar(int id, IFormFile avatar)
    {
        var uploadedAvatar = await _cloudinaryService.UploadImage(avatar);
        if (uploadedAvatar == null)
            throw new FileProcessingException("Upload avatar failed");

        var existingProfile = await _accountRepository.GetById(id);
        if (existingProfile.AvatarUrl != null)
        {
            string fileId = existingProfile.AvatarUrl.Split('/')[^1].Split('.')[0];
            try
            {
                await _cloudinaryService.DeleteImage(fileId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        existingProfile.AvatarUrl = uploadedAvatar;
        _mapper.Map<UpdateAccountDto>(existingProfile);

        var updatedAccount = await _accountRepository.Update(existingProfile);
        if (updatedAccount == null)
        {
            return false;
        }

        return true;
    }

    public async Task<WalletDto> GetWalletByUserId(int userId)
    {
        var wallet = await _walletRepository.GetWalletByUserId(userId);

        if (wallet == null)
            throw new ServiceException($"Wallet with userId:{userId} is not found", new NotFoundException());

        return _mapper.Map<WalletDto>(wallet);
    }

    public async Task<WalletDto> CreateWallet(int id, CreateWalletDto createWalletDto)
    {
        var account = await _accountRepository.GetAccountById(id);
        if (account == null)
            throw new ServiceException($"Account with id:{id} is not found",
                new NotFoundException());

        if (account.Wallet != null)
            throw new ServiceException($"Account with id:{id} already has wallet");
        
        try
        {
            var stripeCustomerId = await _stripeService.CreateStripeCustomer(account, createWalletDto.Balance);
            var wallet = _mapper.Map<Wallet>(createWalletDto);
            wallet.AccountId = id;
            wallet.StripeCustomerId = stripeCustomerId;

            var createdWallet = await _walletRepository.Add(wallet);

            Transaction transaction = new Transaction
            {
                Amount = createWalletDto.Balance,
                Description = "Wallet is created",
                PaymentMethod = PaymentMethodEnum.Card.ToString(),
                TransactionDate = DateTime.Now,
                WalletReceiverId = createdWallet.Id,
                WalletSenderId = createdWallet.Id,
                TransactionId = Guid.NewGuid().ToString("N"),
                Status = "Successful"
            };

            await _transactionRepository.Add(transaction);

            return _mapper.Map<WalletDto>(createdWallet);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<WalletDto> UpdateWalletBankInformation(int userId, UpdateWalletBankInformationDto dto)
    {
        var wallet = await _walletRepository.GetWalletByUserId(userId);
        if (wallet == null)
            throw new ServiceException($"Wallet for user with id:{userId} not found", new NotFoundException());

        wallet.BankAccountName = dto.BankAccountName ?? wallet.BankAccountName;
        wallet.BankAccountNumber = dto.BankAccountNumber ?? wallet.BankAccountNumber;

        var updatedWallet = await _walletRepository.Update(wallet);

        return _mapper.Map<WalletDto>(updatedWallet);
    }

    public async Task<WalletDto> UpdateWalletBalance(int userId, decimal balance)
    {
        var existingWallet = await _walletRepository.GetWalletByUserId(userId);
        if (existingWallet == null)
            throw new ServiceException($"Wallet with userId:{userId} is not found", new NotFoundException());

        var amount = balance - existingWallet.Balance;

        try
        {
            await _stripeService.UpdateCustomerBalance(existingWallet.StripeCustomerId, balance);
            existingWallet.Balance = balance;
            var updatedWallet = await _walletRepository.Update(existingWallet);

            Transaction transaction = new Transaction
            {
                Amount = amount,
                Description = "Wallet balance updated",
                PaymentMethod = PaymentMethodEnum.Card.ToString(),
                TransactionDate = DateTime.Now,
                WalletReceiverId = existingWallet.Id,
                WalletSenderId = existingWallet.Id,
                TransactionId = Guid.NewGuid().ToString("N"),
                Status = "Successful"
            };

            var createdTransaction = await _transactionRepository.Add(transaction);

            return _mapper.Map<WalletDto>(updatedWallet);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}