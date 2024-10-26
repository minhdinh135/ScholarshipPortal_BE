using AutoMapper;
using Domain.DTOs.Account;
using Domain.DTOs.Applicant;
using Domain.DTOs.Application;
using Domain.DTOs.Authentication;
using Domain.DTOs.Category;
using Domain.DTOs.Criteria;
using Domain.DTOs.Major;
using Domain.DTOs.Notification;
using Domain.DTOs.Role;
using Domain.DTOs.ScholarshipProgram;
using Domain.DTOs.University;
using Domain.Entities;

namespace Domain.Automapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
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

        CreateMap<ScholarshipProgram, ScholarshipProgramDto>().ReverseMap();
        CreateMap<ScholarshipProgram, CreateScholarshipProgramRequest>().ReverseMap();
        CreateMap<ScholarshipProgram, UpdateScholarshipProgramRequest>().ReverseMap();

        CreateMap<Skill, SkillDto>().ReverseMap();

        CreateMap<Certificate, CertificateDto>().ReverseMap();

        CreateMap<University, AddUniversityDto>().ReverseMap();
        CreateMap<University, UpdateUniversityDto>().ReverseMap();
        CreateMap<University, UniversityDto>().ReverseMap();

        CreateMap<Application, AddApplicationDto>().ReverseMap();
        CreateMap<Application, UpdateApplicationDto>().ReverseMap();
        CreateMap<Application, ApplicationDto>().ReverseMap();

        CreateMap<Criteria, CriteriaDto>().ReverseMap();
        CreateMap<CreateCriteriaRequest, Criteria>();
        CreateMap<UpdateCriteriaRequest, Criteria>();

        CreateMap<Major, MajorDto>().ReverseMap();
        CreateMap<CreateMajorRequest, Major>();
        CreateMap<UpdateMajorRequest, Major>();

        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>();

        CreateMap<Notification, NotificationAddDTO>().ReverseMap();
        CreateMap<Notification, NotificationUpdateDTO>().ReverseMap();
        CreateMap<Notification, NotificationDTO>().ReverseMap();
        CreateMap<NotificationDTO, NotificationUpdateDTO>().ReverseMap();
    }
}