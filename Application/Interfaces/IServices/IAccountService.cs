using Domain.DTOs.Account;
using Domain.DTOs.Authentication;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAll();
        Task<PaginatedList<AccountDto>> GetAllAppliedToScholarship(int scholarshipId, int pageIndex, int pageSize,
            string sortBy, string sortOrder);
        Task<AccountDto> GetAccount(int id);
        Task<AccountDto> AddAccount(RegisterDto dto);
        Task<AccountDto> UpdateAccount(int id, UpdateAccountDto dto);
        Task<bool> UpdateAvatar(int id, IFormFile avatar);
		Task<List<WalletDto>> GetAllWallets();
		Task<WalletDto> GetWalletByUserId(int userId);
        Task<WalletDto> CreateWallet(int id, CreateWalletDto createWalletDto);
        Task<WalletDto> UpdateWalletBalance(int userId, decimal balance);
		Task<WalletDto> UpdateWalletBankInformation(int userId, UpdateWalletBankInformationDto dto);
		Task<bool> CheckEmailExistsAsync(string email);
	}
}