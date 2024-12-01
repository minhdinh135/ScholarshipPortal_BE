using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Application;
using Domain.DTOs.Common;
using Domain.Entities;

namespace Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ApplicationDocument> _applicationDocumentRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IAccountService _accountService;
        private readonly IApplicationReviewRepository _applicationReviewRepository;
        private readonly IGenericRepository<AwardMilestone> _awardMilestoneRepository;


        public ApplicationService(IApplicationRepository applicationRepository, IMapper mapper,
            IGenericRepository<ApplicationDocument> applicationDocumentRepository,
            ICloudinaryService cloudinaryService, IAccountService accountService,
            IApplicationReviewRepository applicationReviewRepository,
            IGenericRepository<AwardMilestone> awardMilestoneRepository)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
            _applicationDocumentRepository = applicationDocumentRepository;
            _cloudinaryService = cloudinaryService;
            _accountService = accountService;
            _applicationReviewRepository = applicationReviewRepository;
            _awardMilestoneRepository = awardMilestoneRepository;
        }

        public async Task<ApplicationDto> Add(AddApplicationDto dto)
        {
            var application = _mapper.Map<Domain.Entities.Application>(dto);
            application.AppliedDate = DateTime.Now;
            application.Status = ApplicationStatusEnum.Submitted.ToString();

            await _applicationRepository.Add(application);

            return _mapper.Map<ApplicationDto>(application);
        }

        public async Task<ApplicationDto> Delete(int id)
        {
            var entity = await _applicationRepository.GetWithDocumentsAndAccount(id);
            if (entity == null) return null;
            foreach (var document in entity.ApplicationDocuments)
            {
                await _applicationDocumentRepository.DeleteById(document.Id);
                var fileId = document.FileUrl != null ? document.FileUrl.Split('/')[^1] : null;
                if (fileId != null)
                    await _cloudinaryService.DeleteFile(fileId);
            }

            await _applicationRepository.DeleteById(id);
            return _mapper.Map<ApplicationDto>(entity);
        }

        public async Task<IEnumerable<ApplicationReviewDto>> GetAllReviews()
        {
            var reviews = await _applicationReviewRepository.GetAll();

            return _mapper.Map<IEnumerable<ApplicationReviewDto>>(reviews);
        }

        public async Task<IEnumerable<ApplicationReviewDto>> GetReviewsResult(bool isFirstReview)
        {
            var reviews = await _applicationReviewRepository.GetApplicationReviewsResult(isFirstReview);

            return _mapper.Map<IEnumerable<ApplicationReviewDto>>(reviews);
        }

        public async Task AssignApplicationsToExpert(AssignApplicationsToExpertRequest request)
        {
            try
            {
                var expert = await _accountService.GetAccount(request.ExpertId);
                if (expert == null || expert.RoleName != RoleEnum.Expert.ToString())
                    throw new ServiceException($"User with id {request.ExpertId} is not Expert");

                foreach (var applicationId in request.ApplicationIds)
                {
                    var application = await _applicationRepository.GetById(applicationId);
                    if (application == null)
                        throw new ServiceException($"Application with id {applicationId} not found.");
                    application.Status = ApplicationStatusEnum.Reviewing.ToString();
                    await _applicationRepository.Update(application);

                    var review = new ApplicationReview
                    {
                        Description = request.Description,
                        ApplicationId = applicationId,
                        ReviewDate = request.ReviewDate,
                        ExpertId = request.ExpertId,
                        Status = ApplicationReviewStatusEnum.Reviewing.ToString()
                    };
                    await _applicationReviewRepository.Add(review);
                }
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        public async Task CheckApplicationAward(Domain.Entities.Application profile)
        {
            if (profile.Status == ApplicationStatusEnum.Awarded.ToString())
            {
                var awards = await _awardMilestoneRepository.GetAll();
                awards = awards.Where(x => x.ScholarshipProgramId == profile.ScholarshipProgramId).ToList();
                var award = awards.Where(x =>
                        x.FromDate < profile.UpdatedAt &&
                        x.ToDate > profile.UpdatedAt)
                    .FirstOrDefault();
                if (award == null)
                {
                    profile.Status = ApplicationStatusEnum.Awarded.ToString();
                    await _applicationRepository.Update(profile);
                    return;
                }

                if (award.ToDate.Value < DateTime.Now)
                {
                    profile.Status = ApplicationStatusEnum.NeedExtend.ToString();
                    await _applicationRepository.Update(profile);
                }
            }
            else if (profile.Status == ApplicationStatusEnum.NeedExtend.ToString())
            {
                var awards = await _awardMilestoneRepository.GetAll();
                var award = awards.Where(x =>
                        x.ScholarshipProgramId == profile.ScholarshipProgramId &&
                        x.FromDate < profile.UpdatedAt &&
                        x.ToDate > profile.UpdatedAt)
                    .FirstOrDefault();
                if (award == null)
                {
                    profile.Status = ApplicationStatusEnum.Awarded.ToString();
                    await _applicationRepository.Update(profile);
                    return;
                }

                if (award.ToDate.Value < DateTime.Now)
                {
                    profile.Status = ApplicationStatusEnum.Rejected.ToString();
                    profile.UpdatedAt = award.ToDate.Value.AddDays(-1);
                    await _applicationRepository.Update(profile);
                }
            }
        }

        public async Task UpdateReviewResult(UpdateReviewResultDto updateReviewResultDto)
        {
            var existingApplicationReview =
                await _applicationReviewRepository.GetById(updateReviewResultDto.ApplicationReviewId);
            if (existingApplicationReview == null)
                throw new ServiceException(
                    $"Application review with ID {updateReviewResultDto.ApplicationReviewId} is not found");

            existingApplicationReview.Score = updateReviewResultDto.Score;
            existingApplicationReview.Comment = updateReviewResultDto.Comment;
            existingApplicationReview.Status = updateReviewResultDto.IsPassed
                ? updateReviewResultDto.IsFirstReview
                    ? ApplicationReviewStatusEnum.Approved.ToString()
                    : ApplicationReviewStatusEnum.Passed.ToString()
                : ApplicationReviewStatusEnum.Failed.ToString();

            try
            {
                await _applicationReviewRepository.Update(existingApplicationReview);

                var application =
                    await _applicationRepository.GetApplicationById((int)existingApplicationReview.ApplicationId);

                if (!updateReviewResultDto.IsPassed)
                {
                    application.Status = ApplicationStatusEnum.Rejected.ToString();
                    await _applicationRepository.Update(application);
                }
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }


        public async Task<ApplicationDto> Get(int id)
        {
            var entity = await _applicationRepository.GetApplicationById(id);
            if (entity == null) return null;
            return _mapper.Map<ApplicationDto>(entity);
        }

        public async Task<IEnumerable<ApplicationDto>> GetExpertAssignedApplications(int expertId)
        {
            var expertAssignedApplications = await _applicationRepository.GetExpertAssignedApplications(expertId);

            return _mapper.Map<IEnumerable<ApplicationDto>>(expertAssignedApplications);
        }

        public async Task<IEnumerable<ApplicationDto>> GetAll()
        {
            var entities = await _applicationRepository.GetAll();
            return _mapper.Map<IEnumerable<ApplicationDto>>(entities);
        }

        public async Task<Domain.Entities.Application> GetWithDocumentsAndAccount(int applicationId)
        {
            var entities = await _applicationRepository.GetWithDocumentsAndAccount(applicationId);
            return entities;
        }

        public async Task<PaginatedList<ApplicationDto>> GetAll(int pageIndex, int pageSize, string sortBy,
            string sortOrder)
        {
            var categories = await _applicationRepository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

            return _mapper.Map<PaginatedList<ApplicationDto>>(categories);
        }

        public async Task<IEnumerable<ApplicationDto>> GetApplicationsByApplicantId(int applicantId)
        {
            var applications = await _applicationRepository.GetByApplicantId(applicantId);

            return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
        }

        public async Task<IEnumerable<ApplicationDto>> GetApplicationsByScholarshipProgramId(int scholarshipProgramId)
        {
            var applications = await _applicationRepository.GetByScholarshipProgramId(scholarshipProgramId);

            return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
        }

        public async Task<ApplicationDto> Update(int id, UpdateApplicationStatusRequest dto)
        {
            var existingApplication = await _applicationRepository.GetById(id);
            if (existingApplication == null)
                throw new ServiceException($"Application with id:{id} is not found", new NotFoundException());

            _mapper.Map(dto, existingApplication);
            var updatedApplication = await _applicationRepository.Update(existingApplication);
            return _mapper.Map<ApplicationDto>(updatedApplication);
        }

        public async Task<IEnumerable<Domain.Entities.Application>> GetByScholarshipId(int scholarshipId)
        {
            var entities = await _applicationRepository.GetByScholarshipId(scholarshipId);
            return entities;
        }


        public async Task<Domain.Entities.Application> ExtendApplication(ExtendApplicationDto extendApplicationDto)
        {
            foreach (var file in extendApplicationDto.Documents)
            {
                var entity = new ApplicationDocument
                {
                    ApplicationId = extendApplicationDto.ApplicationId,
                    Name = file.Name,
                    Type = file.Type,
                    FileUrl = file.FileUrl
                };
                await _applicationDocumentRepository.Add(entity);
            }

            var entities = await _applicationRepository.GetById(extendApplicationDto.ApplicationId);
            entities.Status = ApplicationStatusEnum.Submitted.ToString();
            await _applicationRepository.Update(entities);

            return entities;
        }
    }
}