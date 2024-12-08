using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.Entities;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.ExternalServices.Background;

public class BackgroundService : IBackgroundService
{
    private readonly ILogger<IBackgroundService> _logger;
    private readonly INotificationService _notificationService;
    private readonly IEmailService _emailService;
    private readonly IScholarshipProgramRepository _scholarshipProgramRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IGenericRepository<AwardMilestone> _awardMilestoneRepository;
    

    public BackgroundService(ILogger<IBackgroundService> logger, INotificationService notificationService,
        IEmailService emailService, IScholarshipProgramRepository scholarshipProgramRepository,
        IApplicationRepository applicationRepository, 
        IGenericRepository<AwardMilestone> awardMilestoneRepository
        )
    {
        _logger = logger;
        _notificationService = notificationService;
        _emailService = emailService;
        _scholarshipProgramRepository = scholarshipProgramRepository;
        _applicationRepository = applicationRepository;
        _awardMilestoneRepository = awardMilestoneRepository;
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

    public async Task ScheduleApplicationsNeedExtend()
    {
        _logger.LogInformation("Start Scheduling Applications Need Extend");
        
        try
        {
            var awardedApplications = await _applicationRepository.GetAll(x => x.Include(x => x.ScholarshipProgram)
                    .Include(x => x.Applicant));
            awardedApplications = awardedApplications
                .Where(a => a.Status == ApplicationStatusEnum.Awarded.ToString());

            var needExtendApplications = new List<(Domain.Entities.Application application, DateTime extendDate)>();
            foreach (var awardedApplication in awardedApplications)
            {
                var awardMilestones = await _awardMilestoneRepository.GetAll();
                awardMilestones = awardMilestones
                    .Where(a => a.ScholarshipProgramId == awardedApplication.ScholarshipProgramId
                        && a.FromDate > awardedApplication.UpdatedAt
                        && a.FromDate < DateTime.Now
                        && a.ToDate > DateTime.Now);

                if (awardMilestones.Any())
                {
                    needExtendApplications.Add((awardedApplication, 
                        awardMilestones.First().FromDate));
                }

            }
            foreach (var needExtendApplication in needExtendApplications)
            {
                TimeSpan delayedTimeSpan = (TimeSpan)(needExtendApplication.extendDate - DateTime.Now);
                var application = await _applicationRepository.GetById(needExtendApplication.application.Id);
                application.Status = ApplicationStatusEnum.NeedExtend.ToString();

                BackgroundJob.Schedule(() => UpdateAndNotifyApplication(application),
                    delayedTimeSpan);
                _logger.LogInformation(
                    $"Application with id:{needExtendApplication.application.Id} is scheduled for updating status after {delayedTimeSpan}");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            _logger.LogError(e.InnerException.Message);
            _logger.LogError(e.StackTrace);
        }
    }
    
    public async Task UpdateAndNotifyApplication(Domain.Entities.Application application)
    {
        var awardedApplications = await _applicationRepository.GetAll(x => x.Include(x => x.ScholarshipProgram)
                .Include(x => x.Applicant));
        awardedApplications = awardedApplications
            .Where(a => a.Id == application.Id);

        var awardedApplication = awardedApplications.First();

        await _applicationRepository.Update(application);

        await _notificationService.SendNotification(awardedApplication.ApplicantId.ToString(),
                $"/funder/application/{awardedApplication.Id}",
                "Need Extend",
                $"Your application to scholarship {awardedApplication.ScholarshipProgram.Name} is waiting for extend."
        );
        try{
        await _emailService.SendEmailAsync(awardedApplication.Applicant.Email.ToString(),
                "Need Extend",
                $"Your application to scholarship {awardedApplication.ScholarshipProgram.Name} is waiting for extend."
       );}
        catch (Exception e){
        }

    }

}
