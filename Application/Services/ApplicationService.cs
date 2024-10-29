using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Application;
using Domain.DTOs.Common;

namespace Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public ApplicationService(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<ApplicationDto> Add(AddApplicationDto dto)
        {
            var entity = _mapper.Map<Domain.Entities.Application>(dto);
            await _applicationRepository.Add(entity);
            return _mapper.Map<ApplicationDto>(entity);
        }

        public async Task<ApplicationDto> Delete(int id)
        {
            var entity = await _applicationRepository.GetById(id);
            if (entity == null) return null;
            await _applicationRepository.DeleteById(id);
            return _mapper.Map<ApplicationDto>(entity);
        }

        public async Task<ApplicationDto> Get(int id)
        {
            var entity = await _applicationRepository.GetById(id);
            if (entity == null) return null;
            return _mapper.Map<ApplicationDto>(entity);
        }

        public async Task<IEnumerable<ApplicationDto>> GetAll()
        {
            var entities = await _applicationRepository.GetAll();
            return _mapper.Map<IEnumerable<ApplicationDto>>(entities);
        }

        public async Task<Domain.Entities.Application> GetWithDocumentsAndAccount(int applicationId)
        {
            var entities = await _applicationRepository.GetWithDocumentsAndAccount(applicationId);
            return entities;
        }

        public async Task<PaginatedList<ApplicationDto>> GetAll(int pageIndex, int pageSize, string sortBy,
            string sortOrder)
        {
            var categories = await _applicationRepository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

            return _mapper.Map<PaginatedList<ApplicationDto>>(categories);
        }

        public async Task<ApplicationDto> Update(int id, UpdateApplicationDto dto)
        {
            var university = await _applicationRepository.GetAll();
            var exist = university.Any(u => u.Id == id);
            if (!exist) throw new Exception("Application not found.");
            var entity = _mapper.Map<Domain.Entities.Application>(dto);
            await _applicationRepository.Update(entity);
            return _mapper.Map<ApplicationDto>(entity);
        }

        public async Task<IEnumerable<Domain.Entities.Application>> GetByScholarshipId(int scholarshipId)
        {
            var entities = await _applicationRepository.GetByScholarshipId(scholarshipId);
            return entities;
        }
    }
}
