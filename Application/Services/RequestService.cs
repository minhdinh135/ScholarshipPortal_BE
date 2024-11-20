using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Request;
using Domain.Entities;

namespace Application.Services;

public class RequestService : IRequestService
{
    private readonly IMapper _mapper;
    private readonly IRequestRepository _requestRepository;

    public RequestService(IMapper mapper, IRequestRepository requestRepository)
    {
        _mapper = mapper;
        _requestRepository = requestRepository;
    }

    public async Task<IEnumerable<RequestDto>> GetAllRequests(RequestQueryParameters requestQueryParameters)
    {
        var requests = await _requestRepository.GetAllRequests(requestQueryParameters);

        return _mapper.Map<IEnumerable<RequestDto>>(requests);
    }

    public async Task<IEnumerable<RequestDto>> GetRequestByApplicantId(int applicantId)
    {
        var requests = await _requestRepository.GetRequestsByApplicantId(applicantId);

        return _mapper.Map<IEnumerable<RequestDto>>(requests);
    }

    public async Task<RequestDto> GetRequestById(int id)
    {
        var request = await _requestRepository.GetRequestById(id);
        if (request == null)
            throw new ServiceException($"Request with id:{id} is not found", new NotFoundException());

        return _mapper.Map<RequestDto>(request);
    }

    public async Task<IEnumerable<Request>> GetByServiceId(int serviceId)
    {
        return await _requestRepository.GetByServiceId(serviceId);
    }

    public async Task<int> CreateRequest(ApplicantCreateRequestDto applicantCreateRequestDto)
    {
        try
        {
            ICollection<RequestDetailFile> files = new List<RequestDetailFile>();
            List<RequestDetail> requestDetails = new List<RequestDetail>();

            applicantCreateRequestDto.RequestFileUrls.ForEach(fileUrl => files.Add(new RequestDetailFile
                { FileUrl = fileUrl, UploadedBy = RoleEnum.Applicant.ToString(), UploadDate = DateTime.Now }));

            applicantCreateRequestDto.ServiceIds.ForEach(serviceId =>
                requestDetails.Add(new RequestDetail { ServiceId = serviceId, RequestDetailFiles = files }));

            var request = new Request
            {
                Description = applicantCreateRequestDto.Description,
                RequestDate = DateTime.Now,
                Status = RequestStatusEnum.Pending.ToString(),
                ApplicantId = applicantCreateRequestDto.ApplicantId,
                RequestDetails = requestDetails
            };

            var addedRequest = await _requestRepository.Add(request);

            return addedRequest.Id;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<int> UpdateRequestResult(int id, ProviderUpdateRequestDto providerUpdateRequestDto)
    {
        try
        {
            var exisingRequest = await _requestRepository.GetRequestById(id);
            if (exisingRequest == null)
                throw new ServiceException($"Request with id:{id} is not found", new NotFoundException());

            providerUpdateRequestDto.ServiceResultDetails.ForEach(x =>
            {
                foreach (var exisingRequestDetail in exisingRequest.RequestDetails)
                {
                    if (exisingRequestDetail.ServiceId == x.ServiceId)
                    {
                        exisingRequestDetail.Comment = x.Comment;
                        exisingRequestDetail.RequestDetailFiles.Where(file => file.UploadedBy != RoleEnum.Applicant.ToString()).ToList().Clear();
                        x.RequestFileUrls.ForEach(fileUrl => exisingRequestDetail.RequestDetailFiles.Add(new RequestDetailFile
                        {
                            FileUrl = fileUrl, UploadedBy = RoleEnum.Provider.ToString(), UploadDate = DateTime.Now
                        }));
                    }
                }
            });

            await _requestRepository.Update(exisingRequest);

            return exisingRequest.Id;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<bool> HasUserRequestedService(int serviceId, int applicantId)
    {
        try
        {
            var requests = await _requestRepository.HasUserRequestedService(serviceId, applicantId);
            return requests;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<bool> CancelRequestAsync(int requestId)
    {
        return await _requestRepository.DeleteRequestAsync(requestId);
    }

    public async Task<Request> GetWithApplicantAndRequestDetails(int id)
    {
        try
        {
            var requests = await _requestRepository.GetWithApplicantAndRequestDetails(id);
            return requests;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

	public async Task<RequestDto> UpdateRequestStatusFinish(int id)
	{
		try
		{
			var existingRequest = await _requestRepository.GetRequestById(id);
			if (existingRequest == null)
				throw new ServiceException($"Request with id:{id} is not found", new NotFoundException());

			existingRequest.Status = "Finished";

			await _requestRepository.Update(existingRequest);

			return _mapper.Map<RequestDto>(existingRequest);
		}
		catch (Exception e)
		{
			throw new ServiceException(e.Message);
		}
	}
}