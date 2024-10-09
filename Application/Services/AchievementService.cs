using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Achievement;
using Domain.DTOs.Common;
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

		public async Task<AchievementDTO> Add(AchievementAddDTO dto)
		{
			var entity = _mapper.Map<Achievement>(dto);
			await _repository.Add(entity);
			return _mapper.Map<AchievementDTO>(entity);
		}

		public async Task<AchievementDTO> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return _mapper.Map<AchievementDTO>(entity);
		}

		public async Task<AchievementDTO> Get(int id)
		{
			var entity = await _repository.GetById(id);
      if (entity == null) return null;
      return _mapper.Map<AchievementDTO>(entity);
		}

		public async Task<IEnumerable<AchievementDTO>> GetAll()
		{
      var university = await _repository.GetAll();
      return _mapper.Map<IEnumerable<AchievementDTO>>(university);
		}

    public async Task<PaginatedList<AchievementDTO>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder)
    {
        var categories = await _repository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

        return _mapper.Map<PaginatedList<AchievementDTO>>(categories);
    }

		public async Task<AchievementDTO> Update(AchievementUpdateDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Achievement not found.");
			var entity = _mapper.Map<Achievement>(dto);
			await _repository.Update(entity);
			return _mapper.Map<AchievementDTO>(entity);
		}
	}
}
