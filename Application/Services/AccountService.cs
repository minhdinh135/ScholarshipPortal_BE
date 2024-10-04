using Application.Helper;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Account;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;
public class AccountService : IAccountService
{
    private readonly IGenericRepository<Account> _accountRepository;
    private readonly IMapper _mapper;

    public AccountService(IMapper mapper, IGenericRepository<Account> accountRepository)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<Account> Add(AccountAddDTO dto)
    {
        try{
            var entity = _mapper.Map<Account>(dto);
            await _accountRepository.Add(entity);
            return entity;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception(ErrorHandler.GetDbError(ex));
        }
    }

    public async Task<Account> Delete(int keys)
    {
        var entity = await _accountRepository.Get(keys);
        if (entity == null) return null;
        await _accountRepository.Delete(keys);
        return entity;
    }

    public async Task<Account> Get(int keys)
    { 
        var entity = await _accountRepository.Get(keys);     
        return entity;
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        var entities = await _accountRepository.GetAll();
        return entities;
    }

    public async Task<Account> Update(AccountUpdateDTO dto)
    {
        var entity = _mapper.Map<Account>(dto);
        await _accountRepository.Update(entity);
        return entity;
    }
}
