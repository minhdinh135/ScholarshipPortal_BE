﻿using Application.Exceptions;
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
    private readonly IGenericRepository<RequestDetail> _requestDetailRepository;

    public RequestService(IMapper mapper, IRequestRepository requestRepository,
        IGenericRepository<RequestDetail> requestDetailRepository)
    {
        _mapper = mapper;
        _requestRepository = requestRepository;
        _requestDetailRepository = requestDetailRepository;
    }

    public async Task<IEnumerable<RequestDto>> GetAllRequests()
    {
        var requests = await _requestRepository.GetAllRequests();

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
            var exisingRequest = await _requestRepository.GetRequestById(id);
            if (exisingRequest == null)
                throw new ServiceException($"Request with id:{id} is not found", new NotFoundException());

            _mapper.Map(updateRequestDto, exisingRequest);

            var updatedRequest = await _requestRepository.Update(exisingRequest);
            foreach(var requestDetail in updateRequestDto.RequestDetails)
            {
                var exisingRequestDetail = await _requestDetailRepository.GetById(requestDetail.Id);
                _mapper.Map(requestDetail, exisingRequestDetail);
                await _requestDetailRepository.Update(exisingRequestDetail);
            }

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
}
