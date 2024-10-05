using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Role;
using Domain.Entities;

namespace Application.Services
{
	public class RoleService : IRoleService
	{
		private readonly IGenericRepository<Role> _repository;
		private readonly IMapper _mapper;

		public RoleService(IGenericRepository<Role> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<Role> Add(RoleAddDTO dto)
		{
			var entity = _mapper.Map<Role>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<Role> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return entity;
		}

		public async Task<Role> Get(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<IEnumerable<Role>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<Role> Update(RoleUpdateDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Role not found.");
			var entity = _mapper.Map<Role>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
}
