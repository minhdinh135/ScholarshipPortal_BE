using Domain.DTOs.Request;
using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IRequestService
{
    Task<IEnumerable<RequestDto>> GetAllRequests(RequestQueryParameters requestQueryParameters);

    Task<RequestDto> GetRequestById(int id);

    Task<RequestDto> CreateRequest(AddRequestDto addRequestDto);

    Task<RequestDto> UpdateRequest(int id, UpdateRequestDto updateRequestDto);
    Task<bool> HasUserRequestedService(int serviceId, int applicantId);
    Task<IEnumerable<Domain.Entities.Request>> GetByServiceId(int serviceId);
    Task<Request> GetWithApplicantAndRequestDetails(int id);
    Task<bool> CancelRequestAsync(int requestId);
}