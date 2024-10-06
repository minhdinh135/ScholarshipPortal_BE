using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Feedback;
using Domain.Entities;

namespace Application.Services
{
	public class FeedbackService : IFeedbackService
	{
		private readonly IGenericRepository<Feedback> _repository;
		private readonly IMapper _mapper;

		public FeedbackService(IGenericRepository<Feedback> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<Feedback> Add(AddFeedbackDTO dto)
		{
			var entity = _mapper.Map<Feedback>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<Feedback> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return entity;
		}

		public async Task<Feedback> Get(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<IEnumerable<Feedback>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<Feedback> Update(UpdateFeedbackDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Feedback not found.");
			var entity = _mapper.Map<Feedback>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
}
