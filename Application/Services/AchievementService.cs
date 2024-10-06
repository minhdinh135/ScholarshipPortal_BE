using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Achievement;
using Domain.Entities;

namespace Application.Services
{
	public class AchievementService : IAchievementService
	{
		private readonly IGenericRepository<Achievement> _repository;
		private readonly IMapper _mapper;

		public AchievementService(IGenericRepository<Achievement> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<Achievement> Add(AchievementAddDTO dto)
		{
			var entity = _mapper.Map<Achievement>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<Achievement> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return entity;
		}

		public async Task<Achievement> Get(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<IEnumerable<Achievement>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<Achievement> Update(AchievementUpdateDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Achievement not found.");
			var entity = _mapper.Map<Achievement>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
}
