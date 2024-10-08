using Domain.DTOs.Major;

namespace Application.Interfaces.IServices;

public interface IMajorService
{
    Task<IEnumerable<MajorDto>> GetAllMajors();
    Task<MajorDto> GetMajorById(int id);
    Task<MajorDto> CreateMajor(CreateMajorRequest createMajorRequest);
    Task<MajorDto> UpdateMajor(int id, UpdateMajorRequest updateMajorRequest);
    Task<MajorDto> DeleteMajorById(int id);
}