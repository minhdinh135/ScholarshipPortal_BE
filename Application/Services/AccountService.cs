using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Account;
using Domain.Entities;

namespace Application.Services
{
	public class AccountService : IAccountsService
	{
		private readonly IGenericRepository<Account> _repository;
		private readonly IMapper _mapper;

		public AccountService(IGenericRepository<Account> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<Account> Add(AccountAddDTO dto)
		{
			var entity = _mapper.Map<Account>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<Account> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return entity;
		}

		public async Task<Account> Get(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<IEnumerable<Account>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<Account> Update(AccountUpdateDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Account not found.");
			var entity = _mapper.Map<Account>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
}
