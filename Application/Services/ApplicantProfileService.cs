using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.ApplicantProfile;
using Domain.Entities;

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
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return entity;
		}

		public async Task<ApplicantProfile> Get(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<IEnumerable<ApplicantProfileDTO>> GetAll()
		{
			var entities = await _repository.GetAll();
			return _mapper.Map<IEnumerable<ApplicantProfileDTO>>(entities);
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
