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
            .ForMember(dest => dest.Skills, opt =>
                opt.MapFrom(src => src.ScholarshipProgramSkills.Select(spm => spm.Skill)))
            .ReverseMap();
        CreateMap<CreateScholarshipProgramRequest, ScholarshipProgram>()
            .ForMember(dest => dest.ScholarshipProgramMajors, opt =>
                opt.MapFrom(src => src.MajorIds.Select(id => new ScholarshipProgramMajor { MajorId = id }).ToList()))
            .ForMember(dest => dest.ScholarshipProgramUniversities, opt =>
                opt.MapFrom(src =>
                    src.UniversityIds.Select(id => new ScholarshipProgramUniversity { UniversityId = id }).ToList()))
            .ForMember(dest => dest.ScholarshipProgramCertificates, opt =>
                opt.MapFrom(src =>
                    src.CertificateIds.Select(id => new ScholarshipProgramCertificate { CertificateId = id }).ToList()))
            .ForMember(dest => dest.ScholarshipProgramSkills, opt =>
                opt.MapFrom(src => src.SkillIds.Select(id => new ScholarshipProgramSkill { SkillId = id }).ToList()));
        CreateMap<UpdateScholarshipProgramRequest, ScholarshipProgram>()
            .ForMember(dest => dest.ScholarshipProgramMajors, opt =>
                opt.MapFrom((src, dest) => src.MajorIds.Select(id => new ScholarshipProgramMajor
                    { MajorId = id, ScholarshipProgramId = dest.Id }).ToList()))
            .ForMember(dest => dest.ScholarshipProgramUniversities, opt =>
                opt.MapFrom((src, dest) => src.UniversityIds.Select(id => new ScholarshipProgramUniversity
                    { UniversityId = id, ScholarshipProgramId = dest.Id })))
            .ForMember(dest => dest.ScholarshipProgramCertificates, opt =>
                opt.MapFrom((src, dest) => src.CertificateIds.Select(id => new ScholarshipProgramCertificate
                    { CertificateId = id, ScholarshipProgramId = dest.Id })))
            .ForMember(dest => dest.ScholarshipProgramSkills, opt =>
                opt.MapFrom((src, dest) => src.SkillIds.Select(id => new ScholarshipProgramSkill
                    { SkillId = id, ScholarshipProgramId = dest.Id })));

        CreateMap<Skill, SkillDto>().ReverseMap();

        CreateMap<Certificate, CertificateDto>().ReverseMap();

        CreateMap<University, AddUniversityDto>().ReverseMap();
        CreateMap<University, UpdateUniversityDto>().ReverseMap();
        CreateMap<University, UniversityDto>().ReverseMap();

        CreateMap<Criteria, CriteriaDto>().ReverseMap();
        CreateMap<CreateCriteriaRequest, Criteria>();
        CreateMap<UpdateCriteriaRequest, Criteria>();

        CreateMap<Major, MajorDto>().ReverseMap();
        CreateMap<CreateMajorRequest, Major>();
        CreateMap<UpdateMajorRequest, Major>();

        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>();

        CreateMap<Application, AddApplicationDto>().ReverseMap();
        CreateMap<Application, UpdateApplicationDto>().ReverseMap();
        CreateMap<Application, ApplicationDto>().ReverseMap();

        CreateMap<Notification, NotificationAddDTO>().ReverseMap();
        CreateMap<Notification, NotificationUpdateDTO>().ReverseMap();
        CreateMap<Notification, NotificationDTO>().ReverseMap();
        CreateMap<NotificationDTO, NotificationUpdateDTO>().ReverseMap();
    }
}