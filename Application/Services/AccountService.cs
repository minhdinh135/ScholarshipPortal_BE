using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Account;
using Domain.DTOs.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<AccountDTO> Add(AccountAddDTO dto)
        {
            var entity = _mapper.Map<Account>(dto);
            await _repository.Add(entity);
            return _mapper.Map<AccountDTO>(entity);
        }

        public async Task<AccountDTO> Delete(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null) return null;
            await _repository.DeleteById(id);
            return _mapper.Map<AccountDTO>(entity);
        }

        public async Task<AccountDTO> Get(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null) return null;
            return _mapper.Map<AccountDTO>(entity);
        }

        public async Task<IEnumerable<AccountDTO>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<IEnumerable<AccountDTO>>(entities);
        }


        public async Task<IEnumerable<AccountWithRoleDTO>> GetAllWithRole()
        {
            var entities = await _repository.GetAll(x => x.Include(x => x.Role));
            List<AccountWithRoleDTO> res = new();
            foreach (var acc in entities)
            {
                res.Add(new AccountWithRoleDTO
                {
                    Id = acc.Id,
                    Username = acc.Username,
                    PhoneNumber = acc.PhoneNumber,
                    Email = acc.Email,
                    HashedPassword = acc.HashedPassword,
                    Address = acc.Address,
                    Avatar = acc.AvatarUrl,
                    RoleId = acc.RoleId,
                    RoleName = acc.Role != null ? acc.Role.Name : "",
                    CreatedAt = acc.CreatedAt,
                    UpdatedAt = acc.UpdatedAt,
                    Status = acc.Status,
                });
            }

            return res;
        }

        public async Task<PaginatedList<AccountDTO>> GetAll(int pageIndex, int pageSize, string sortBy,
            string sortOrder)
        {
            var categories = await _repository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

            return _mapper.Map<PaginatedList<AccountDTO>>(categories);
        }

        public async Task<AccountDTO> Update(AccountUpdateDTO dto)
        {
            var university = await _repository.GetAll();
            var exist = university.Any(u => u.Id == dto.Id);
            if (!exist) throw new Exception("Account not found.");
            var entity = _mapper.Map<Account>(dto);
            await _repository.Update(entity);
            return _mapper.Map<AccountDTO>(entity);
        }
    }
}