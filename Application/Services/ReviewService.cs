using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class ReviewService : IReviewsService
	{
		private readonly IGenericRepository<Review> _repository;
		protected readonly IMapper _mapper;
		public ReviewService(IGenericRepository<Review> repository, IMapper mapper) {
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<Review> Add(AddReviewDTO dto)
		{ 
			var entity = _mapper.Map<Review>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<Review> Delete(int keys)
		{
			var entity = await _repository.Get(keys);
			if (entity == null) return null;
			await _repository.Delete(keys);
			return entity;
		}

		public async Task<Review> Get(int keys)
		{
			var entity = await _repository.Get(keys);
			return entity;
		}

		public async Task<IEnumerable<Review>> GetAll()
		{
			var entities = await _repository.GetAll();
			return entities;
		}

		public async Task<Review> Update(UpdateReviewDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Review not found.");
			var entity = _mapper.Map<Review>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
}
