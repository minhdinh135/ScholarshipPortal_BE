using Application.Exceptions;
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

        public async Task<IEnumerable<ApplicationDto>> GetApplicationsByApplicantId(int applicantId)
        {
            var applications = await _applicationRepository.GetByApplicantId(applicantId);

            return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
        }

        public async Task<IEnumerable<ApplicationDto>> GetApplicationsByScholarshipProgramId(int scholarshipProgramId)
        {
            var applications = await _applicationRepository.GetByScholarshipProgramId(scholarshipProgramId);

            return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
        }

        public async Task<ApplicationDto> Update(int id, UpdateApplicationStatusRequest dto)
        {
            var existingApplication = await _applicationRepository.GetById(id);
            if (existingApplication == null)
                throw new ServiceException($"Application with id:{id} is not found", new NotFoundException());
            
            _mapper.Map(dto, existingApplication);
            var updatedApplication = await _applicationRepository.Update(existingApplication);
            return _mapper.Map<ApplicationDto>(updatedApplication);
        }

        public async Task<IEnumerable<Domain.Entities.Application>> GetByScholarshipId(int scholarshipId)
        {
            var entities = await _applicationRepository.GetByScholarshipId(scholarshipId);
            return entities;
        }
    }
}
