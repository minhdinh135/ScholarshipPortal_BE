using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Account;
using Domain.DTOs.Applicant;
using Domain.DTOs.Application;
using Domain.DTOs.Authentication;
using Domain.DTOs.AwardMilestone;
using Domain.DTOs.Category;
using Domain.DTOs.Common;
using Domain.DTOs.Country;
using Domain.DTOs.Criteria;
using Domain.DTOs.Expert;
using Domain.DTOs.Feedback;
using Domain.DTOs.Funder;
using Domain.DTOs.Major;
using Domain.DTOs.Notification;
using Domain.DTOs.Payment;
using Domain.DTOs.Provider;
using Domain.DTOs.Request;
using Domain.DTOs.ReviewMilestone;
using Domain.DTOs.Role;
using Domain.DTOs.ScholarshipProgram;
using Domain.DTOs.Service;
using Domain.DTOs.Subscription;
using Domain.DTOs.University;
using Domain.Entities;

namespace Domain.Automapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<BaseEntity, BaseDto>()
        //     .ReverseMap();

        CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>));

        //Account and Role Mapping
        CreateMap<Role, AddRoleDto>().ReverseMap();
        CreateMap<Role, UpdateRoleDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();

        CreateMap<Account, RegisterDto>();
        CreateMap<RegisterDto, Account>()
            .ForMember(dest => dest.FunderId, opt =>
                opt.MapFrom(r => (r.FunderId == null || r.FunderId == 0) ? null : r.FunderId));
        CreateMap<Account, UpdateAccountDto>().ReverseMap();
        CreateMap<Account, AccountDto>()
            .ForMember(dest => dest.RoleName, opt =>
                opt.MapFrom(src => src.Role.Name))
            .ReverseMap();
        CreateMap<AccountDto, UpdateAccountDto>().ReverseMap();
        CreateMap<Account, ExpertDetailsDto>()
            .ForMember(dest => dest.Name, opt => opt
                .MapFrom(src => src.ExpertProfile.Name))
            .ForMember(dest => dest.Description, opt => opt
                .MapFrom(src => src.ExpertProfile.Description))
            .ForMember(dest => dest.Major, opt => opt
                .MapFrom(src => src.ExpertProfile.Major))
            .ReverseMap();

        CreateMap<Wallet, WalletDto>().ReverseMap();
        CreateMap<CreateWalletDto, Wallet>();
        CreateMap<UpdateWalletBalanceDto, Wallet>();

        CreateMap<Subscription, SubscriptionDto>().ReverseMap();
        CreateMap<AddSubscriptionDto, Subscription>();
        CreateMap<UpdateSubscriptionDto, Subscription>();

        CreateMap<TransferRequest, Transaction>()
            .ForMember(dest => dest.Amount, opt =>
                opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Description, opt =>
                opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.TransactionId, opt =>
                opt.MapFrom(src => Guid.NewGuid().ToString("N")))
            .ForMember(dest => dest.PaymentMethod, opt =>
                opt.MapFrom(src => src.PaymentMethod))
            .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => TransactionStatusEnum.Successful.ToString()))
            .ForMember(dest => dest.TransactionDate, opt =>
                opt.MapFrom(src => DateTime.UtcNow));


        // Funder Profile mapping
        CreateMap<FunderProfile, FunderProfileDto>().ReverseMap();
        CreateMap<FunderProfile, FunderProfileDetails>()
            .ForMember(dest => dest.Avatar, opt => opt
                .MapFrom(src => src.Funder.AvatarUrl))
            .ForMember(dest => dest.Username, opt => opt
                .MapFrom(src => src.Funder.Username))
            .ForMember(dest => dest.Email, opt => opt
                .MapFrom(src => src.Funder.Email))
            .ForMember(dest => dest.Phone, opt => opt
                .MapFrom(src => src.Funder.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt
                .MapFrom(src => src.Funder.Address))
            .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Funder.Status))
            .ForMember(dest => dest.FunderDocuments, opt =>
                opt.MapFrom(src => src.FunderDocuments))
            .ReverseMap();
        CreateMap<AddFunderDetailsDto, FunderProfile>();
        CreateMap<UpdateFunderDetailsDto, FunderProfile>();
        CreateMap<FunderDocument, FunderDocumentDto>().ReverseMap();

        // Expert Profile mapping
        CreateMap<ExpertProfile, ExpertProfileDto>().ReverseMap();
        CreateMap<ExpertProfile, ExpertDetailsDto>()
            .ForMember(dest => dest.AvatarUrl, opt => opt
                .MapFrom(src => src.Expert.AvatarUrl))
            .ForMember(dest => dest.Username, opt => opt
                .MapFrom(src => src.Expert.Username))
            .ForMember(dest => dest.Email, opt => opt
                .MapFrom(src => src.Expert.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt
                .MapFrom(src => src.Expert.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt
                .MapFrom(src => src.Expert.Address))
            .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Expert.Status))
            .ForMember(dest => dest.FunderId, opt =>
                opt.MapFrom(src => src.Expert.FunderId))
            .ReverseMap();
        CreateMap<CreateExpertDetailsDto, ExpertProfile>();
        CreateMap<UpdateExpertDetailsDto, ExpertProfile>();

        // Provider Profile mapping
        CreateMap<ProviderProfile, ProviderProfileDto>().ReverseMap();
        CreateMap<ProviderProfile, ProviderProfileDetails>()
            .ForMember(dest => dest.Avatar, opt => opt
                .MapFrom(src => src.Provider.AvatarUrl))
            .ForMember(dest => dest.Username, opt => opt
                .MapFrom(src => src.Provider.Username))
            .ForMember(dest => dest.Email, opt => opt
                .MapFrom(src => src.Provider.Email))
            .ForMember(dest => dest.Phone, opt => opt
                .MapFrom(src => src.Provider.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt
                .MapFrom(src => src.Provider.Address))
            .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Provider.Status))
            .ForMember(dest => dest.SubscriptionName, opt =>
                opt.MapFrom(src => src.Provider.Subscription.Name))
            .ForMember(dest => dest.ProviderDocuments, opt =>
                opt.MapFrom(src => src.ProviderDocuments))
            .ReverseMap();
        CreateMap<ProviderDocument, ProviderDocumentDto>().ReverseMap();
        CreateMap<AddProviderDetailsDto, ProviderProfile>();
        CreateMap<UpdateProviderDetailsDto, ProviderProfile>();

        // Applicant Profile mapping
        CreateMap<ApplicantProfile, AddApplicantProfileDto>().ReverseMap();
        CreateMap<ApplicantProfile, ApplicantProfileDetails>()
            .ForMember(dest => dest.Avatar, opt => opt
                .MapFrom(src => src.Applicant.AvatarUrl))
            .ForMember(dest => dest.Username, opt => opt
                .MapFrom(src => src.Applicant.Username))
            .ForMember(dest => dest.Email, opt => opt
                .MapFrom(src => src.Applicant.Email))
            .ForMember(dest => dest.Phone, opt => opt
                .MapFrom(src => src.Applicant.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt
                .MapFrom(src => src.Applicant.Address))
            .ForMember(dest => dest.Achievements, opt => opt
                .MapFrom(src => src.Achievements.Select(a => a.Name)))
            .ForMember(dest => dest.Skills, opt => opt
                .MapFrom(src => src.ApplicantSkills.Select(s => s.Name)))
            .ForMember(dest => dest.Experience, opt => opt
                .MapFrom(src => src.Experiences.Select(e => e.Name)))
            .ForMember(dest => dest.Certificates, opt => opt
                .MapFrom(src => src.ApplicantCertificates.Select(c => c.Name)))
            .ReverseMap();
        CreateMap<ApplicantProfile, UpdateApplicantProfileDto>().ReverseMap();
        CreateMap<ApplicantProfile, ApplicantProfileDto>().ReverseMap();

        CreateMap<Achievement, AddAchievementDto>().ReverseMap();
        CreateMap<Achievement, UpdateAchievementDto>().ReverseMap();
        CreateMap<Achievement, AchievementDto>().ReverseMap();

        CreateMap<ApplicantSkill, ApplicantSkillDto>().ReverseMap();
        CreateMap<ApplicantSkill, AddApplicantSkillDto>().ReverseMap();
        CreateMap<ApplicantSkill, UpdateApplicantSkillDto>().ReverseMap();

        CreateMap<ApplicantCertificate, ApplicantCertificateDto>().ReverseMap();
        CreateMap<ApplicantCertificate, AddApplicantCertificateDto>().ReverseMap();
        CreateMap<ApplicantCertificate, UpdateApplicantCertificateDto>().ReverseMap();

        // Scholarship Program mapping
        CreateMap<ScholarshipProgram, ScholarshipProgramDto>()
            .ForMember(dest => dest.Certificates, opt =>
                opt.MapFrom(src => src.ScholarshipProgramCertificates.Select(spc => spc.Certificate)))
            .ReverseMap();
        CreateMap<CreateScholarshipProgramRequest, ScholarshipProgram>()
            .ForMember(dest => dest.ScholarshipProgramCertificates, opt =>
                opt.MapFrom(src =>
                    src.CertificateIds.Select(id => new ScholarshipProgramCertificate { CertificateId = id })
                        .ToList()));
        CreateMap<UpdateScholarshipProgramRequest, ScholarshipProgram>()
            .ForMember(dest => dest.ScholarshipProgramCertificates, opt =>
                opt.MapFrom((src, dest) => src.CertificateIds.Select(id => new ScholarshipProgramCertificate
                    { CertificateId = id, ScholarshipProgramId = dest.Id }).ToList()));

        CreateMap<List<MajorSkillsDto>, ICollection<MajorSkill>>()
            .ConvertUsing(dtos =>
                dtos.SelectMany(dto => dto.SkillIds.Select(skillId => new MajorSkill
                {
                    MajorId = dto.MajorId,
                    SkillId = skillId
                })).ToList()
            );

        CreateMap<Skill, SkillDto>().ReverseMap();

        CreateMap<Certificate, CertificateDto>().ReverseMap();

        CreateMap<Country, CountryDto>().ReverseMap();

        CreateMap<University, AddUniversityDto>().ReverseMap();
        CreateMap<University, UpdateUniversityDto>().ReverseMap();
        CreateMap<University, UniversityDto>().ReverseMap();

        CreateMap<Criteria, CriteriaDto>().ReverseMap();
        CreateMap<Criteria, CriteriaDetails>().ReverseMap();
        CreateMap<CreateCriteriaRequest, Criteria>();
        CreateMap<UpdateCriteriaRequest, Criteria>();

        CreateMap<Major, MajorDto>()
            .ForMember(dest => dest.Skills, opt =>
                opt.MapFrom(src => src.MajorSkills.Select(ms => ms.Skill)))
            .ReverseMap();
        CreateMap<CreateMajorRequest, Major>();
        CreateMap<UpdateMajorRequest, Major>();

        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>();

        // Application
        CreateMap<Application, AddApplicationDto>()
            .ForMember(dest => dest.Documents, opt =>
                opt.MapFrom(src => src.ApplicationDocuments))
            .ReverseMap();
        CreateMap<Application, UpdateApplicationDto>().ReverseMap();
        CreateMap<Application, UpdateApplicationStatusRequest>().ReverseMap();
        CreateMap<Application, ApplicationDto>()
            .ForMember(dest => dest.ApplicantName, opt =>
                opt.MapFrom(src =>
                    $"{src.Applicant.ApplicantProfile.FirstName} {src.Applicant.ApplicantProfile.LastName}"))
            .ForMember(dest => dest.ScholarshipName, opt =>
                opt.MapFrom(src => src.ScholarshipProgram.Name))
            .ReverseMap();
        CreateMap<Application, ApplicationFullDto>().ReverseMap();

        CreateMap<ApplicationDocument, ApplicationDocumentDto>().ReverseMap();
        CreateMap<ApplicationDocument, AddApplicationDocumentDto>().ReverseMap();
        CreateMap<ApplicationDocument, UpdateApplicationDocumentDto>().ReverseMap();

        CreateMap<ApplicationReview, ApplicationReviewDto>()
            .ForMember(dest => dest.ApplicantName, opt =>
                opt.MapFrom(src =>
                    $"{src.Application.Applicant.ApplicantProfile.FirstName} {src.Application.Applicant.ApplicantProfile.LastName}"))
            .ReverseMap();

        // Service mapping
        CreateMap<Service, ServiceDto>().ReverseMap();
        CreateMap<AddServiceDto, Service>();
        CreateMap<UpdateServiceDto, Service>();

        // Request mapping
        CreateMap<Request, RequestDto>()
            .ForMember(dest => dest.RequestDetails, opt =>
                opt.MapFrom(src => src.RequestDetails))
            .ForMember(dest => dest.Service, opt =>
                opt.MapFrom(src => src.RequestDetails.First().Service))
            .ReverseMap();
        CreateMap<RequestDetail, RequestDetailsDto>()
            .ForMember(dest => dest.Files, opt =>
                opt.MapFrom(src => src.RequestDetailFiles.Select(f => new RequestFile
                {
                    FileUrl = f.FileUrl,
                    UploadDate = f.UploadDate,
                    UploadedBy = f.UploadedBy
                })))
            .ReverseMap();
        CreateMap<RequestDetailFile, RequestFile>().ReverseMap();

        // Feedback mapping
        CreateMap<Feedback, FeedbackDto>().ReverseMap();
        CreateMap<AddFeedbackDto, Feedback>();
        CreateMap<UpdateFeedbackDto, Feedback>();

        // Notification mapping
        CreateMap<Notification, NotificationAddDTO>().ReverseMap();
        CreateMap<Notification, NotificationUpdateDTO>().ReverseMap();
        CreateMap<Notification, NotificationDTO>().ReverseMap();
        CreateMap<NotificationDTO, NotificationUpdateDTO>().ReverseMap();

        // Review milestone mapping
        CreateMap<ReviewMilestone, ReviewMilestoneDto>().ReverseMap();
        CreateMap<ReviewMilestone, ReviewMilestoneDetails>().ReverseMap();
        CreateMap<AddReviewMilestoneDto, ReviewMilestone>();
        CreateMap<UpdateReviewMilestoneDto, ReviewMilestone>();

        // Award milestone mapping
        CreateMap<AwardMilestone, AwardMilestoneDto>().ReverseMap();
        CreateMap<AwardMilestone, AwardMilestoneDetails>().ReverseMap();
        CreateMap<CreateAwardMilestoneDto, AwardMilestone>();
        CreateMap<UpdateAwardMilestoneDto, AwardMilestone>();

        // Award milestone mapping
        CreateMap<AwardMilestoneDocument, AwardMilestoneDocumentDto>().ReverseMap();
    }
}
