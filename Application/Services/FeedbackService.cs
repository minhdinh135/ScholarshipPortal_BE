using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.Feedback;
using Domain.Entities;

namespace Application.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IMapper _mapper;
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackService(IMapper mapper, IFeedbackRepository feedbackRepository)
    {
        _mapper = mapper;
        _feedbackRepository = feedbackRepository;
    }

    public async Task<IEnumerable<FeedbackDto>> GetAllFeedbacks()
    {
        var feedbacks = await _feedbackRepository.GetAll();

        return _mapper.Map<IEnumerable<FeedbackDto>>(feedbacks);
    }

    public async Task<FeedbackDto> GetFeedbackById(int id)
    {
        var feedback = await _feedbackRepository.GetById(id);
        if (feedback == null)
            throw new ServiceException($"Feedback with id:{id} is not found", new NotFoundException());

        return _mapper.Map<FeedbackDto>(feedback);
    }

    public async Task<FeedbackDto> AddFeedback(AddFeedbackDto addFeedbackDto)
    {
        try
        {
            var feedback = _mapper.Map<Feedback>(addFeedbackDto);
            var addedFeedback = await _feedbackRepository.Add(feedback);

            return _mapper.Map<FeedbackDto>(feedback);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<FeedbackDto> UpdateFeedback(int id, UpdateFeedbackDto updateFeedbackDto)
    {
        try
        {
            var existingFeedback = await _feedbackRepository.GetById(id);
            if (existingFeedback == null)
                throw new ServiceException($"Feedback with id:{id} is not found", new NotFoundException());

            _mapper.Map(updateFeedbackDto, existingFeedback);

            var updatedFeedback = await _feedbackRepository.Update(existingFeedback);

            return _mapper.Map<FeedbackDto>(updatedFeedback);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}