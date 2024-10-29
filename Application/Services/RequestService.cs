using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Request;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<RequestDto>> GetAllRequests()
    {
        var requests = await _requestRepository.GetAll();

        return _mapper.Map<IEnumerable<RequestDto>>(requests);
    }

    public async Task<RequestDto> GetRequestById(int id)
    {
        var request = await _requestRepository.GetById(id);
        if (request == null)
            throw new ServiceException($"Request with id:{id} is not found", new NotFoundException());

        return _mapper.Map<RequestDto>(request);
    }

    public async Task<RequestDto> CreateRequest(AddRequestDto addRequestDto)
    {
        try
        {
            var request = _mapper.Map<Request>(addRequestDto);
            var addedRequest = await _requestRepository.Add(request);

            return _mapper.Map<RequestDto>(addedRequest);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<RequestDto> UpdateRequest(int id, UpdateRequestDto updateRequestDto)
    {
        try
        {
            var exisingRequest = await _requestRepository.GetById(id);
            if (exisingRequest == null)
                throw new ServiceException($"Request with id:{id} is not found", new NotFoundException());

            _mapper.Map(updateRequestDto, exisingRequest);

            var updatedRequest = await _requestRepository.Update(exisingRequest);

            return _mapper.Map<RequestDto>(updatedRequest);
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
}