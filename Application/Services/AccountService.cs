using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Account;
using Domain.DTOs.Authentication;
using Domain.DTOs.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly ICloudinaryService _cloudinaryService;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, IPasswordService passwordService, ICloudinaryService cloudinaryService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _passwordService = passwordService;
            _cloudinaryService = cloudinaryService;
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
            var entity = await _accountRepository.GetById(id);
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
            if(existingProfile.AvatarUrl != null){
                string fileId = existingProfile.AvatarUrl.Split('/')[^1].Split('.')[0];
                try{
                    await _cloudinaryService.DeleteImage(fileId);
                }
                catch(Exception e)
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
	}
}
