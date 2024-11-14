using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Constants;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Infrastructure.ExternalServices.Background;

public class BackgroundService : IBackgroundService
{
    private readonly ILogger<IBackgroundService> _logger;
    private readonly INotificationService _notificationService;
    private readonly IEmailService _emailService;
    private readonly IScholarshipProgramRepository _scholarshipProgramRepository;

    public BackgroundService(ILogger<IBackgroundService> logger, INotificationService notificationService,
        IEmailService emailService, IScholarshipProgramRepository scholarshipProgramRepository)
    {
        _logger = logger;
        _notificationService = notificationService;
        _emailService = emailService;
        _scholarshipProgramRepository = scholarshipProgramRepository;
    }

    public async Task ScheduleScholarshipsAfterDeadline()
    {
        _logger.LogInformation("Start Scheduling Scholarships after Deadline");
        
        try
        {
            var openScholarshipPrograms = await _scholarshipProgramRepository.GetOpenScholarshipPrograms();

            foreach (var openScholarshipProgram in openScholarshipPrograms)
            {
                TimeSpan delayedTimeSpan = (TimeSpan)(openScholarshipProgram.Deadline - DateTime.Now);
                openScholarshipProgram.Status = ScholarshipProgramStatusEnum.Reviewing.ToString();

                BackgroundJob.Schedule(() => _scholarshipProgramRepository.Update(openScholarshipProgram),
                    delayedTimeSpan);
                _logger.LogInformation(
                    $"Scholarship program with id:{openScholarshipProgram.Id} is scheduled for updating status after {delayedTimeSpan}");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}