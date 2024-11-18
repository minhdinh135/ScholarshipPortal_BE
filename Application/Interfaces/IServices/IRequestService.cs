using Domain.DTOs.Request;
using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IRequestService
{
    Task<IEnumerable<RequestDto>> GetAllRequests(RequestQueryParameters requestQueryParameters);

    Task<RequestDto> GetRequestById(int id);
	Task<IEnumerable<RequestDto>> GetRequestByApplicantId(int applicantId);

    Task<int> CreateRequest(ApplicantCreateRequestDto applicantCreateRequestDto);

    Task<int> UpdateRequestResult(int id, ProviderUpdateRequestDto providerUpdateRequestDto);
    Task<bool> HasUserRequestedService(int serviceId, int applicantId);
    Task<IEnumerable<Domain.Entities.Request>> GetByServiceId(int serviceId);
    Task<Request> GetWithApplicantAndRequestDetails(int id);
    Task<bool> CancelRequestAsync(int requestId);
}