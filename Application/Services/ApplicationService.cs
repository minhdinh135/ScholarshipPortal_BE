using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Application;

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

		public async Task<Domain.Entities.Application> Add(ApplicationAddDTO dto)
		{
			var entity = _mapper.Map<Domain.Entities.Application>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<Domain.Entities.Application> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return entity;
		}

		public async Task<Domain.Entities.Application> Get(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<IEnumerable<Domain.Entities.Application>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<Domain.Entities.Application> Update(ApplicationUpdateDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Application not found.");
			var entity = _mapper.Map<Domain.Entities.Application>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
}
