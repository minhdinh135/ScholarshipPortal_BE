using AutoMapper;
using Domain.DTOs.Account;
using Domain.DTOs.Achievement;
using Domain.DTOs.Applicant;
using Domain.DTOs.Application;
using Domain.DTOs.Authentication;
using Domain.DTOs.Category;
using Domain.DTOs.Criteria;
using Domain.DTOs.Major;
using Domain.DTOs.Notification;
using Domain.DTOs.Role;
using Domain.DTOs.University;
using Domain.Entities;

namespace Domain.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, RegisterDto>().ReverseMap();
            CreateMap<Account, UpdateAccountDto>().ReverseMap();
            CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.RoleName, opt =>
                    opt.MapFrom(src => src.Role.Name))
                .ReverseMap();

            CreateMap<ApplicantProfile, ApplicantProfileDto>().ReverseMap();
            CreateMap<ApplicantProfile, AddApplicantProfileDto>().ReverseMap();
            CreateMap<ApplicantProfile, UpdateApplicantProfileDto>().ReverseMap();

            CreateMap<Role, AddRoleDto>().ReverseMap();
            CreateMap<Role, UpdateRoleDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<University, AddUniversityDto>().ReverseMap();
            CreateMap<University, UpdateUniversityDto>().ReverseMap();
            CreateMap<University, UniversityDto>().ReverseMap();

            CreateMap<Achievement, AddAchievementDto>().ReverseMap();
            CreateMap<Achievement, UpdateAchievementDto>().ReverseMap();
            CreateMap<Achievement, AchievementDto>().ReverseMap();

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
}