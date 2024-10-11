using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Application;
using Domain.DTOs.Common;

namespace Application.Services
{
	public class ApplicationService : IApplicationService
	{
		private readonly IGenericRepository<Domain.Entities.Application> _repository;
		private readonly IMapper _mapper;

		public ApplicationService(IGenericRepository<Domain.Entities.Application> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<ApplicationDTO> Add(ApplicationAddDTO dto)
		{
			var entity = _mapper.Map<Domain.Entities.Application>(dto);
			await _repository.Add(entity);
			return _mapper.Map<ApplicationDTO>(entity);
		}

		public async Task<ApplicationDTO> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return _mapper.Map<ApplicationDTO>(entity);
		}

		public async Task<ApplicationDTO> Get(int id)
		{
			var entity = await _repository.GetById(id);
      if (entity == null) return null;
      return _mapper.Map<ApplicationDTO>(entity);
		}

		public async Task<IEnumerable<ApplicationDTO>> GetAll()
		{
			var entities = await _repository.GetAll();
      return _mapper.Map<IEnumerable<ApplicationDTO>>(entities);
		}

    public async Task<PaginatedList<ApplicationDTO>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder)
    {
        var categories = await _repository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

        return _mapper.Map<PaginatedList<ApplicationDTO>>(categories);
    }

		public async Task<ApplicationDTO> Update(ApplicationUpdateDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Application not found.");
			var entity = _mapper.Map<Domain.Entities.Application>(dto);
			await _repository.Update(entity);
			return _mapper.Map<ApplicationDTO>(entity);
		}
	}
}
