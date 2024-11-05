using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Application;
using Domain.DTOs.Common;
using Domain.Entities;

namespace Application.Services
{
    public class ApplicationDocumentService : IApplicationDocumentService
    {
        private readonly IGenericRepository<ApplicationDocument> _applicationDocumentRepository;
        private readonly IMapper _mapper;

        public ApplicationDocumentService(IGenericRepository<ApplicationDocument> applicationDocumentRepository,
                IMapper mapper)
        {
            _applicationDocumentRepository = applicationDocumentRepository;
            _mapper = mapper;
        }

        public async Task<ApplicationDocumentDto> Add(AddApplicationDocumentDto dto)
        {
            var entity = _mapper.Map<ApplicationDocument>(dto);
            await _applicationDocumentRepository.Add(entity);
            return _mapper.Map<ApplicationDocumentDto>(entity);
        }

        public async Task<ApplicationDocumentDto> Delete(int id)
        {
            var entity = await _applicationDocumentRepository.GetById(id);
            if (entity == null) return null;
            await _applicationDocumentRepository.DeleteById(id);
            return _mapper.Map<ApplicationDocumentDto>(entity);
        }


        public async Task<ApplicationDocumentDto> Get(int id)
        {
            var entity = await _applicationDocumentRepository.GetById(id);
            if (entity == null) return null;
            return _mapper.Map<ApplicationDocumentDto>(entity);
        }

        public async Task<IEnumerable<ApplicationDocumentDto>> GetAll()
        {
            var entities = await _applicationDocumentRepository.GetAll();
            return _mapper.Map<IEnumerable<ApplicationDocumentDto>>(entities);
        }


        public async Task<PaginatedList<ApplicationDocumentDto>> GetAll(int pageIndex, int pageSize, string sortBy,
            string sortOrder)
        {
            var categories = await _applicationDocumentRepository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

            return _mapper.Map<PaginatedList<ApplicationDocumentDto>>(categories);
        }

        public async Task<ApplicationDocumentDto> Update(int id, UpdateApplicationDocumentDto dto)
        {
            var university = await _applicationDocumentRepository.GetAll();
            var exist = university.Any(u => u.Id == id);
            if (!exist) throw new Exception("Application document not found.");
            var entity = _mapper.Map<ApplicationDocument>(dto);
            await _applicationDocumentRepository.Update(entity);
            return _mapper.Map<ApplicationDocumentDto>(entity);
        }
        
    }
}
