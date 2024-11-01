using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.University;

namespace Application.Services;

public class UniversityService : IUniversityService
{
    private readonly IMapper _mapper;
    private readonly IUniversityRepository _universityRepository;

    public UniversityService(IMapper mapper, IUniversityRepository universityRepository)
    {
        _mapper = mapper;
        _universityRepository = universityRepository;
    }
    public async Task<IEnumerable<UniversityDto>> GetAllUniversities()
    {
        var universities = await _universityRepository.GetAllUniversities();

        return _mapper.Map<IEnumerable<UniversityDto>>(universities);
    }

    public async Task<UniversityDto> GetUniversityById(int id)
    {
        var university = await _universityRepository.GetUniversityById(id);

        if (university == null)
            throw new ServiceException($"University with id:{id} is not found", new NotFoundException());

        return _mapper.Map<UniversityDto>(university);
    }
}