using AutoMapper;
using Domain.DTOs.Account;
using Domain.DTOs.Applicant;
using Domain.DTOs.Application;
using Domain.DTOs.Authentication;
using Domain.DTOs.Category;
using Domain.DTOs.Country;
using Domain.DTOs.Criteria;
using Domain.DTOs.Feedback;
using Domain.DTOs.Funder;
using Domain.DTOs.Major;
using Domain.DTOs.Notification;
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
        
        // Funder Profile mapping
        CreateMap<FunderProfile, FunderProfileDto>().ReverseMap();
        CreateMap<UpdateFunderDetailsDto, FunderProfile>();
        CreateMap<FunderDocument, FunderDocumentDto>().ReverseMap();
        
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
            .ForMember(dest => dest.Majors, opt =>
                opt.MapFrom(src => src.ScholarshipProgramMajors.Select(spm => spm.Major)))
            .ForMember(dest => dest.Universities, opt =>
                opt.MapFrom(src => src.ScholarshipProgramUniversities.Select(spu => spu.University)))
            .ForMember(dest => dest.Certificates, opt =>
                opt.MapFrom(src => src.ScholarshipProgramCertificates.Select(spc => spc.Certificate)))
            .ReverseMap();
        CreateMap<CreateScholarshipProgramRequest, ScholarshipProgram>()
            .ForMember(dest => dest.ScholarshipProgramMajors, opt =>
                opt.MapFrom(src => src.MajorIds.Select(id => new ScholarshipProgramMajor { MajorId = id }).ToList()))
            .ForMember(dest => dest.ScholarshipProgramUniversities, opt =>
                opt.MapFrom(src =>
                    src.UniversityIds.Select(id => new ScholarshipProgramUniversity { UniversityId = id }).ToList()))
            .ForMember(dest => dest.ScholarshipProgramCertificates, opt =>
                opt.MapFrom(src =>
                    src.CertificateIds.Select(id => new ScholarshipProgramCertificate { CertificateId = id })
                        .ToList()));
        CreateMap<UpdateScholarshipProgramRequest, ScholarshipProgram>()
            .ForMember(dest => dest.ScholarshipProgramMajors, opt =>
                opt.MapFrom((src, dest) => src.MajorIds.Select(id => new ScholarshipProgramMajor
                    { MajorId = id, ScholarshipProgramId = dest.Id }).ToList()))
            .ForMember(dest => dest.ScholarshipProgramUniversities, opt =>
                opt.MapFrom((src, dest) => src.UniversityIds.Select(id => new ScholarshipProgramUniversity
                    { UniversityId = id, ScholarshipProgramId = dest.Id }).ToList()))
            .ForMember(dest => dest.ScholarshipProgramCertificates, opt =>
                opt.MapFrom((src, dest) => src.CertificateIds.Select(id => new ScholarshipProgramCertificate
                    { CertificateId = id, ScholarshipProgramId = dest.Id }).ToList()));
            
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

        CreateMap<Application, AddApplicationDto>().ReverseMap();
        CreateMap<Application, UpdateApplicationDto>().ReverseMap();
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
        CreateMap<RequestDetail, RequestDetailsDto>().ReverseMap();
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
