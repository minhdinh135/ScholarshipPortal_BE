using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.ReviewMilestone;
using Domain.Entities;

namespace Application.Services;

public class ReviewMilestoneService : IReviewMilestoneService
{
    private readonly IMapper _mapper;
    private readonly IReviewMilestoneRepository _reviewMilestoneRepository;

    public ReviewMilestoneService(IMapper mapper, IReviewMilestoneRepository reviewMilestoneRepository)
    {
        _mapper = mapper;
        _reviewMilestoneRepository = reviewMilestoneRepository;
    }

    public async Task<IEnumerable<ReviewMilestoneDto>> GetAll()
    {
        var requests = await _reviewMilestoneRepository.GetAll();

        return _mapper.Map<IEnumerable<ReviewMilestoneDto>>(requests);
    }

    public async Task<ReviewMilestoneDto> GetById(int id)
    {
        var request = await _reviewMilestoneRepository.GetById(id);
        if (request == null)
            throw new ServiceException($"Request with id:{id} is not found", new NotFoundException());

        return _mapper.Map<ReviewMilestoneDto>(request);
    }


	public async Task<ReviewMilestoneDto> CreateReviewMilestone(AddReviewMilestoneDto dto)
    {
        try
        {
            var request = _mapper.Map<ReviewMilestone>(dto);
            var addedRequest = await _reviewMilestoneRepository.Add(request);

            return _mapper.Map<ReviewMilestoneDto>(addedRequest);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<ReviewMilestoneDto> UpdateReviewMilestone(int id, UpdateReviewMilestoneDto dto)
    {
        try
        {
            var exisingRequest = await _reviewMilestoneRepository.GetById(id);
            if (exisingRequest == null)
                throw new ServiceException($"Request with id:{id} is not found", new NotFoundException());

            _mapper.Map(dto, exisingRequest);

            var updatedRequest = await _reviewMilestoneRepository.Update(exisingRequest);

            return _mapper.Map<ReviewMilestoneDto>(updatedRequest);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

}
