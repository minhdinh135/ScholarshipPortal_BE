using Application.Interfaces.IRepositories;
using Domain.DTOs.Application;
using Domain.DTOs.Common;

namespace Application.Interfaces.IServices
{
    public interface IApplicationDocumentService 
    {
        Task<IEnumerable<ApplicationDocumentDto>> GetAll();
        Task<PaginatedList<ApplicationDocumentDto>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
        Task<ApplicationDocumentDto> Get(int id);
        Task<ApplicationDocumentDto> Add(AddApplicationDocumentDto dto);
        Task<ApplicationDocumentDto> Update(int id, UpdateApplicationDocumentDto dto);
        Task<ApplicationDocumentDto> Delete(int id);
    }
}
