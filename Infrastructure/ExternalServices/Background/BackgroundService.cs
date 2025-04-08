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
            var openScholarshipPrograms = await _scholarshipProgramRepository.GetAll(x => 
                    x.Include(x => x.AwardMilestones));

            foreach (var openScholarshipProgram in openScholarshipPrograms)
            {
                foreach (var milestone in openScholarshipProgram.AwardMilestones)
                {
                    milestone.ScholarshipProgram = null; // Prevent self-referencing loop
                }

                if(openScholarshipProgram.AwardMilestones.Count == 0)
                    continue;
                var isAwarding = openScholarshipProgram.AwardMilestones
                        .Where(x => x.FromDate > DateTime.Now ||
                            openScholarshipProgram.AwardMilestones
                            .Any(x => x.FromDate <= DateTime.Now && x.ToDate >= DateTime.Now))
                        .OrderBy(x => x.FromDate)
                        .FirstOrDefault();
                var isCompleted = openScholarshipProgram.AwardMilestones
                        .OrderByDescending(x => x.FromDate)
                        .FirstOrDefault();
                TimeSpan delayedTimeSpan = TimeSpan.MaxValue;
                if(isCompleted != null) {
                    if(openScholarshipProgram.Status == ScholarshipProgramStatusEnum.Completed.ToString())
                        continue;
                    delayedTimeSpan = (TimeSpan)(isCompleted.ToDate - DateTime.Now);
                    openScholarshipProgram.Status = ScholarshipProgramStatusEnum.Completed.ToString();

                    BackgroundJob.Schedule(() => UpdateScholarshipCompleted(openScholarshipProgram),
                            delayedTimeSpan);
                }
                if(isAwarding != null && delayedTimeSpan > TimeSpan.Zero) {
                    if(openScholarshipProgram.Status == ScholarshipProgramStatusEnum.Awarding.ToString())
                        continue;
                    delayedTimeSpan = (TimeSpan)(isAwarding.FromDate - DateTime.Now);
                    openScholarshipProgram.Status = ScholarshipProgramStatusEnum.Awarding.ToString();

                    BackgroundJob.Schedule(() => UpdateScholarshipAwarding(openScholarshipProgram),
                            delayedTimeSpan);
                }
                if(delayedTimeSpan > TimeSpan.Zero){
                    if(openScholarshipProgram.Status == ScholarshipProgramStatusEnum.Reviewing.ToString())
                        continue;
                    delayedTimeSpan = (TimeSpan)(openScholarshipProgram.Deadline - DateTime.Now);
                    openScholarshipProgram.Status = ScholarshipProgramStatusEnum.Reviewing.ToString();

                    BackgroundJob.Schedule(() => UpdateScholarshipReviewing(openScholarshipProgram),
                        delayedTimeSpan);
                    _logger.LogInformation(
                        $"Scholarship program with id:{openScholarshipProgram.Id} is scheduled for updating status after {delayedTimeSpan}");
                }
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
                        && (a.FromDate > awardedApplication.UpdatedAt ||
                           (a.FromDate <= DateTime.Now &&
                            a.ToDate >= DateTime.Now &&
                            a.FromDate > awardedApplication.UpdatedAt)))
                    .OrderBy(a => a.FromDate);

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

                BackgroundJob.Schedule(() => UpdateAndNotifyApplication(application.Id, ApplicationStatusEnum.NeedExtend.ToString(), needExtendApplication.extendDate),
                    delayedTimeSpan);
                _logger.LogInformation(
                    $"Application with id:{needExtendApplication.application.Id} is scheduled for updating status after {delayedTimeSpan}");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            _logger.LogError(e.StackTrace);
        }
    }

    public async Task ScheduleApplicationsRejectedInAward()
    {
        _logger.LogInformation("Start Scheduling Applications Need Reject Award");
        
        try
        {
            var needRejectApplications = await _applicationRepository.GetAll(x => x.Include(x => x.ScholarshipProgram)
                    .ThenInclude(x => x.AwardMilestones));
            needRejectApplications = needRejectApplications
                .Where(a => (a.Status == ApplicationStatusEnum.NeedExtend.ToString() ||
                            a.Status == ApplicationStatusEnum.Submitted.ToString() ||
                            a.Status == ApplicationStatusEnum.Approved.ToString()) &&
                        a.UpdatedAt > a.ScholarshipProgram.Deadline &&
                        (a.ScholarshipProgram.AwardMilestones.Any(x => x.ToDate > DateTime.Now) || 
                         a.ScholarshipProgram.AwardMilestones.Any(x => x.ToDate >= DateTime.Now &&
                             x.FromDate <= DateTime.Now && a.UpdatedAt < x.FromDate
                )));
            
            foreach (var needRejectApplication in needRejectApplications)
            {
                var awardMilestone = needRejectApplication.ScholarshipProgram.AwardMilestones
                    .Where(a => a.ToDate > needRejectApplication.UpdatedAt)
                    .OrderBy(a => a.FromDate).First();
                var updateImmediately = needRejectApplication.ScholarshipProgram.AwardMilestones
                    .Where(a => a.FromDate <= DateTime.Now && a.ToDate >= DateTime.Now &&
                        needRejectApplication.UpdatedAt < a.FromDate).FirstOrDefault();
                TimeSpan delayedTimeSpan = (TimeSpan)((updateImmediately != null ?
                    updateImmediately.ToDate : awardMilestone.ToDate) - DateTime.Now);
                var application = await _applicationRepository.GetById(needRejectApplication.Id);

                BackgroundJob.Schedule(() => UpdateApplicationRejectedAward(application.Id, ApplicationStatusEnum.Rejected.ToString(),
                    (updateImmediately != null ? updateImmediately.ToDate : awardMilestone.ToDate)),
                    delayedTimeSpan);
                _logger.LogInformation(
                    $"Application with id:{needRejectApplication.Id} is scheduled for updating status after {delayedTimeSpan}");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            _logger.LogError(e.StackTrace);
        }
    }
    
    public async Task UpdateAndNotifyApplication(int applicationId, string status, DateTime extendDate)
    {
        var awardedApplications = await _applicationRepository.GetAll(x => x.Include(x => x.ScholarshipProgram)
                .Include(x => x.Applicant));
        awardedApplications = awardedApplications
            .Where(a => a.Id == applicationId);

        var awardedApplication = awardedApplications.First();

        //var application = await _applicationRepository.GetById(applicationId);
        awardedApplication.Status = status;
        awardedApplication.UpdatedAt = extendDate.AddDays(1);

        await _applicationRepository.Update(awardedApplication);

        await _notificationService.SendNotification(awardedApplication.ApplicantId.ToString(),
                $"/funder/application/{awardedApplication.Id}",
                status,
                $"Your application to scholarship {awardedApplication.ScholarshipProgram.Name} is {status}."
        );
        try{
            await _emailService.SendEmailAsync(awardedApplication.Applicant.Email.ToString(),
                    status,
                    $"Your application to scholarship {awardedApplication.ScholarshipProgram.Name} is {status}."
           );
        }
        catch (Exception e){
            _logger.LogError(e.Message);
        }
    }

    public async Task UpdateApplicationRejectedAward(int applicationId, string status, DateTime extendDate)
    {
        var awardedApplications = await _applicationRepository.GetAll(x => x.Include(x => x.ScholarshipProgram)
                .ThenInclude(x => x.AwardMilestones)
                .Include(x => x.Applicant));
        awardedApplications = awardedApplications
            .Where(a => a.Id == applicationId);

        var awardedApplication = awardedApplications.First();

        //var application = await _applicationRepository.GetById(applicationId);
        if(awardedApplication.Status == ApplicationStatusEnum.Awarded.ToString() ||
          awardedApplication.Status == ApplicationStatusEnum.Rejected.ToString()) 
            return;
        awardedApplication.Status = status;
        awardedApplication.UpdatedAt = extendDate.AddDays(-1);

        await _applicationRepository.Update(awardedApplication);

        await _notificationService.SendNotification(awardedApplication.ApplicantId.ToString(),
                $"/funder/application/{awardedApplication.Id}",
                status,
                $"Your application to scholarship {awardedApplication.ScholarshipProgram.Name} is {status}."
        );
        try{
            await _emailService.SendEmailAsync(awardedApplication.Applicant.Email.ToString(),
                    status,
                    $"Your application to scholarship {awardedApplication.ScholarshipProgram.Name} is {status}."
           );
        }
        catch (Exception e){
            _logger.LogError(e.Message);
        }        
    }

    public async Task UpdateScholarshipReviewing(ScholarshipProgram scholarshipProgram)
    {
        var scholarship = await _scholarshipProgramRepository.GetById(scholarshipProgram.Id);
        if(scholarship.Status == ScholarshipProgramStatusEnum.Reviewing.ToString())
            return;

        scholarship.Status = ScholarshipProgramStatusEnum.Reviewing.ToString();

        await _scholarshipProgramRepository.Update(scholarshipProgram);
    }

    public async Task UpdateScholarshipAwarding(ScholarshipProgram scholarshipProgram)
    {
        var scholarship = await _scholarshipProgramRepository.GetById(scholarshipProgram.Id);
        if(scholarship.Status == ScholarshipProgramStatusEnum.Awarding.ToString())
            return;

        scholarship.Status = ScholarshipProgramStatusEnum.Awarding.ToString();
        await _scholarshipProgramRepository.Update(scholarshipProgram);
    }

    public async Task UpdateScholarshipCompleted(ScholarshipProgram scholarshipProgram)
    {
        var scholarship = await _scholarshipProgramRepository.GetById(scholarshipProgram.Id);
        if(scholarship.Status == ScholarshipProgramStatusEnum.Completed.ToString())
            return;
        scholarship.Status = ScholarshipProgramStatusEnum.Completed.ToString();
        await _scholarshipProgramRepository.Update(scholarshipProgram);
    }
}
