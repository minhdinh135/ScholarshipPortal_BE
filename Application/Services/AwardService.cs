using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Award;
using Domain.Entities;

namespace Application.Services
{
	public class AwardService : IAwardService
	{
		private readonly IGenericRepository<Award> _repository;
		private readonly IMapper _mapper;

		public AwardService(IGenericRepository<Award> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<Award> Add(AddAwardDTO dto)
		{
			var entity = _mapper.Map<Award>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<Award> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return entity;
		}

		public async Task<Award> Get(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<IEnumerable<Award>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<Award> Update(UpdateAwardDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Award not found.");
			var entity = _mapper.Map<Award>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
	}
