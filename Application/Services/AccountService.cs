using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
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

    public AccountService(
        IAccountRepository accountRepository,
        IMapper mapper,
        IPasswordService passwordService,
        ICloudinaryService cloudinaryService,
        IWalletRepository walletRepository
    )
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _passwordService = passwordService;
        _cloudinaryService = cloudinaryService;
        _walletRepository = walletRepository;
    }

    public async Task<AccountDto> AddAccount(RegisterDto dto)
    {
        var entity = _mapper.Map<Account>(dto);
        entity.HashedPassword = _passwordService.HashPassword(dto.Password);

        await _accountRepository.Add(entity);

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

    public async Task<List<WalletDto>> GetAllWallets()
    {
        var wallets = await _walletRepository.GetAll();

        if (wallets == null || wallets.Count() == 0)
            throw new ServiceException("No wallets found", new NotFoundException());

        return _mapper.Map<List<WalletDto>>(wallets);
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
            var wallet = _mapper.Map<Wallet>(createWalletDto);
            wallet.AccountId = id;

            var createdWallet = await _walletRepository.Add(wallet);

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

        try
        {
            existingWallet.Balance = balance;
            var updatedWallet = await _walletRepository.Update(existingWallet);

            return _mapper.Map<WalletDto>(updatedWallet);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

	public async Task<bool> CheckEmailExistsAsync(string email)
	{
		var accounts = await _accountRepository.GetAll();
		return accounts.Any(a => a.Email == email);
	}

}