using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.University;
using Domain.Entities;

namespace Application.Services
{
	public class UniversityService : IUniversityService
	{
		private readonly IGenericRepository<University> _repository;
		private readonly IMapper _mapper;

		public UniversityService(IGenericRepository<University> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<University> Add(AddUniversityDTO dto)
		{
			var entity = _mapper.Map<University>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<University> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return entity;
		}

		public async Task<University> Get(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<IEnumerable<University>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<University> Update(UpdateUniversityDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("University not found.");

			var entity = _mapper.Map<University>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
}
