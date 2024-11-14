using AutoMapper;
using Domain.DTOs.Account;
using Domain.DTOs.Applicant;
using Domain.DTOs.Application;
using Domain.DTOs.Authentication;
using Domain.DTOs.Category;
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
using Domain.DTOs.University;
using Domain.Entities;

namespace Domain.Automapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Account and Role Mapping
        CreateMap<Role, AddRoleDto>().ReverseMap();
        CreateMap<Role, UpdateRoleDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();

        CreateMap<Account, RegisterDto>().ReverseMap();
        CreateMap<Account, UpdateAccountDto>().ReverseMap();
        CreateMap<Account, AccountDto>()
            .ForMember(dest => dest.RoleName, opt =>
                opt.MapFrom(src => src.Role.Name))
            .ReverseMap();
        CreateMap<AccountDto, UpdateAccountDto>().ReverseMap();

        CreateMap<Wallet, WalletDto>().ReverseMap();
        CreateMap<CreateWalletDto, Wallet>();
        CreateMap<UpdateWalletBalanceDto, Wallet>();

        CreateMap<TransferRequest, Transaction>()
            .ForMember(dest => dest.Amount, opt =>
                opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Description, opt =>
                opt.MapFrom(src => "Wallet Balance Transfer"))
            .ForMember(dest => dest.TransactionId, opt =>
                opt.MapFrom(src => Guid.NewGuid().ToString("N")))
            .ForMember(dest => dest.PaymentMethod, opt =>
                opt.MapFrom(src => "Credit Card"))
            .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => "PAID"))
            .ForMember(dest => dest.TransactionDate, opt =>
                opt.MapFrom(src => DateTime.UtcNow));


        // Funder Profile mapping
        CreateMap<FunderProfile, FunderProfileDto>().ReverseMap();
        CreateMap<UpdateFunderDetailsDto, FunderProfile>();
        CreateMap<FunderDocument, FunderDocumentDto>().ReverseMap();
        
        // Expert Profile mapping
        CreateMap<ExpertProfile, ExpertProfileDto>().ReverseMap();
        CreateMap<CreateExpertDetailsDto, ExpertProfile>();
        CreateMap<UpdateExpertDetailsDto, ExpertProfile>();

        // Provider Profile mapping
        CreateMap<ProviderProfile, ProviderProfileDto>().ReverseMap();
        CreateMap<AddProviderDetailsDto, ProviderProfile>();
        CreateMap<UpdateProviderDetailsDto, ProviderProfile>();

        // Applicant Profile mapping
        CreateMap<ApplicantProfile, AddApplicantProfileDto>().ReverseMap();
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
            .ForMember(dest => dest.MajorSkills, opt =>
                opt.MapFrom(src => src.MajorSkills.GroupBy(ms => ms.Major.Id).Select(group => new MajorDto
                {
                    Id = group.Key,
                    Name = group.First().Major.Name,  // Use the first major from the group
                    Description = group.First().Major.Description,
                    Skills = group.Select(ms => new SkillDto
                    {
                        Id = ms.Skill.Id,
                        Name = ms.Skill.Name,
                        Description = ms.Skill.Description,
                        Type = ms.Skill.Type
                    }).ToList() 
                }).ToList()))
            .ForMember(dest => dest.Universities, opt =>
                opt.MapFrom(src => src.ScholarshipProgramUniversities.Select(spu => spu.University)))
            .ForMember(dest => dest.Certificates, opt =>
                opt.MapFrom(src => src.ScholarshipProgramCertificates.Select(spc => spc.Certificate)))
            .ReverseMap();
        CreateMap<CreateScholarshipProgramRequest, ScholarshipProgram>()
            .ForMember(dest => dest.MajorSkills, opt =>
                opt.MapFrom(src => src.MajorSkills))
            .ForMember(dest => dest.ScholarshipProgramUniversities, opt =>
                opt.MapFrom(src =>
                    src.UniversityIds.Select(id => new ScholarshipProgramUniversity { UniversityId = id }).ToList()))
            .ForMember(dest => dest.ScholarshipProgramCertificates, opt =>
                opt.MapFrom(src =>
                    src.CertificateIds.Select(id => new ScholarshipProgramCertificate { CertificateId = id })
                        .ToList()));
        CreateMap<UpdateScholarshipProgramRequest, ScholarshipProgram>()
            .ForMember(dest => dest.MajorSkills, opt =>
                opt.MapFrom((src, dest) => src.MajorSkills))
            .ForMember(dest => dest.ScholarshipProgramUniversities, opt =>
                opt.MapFrom((src, dest) => src.UniversityIds.Select(id => new ScholarshipProgramUniversity
                    { UniversityId = id, ScholarshipProgramId = dest.Id }).ToList()))
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

        CreateMap<ScholarshipProgram, ScholarshipProgramElasticDocument>()
            .ForMember(dest => dest.CategoryName, opt =>
                opt.MapFrom(src => src.Category.Name))
            .ReverseMap();

        CreateMap<Skill, SkillDto>().ReverseMap();

        CreateMap<Certificate, CertificateDto>().ReverseMap();

        CreateMap<Country, CountryDto>().ReverseMap();

        CreateMap<University, AddUniversityDto>().ReverseMap();
        CreateMap<University, UpdateUniversityDto>().ReverseMap();
        CreateMap<University, UniversityDto>().ReverseMap();

        CreateMap<Criteria, CriteriaDto>().ReverseMap();
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
        CreateMap<Application, ApplicationDto>().ReverseMap();
        
        CreateMap<ApplicationDocument, ApplicationDocumentDto>().ReverseMap();
        CreateMap<ApplicationDocument, AddApplicationDocumentDto>().ReverseMap();
        CreateMap<ApplicationDocument, UpdateApplicationDocumentDto>().ReverseMap();
        
        CreateMap<ApplicationReview, ApplicationReviewDto>().ReverseMap();

        // Service mapping
        CreateMap<Service, ServiceDto>().ReverseMap();
        CreateMap<AddServiceDto, Service>();
        CreateMap<UpdateServiceDto, Service>();

        // Request mapping
        CreateMap<Request, RequestDto>().ReverseMap();
        CreateMap<RequestDetail, RequestDetailsDto>()
            .ForMember(dest => dest.Service, opt =>
                opt.MapFrom(r => r.Service))
            .ReverseMap();
        CreateMap<AddRequestDto, Request>();
        CreateMap<UpdateRequestDto, Request>().ReverseMap();
        CreateMap<AddRequestDetailsDto, RequestDetail>();
        CreateMap<UpdateRequestDetailsDto, RequestDetail>();

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
        CreateMap<AddReviewMilestoneDto, ReviewMilestone>();
        CreateMap<UpdateReviewMilestoneDto, ReviewMilestone>();
    }
}