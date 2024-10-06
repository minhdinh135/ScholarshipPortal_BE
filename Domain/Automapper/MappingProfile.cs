using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Account;
using Domain.DTOs.Category;
using Domain.DTOs.Common;
using Domain.DTOs;
using Domain.DTOs.Account;
using Domain.DTOs.Achievement;
using Domain.DTOs.ApplicantProfile;
using Domain.DTOs.Award;
using Domain.DTOs.Country;
using Domain.DTOs.Criteria;
using Domain.DTOs.Document;
using Domain.DTOs.Feedback;
using Domain.DTOs.Major;
using Domain.DTOs.Review;
using Domain.DTOs.Role;
using Domain.DTOs.ScholarshipProgram;
using Domain.DTOs.University;
using Domain.Entities;

namespace Domain.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseEntity, BaseDto>()
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt))
                .ReverseMap();

            CreateMap<BaseCreateRequest, BaseEntity>()
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => StatusEnum.ACTIVE));

            CreateMap<BaseUpdateRequest, BaseEntity>()
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<Account, AccountAddDTO>().ReverseMap();
            CreateMap<Account, AccountUpdateDTO>().ReverseMap();

            CreateMap<Role, RoleAddDTO>().ReverseMap();
            CreateMap<Role, RoleUpdateDTO>().ReverseMap();

            CreateMap<Review, AddReviewDTO>().ReverseMap();
            CreateMap<Review, UpdateReviewDTO>().ReverseMap();

            CreateMap<Feedback, AddFeedbackDTO>().ReverseMap();
            CreateMap<Feedback, UpdateFeedbackDTO>().ReverseMap();

            CreateMap<Document, AddDocumentDTO>().ReverseMap();
            CreateMap<Document, UpdateDocumentDTO>().ReverseMap();

            CreateMap<Award, AddAwardDTO>().ReverseMap();
            CreateMap<Award, UpdateAwardDTO>().ReverseMap();

            CreateMap<ApplicantProfile, AddApplicantProfileDTO>().ReverseMap();
            CreateMap<ApplicantProfile, UpdateApplicantProfileDTO>().ReverseMap();

            CreateMap<University, AddUniversityDTO>().ReverseMap();
            CreateMap<University, UpdateUniversityDTO>().ReverseMap();

            CreateMap<Country, AddCountryDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();

            CreateMap<Category, CategoryDto>()
                .IncludeBase<BaseEntity, BaseDto>()
                .ReverseMap();
            CreateMap<CreateCategoryRequest, Category>()
                .IncludeBase<BaseCreateRequest, BaseEntity>();
            CreateMap<UpdateCategoryRequest, Category>()
                .IncludeBase<BaseUpdateRequest, BaseEntity>();

            CreateMap<Major, MajorDto>()
                .IncludeBase<BaseEntity, BaseDto>()
                .ReverseMap();
            CreateMap<CreateMajorRequest, Major>()
                .IncludeBase<BaseCreateRequest, BaseEntity>();
            CreateMap<UpdateMajorRequest, Major>()
                .IncludeBase<BaseUpdateRequest, BaseEntity>();

            CreateMap<Criteria, CriteriaDto>()
                .IncludeBase<BaseEntity, BaseDto>()
                .ReverseMap();
            CreateMap<CreateCriteriaRequest, Criteria>()
                .IncludeBase<BaseCreateRequest, BaseEntity>();
            CreateMap<UpdateCriteriaRequest, Criteria>()
                .IncludeBase<BaseUpdateRequest, BaseEntity>();

            CreateMap<ScholarshipProgram, ScholarshipProgramDto>()
                .IncludeBase<BaseEntity, BaseDto>()
                .ReverseMap();
            CreateMap<CreateScholarshipProgramRequest, ScholarshipProgram>()
                .IncludeBase<BaseCreateRequest, BaseEntity>();
            CreateMap<UpdateScholarshipProgramRequest, ScholarshipProgram>()
                .IncludeBase<BaseUpdateRequest, BaseEntity>();

            CreateMap<Achievement, AchievementAddDTO>().ReverseMap();
            CreateMap<Achievement, AchievementUpdateDTO>().ReverseMap();
        }
    }
}