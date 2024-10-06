using Application.Helper;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;
public class GenericService<T, AD, UD> : IGenericService<T, AD, UD> where T : class
{
  protected readonly IGenericRepository<T> _genericRepository;
  protected readonly IMapper _mapper;

  public GenericService(IMapper mapper, IGenericRepository<T> genericRepository)
  {
    _genericRepository = genericRepository;
    _mapper = mapper;
  }

  public async Task<T> Add(AD dto)
  {
    try{
      var entity = _mapper.Map<T>(dto);
      await _genericRepository.Add(entity);
      return entity;
    }
    catch (DbUpdateException ex)
    {
      throw new Exception(ErrorHandler.GetDbError(ex));
    }
  }

  public async Task<T> Delete(params int[] keys)
  {
    var entity = await _genericRepository.GetById(keys);
    if (entity == null) return null;
    await _genericRepository.DeleteById(keys);
    return entity;
  }

  public async Task<IEnumerable<T>> GetAll()
  {
    var entities = await _genericRepository.GetAll();
    return entities;
  }

  public async Task<T> Get(params int[] keys)
  {
    var entity = await _genericRepository.GetById(keys);     
    return entity;
  }

  public async Task<T> Update(UD dto)
  {
    var entity = _mapper.Map<T>(dto);
    await _genericRepository.Update(entity);
    return entity;
  }
}
