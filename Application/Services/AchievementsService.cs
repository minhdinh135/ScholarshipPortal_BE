using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Achievement;
using Domain.Entities;

namespace Application.Services
{
	public class AchievementsService : IAchievementsService
	{
		private readonly IGenericRepository<Achievement> _repository;
		private readonly IMapper _mapper;

		public AchievementsService(IGenericRepository<Achievement> repository, IMapper mapper)
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
			var entity = await _repository.Get(id);
			if (entity == null) return null;
			await _repository.Delete(id);
			return entity;
		}

		public async Task<Achievement> Get(int id)
		{
			return await _repository.Get(id);
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
