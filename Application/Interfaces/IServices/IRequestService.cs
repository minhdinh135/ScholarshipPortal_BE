using Domain.DTOs.Request;

namespace Application.Interfaces.IServices;

public interface IRequestService
{
    Task<IEnumerable<RequestDto>> GetAllRequests();

    Task<RequestDto> GetRequestById(int id);

    Task<RequestDto> CreateRequest(AddRequestDto addRequestDto);

    Task<RequestDto> UpdateRequest(int id, UpdateRequestDto updateRequestDto);
}