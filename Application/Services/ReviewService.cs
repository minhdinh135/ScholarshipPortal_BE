using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Review;
using Domain.Entities;

namespace Application.Services
{
	public class ReviewService : IReviewService
	{
		private readonly IGenericRepository<ApplicationReview> _repository;
		protected readonly IMapper _mapper;
		public ReviewService(IGenericRepository<ApplicationReview> repository, IMapper mapper) {
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<ApplicationReview> Add(AddReviewDTO dto)
		{ 
			var entity = _mapper.Map<ApplicationReview>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<ApplicationReview> Delete(int keys)
		{
			var entity = await _repository.GetById(keys);
			if (entity == null) return null;
			await _repository.DeleteById(keys);
			return entity;
		}

		public async Task<ApplicationReview> Get(int keys)
		{
			var entity = await _repository.GetById(keys);
			return entity;
		}

		public async Task<IEnumerable<ApplicationReview>> GetAll()
		{
			var entities = await _repository.GetAll();
			return entities;
		}

		public async Task<ApplicationReview> Update(UpdateReviewDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Review not found.");
			var entity = _mapper.Map<ApplicationReview>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
}
