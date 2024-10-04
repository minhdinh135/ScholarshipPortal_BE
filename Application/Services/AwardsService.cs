using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class AwardsService : IAwardsService
	{
		private readonly IGenericRepository<Award> _repository;
		private readonly IMapper _mapper;

		public AwardsService(IGenericRepository<Award> repository, IMapper mapper)
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
			var entity = await _repository.Get(id);
			if (entity == null) return null;
			await _repository.Delete(id);
			return entity;
		}

		public async Task<Award> Get(int id)
		{
			return await _repository.Get(id);
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
