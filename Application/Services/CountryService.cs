using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Country;
using Domain.Entities;

namespace Application.Services
{
	public class CountryService : ICountryService
	{
		private readonly IGenericRepository<Country> _repository;
		private readonly IMapper _mapper;

		public CountryService(IGenericRepository<Country> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<Country> Add(AddCountryDTO dto)
		{
			var entity = _mapper.Map<Country>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<Country> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return entity;
		}

		public async Task<Country> Get(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<IEnumerable<Country>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<Country> Update(UpdateCountryDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Country not found.");
			var entity = _mapper.Map<Country>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
}
