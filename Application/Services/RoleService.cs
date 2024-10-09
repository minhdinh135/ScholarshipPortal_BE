using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Common;
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

		public async Task<RoleDTO> Add(RoleAddDTO dto)
		{
			var entity = _mapper.Map<Role>(dto);
			await _repository.Add(entity);
			return _mapper.Map<RoleDTO>(entity);
		}

		public async Task<RoleDTO> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return _mapper.Map<RoleDTO>(entity);
		}

		public async Task<RoleDTO> Get(int id)
		{
			var entity = await _repository.GetById(id);
      if (entity == null) return null;
      return _mapper.Map<RoleDTO>(entity);
		}

		public async Task<IEnumerable<RoleDTO>> GetAll()
		{
			var university = await _repository.GetAll();
      return _mapper.Map<IEnumerable<RoleDTO>>(university);
		}

    public async Task<PaginatedList<RoleDTO>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder)
    {
        var categories = await _repository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

        return _mapper.Map<PaginatedList<RoleDTO>>(categories);
    }

		public async Task<RoleDTO> Update(RoleUpdateDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Role not found.");
			var entity = _mapper.Map<Role>(dto);
			await _repository.Update(entity);
			return _mapper.Map<RoleDTO>(entity);
		}
	}
}
