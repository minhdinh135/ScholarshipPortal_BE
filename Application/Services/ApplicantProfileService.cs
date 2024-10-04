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
	public class ApplicantProfileService : IApplicantProfileService
	{
		private readonly IGenericRepository<ApplicantProfile> _repository;
		private readonly IMapper _mapper;

		public ApplicantProfileService(IGenericRepository<ApplicantProfile> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<ApplicantProfile> Add(AddApplicantProfileDTO dto)
		{
			var entity = _mapper.Map<ApplicantProfile>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<ApplicantProfile> Delete(int id)
		{
			var entity = await _repository.Get(id);
			if (entity == null) return null;
			await _repository.Delete(id);
			return entity;
		}

		public async Task<ApplicantProfile> Get(int id)
		{
			return await _repository.Get(id);
		}

		public async Task<IEnumerable<ApplicantProfile>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<ApplicantProfile> Update(UpdateApplicantProfileDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("ApplicantProfile not found.");
			var entity = _mapper.Map<ApplicantProfile>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
}
