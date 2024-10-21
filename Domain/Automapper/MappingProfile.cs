using AutoMapper;
using Domain.DTOs.Account;
using Domain.DTOs.Achievement;
using Domain.DTOs.ApplicantProfile;
using Domain.DTOs.Application;
using Domain.DTOs.Country;
using Domain.DTOs.Document;
using Domain.DTOs.Feedback;
using Domain.DTOs.Review;
using Domain.DTOs.Role;
using Domain.DTOs.University;
using Domain.Entities;

namespace Domain.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountAddDTO>().ReverseMap();
            CreateMap<Account, AccountUpdateDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();

            CreateMap<Role, RoleAddDTO>().ReverseMap();
            CreateMap<Role, RoleUpdateDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();

            CreateMap<ApplicationReview, AddReviewDTO>().ReverseMap();
            CreateMap<ApplicationReview, UpdateReviewDTO>().ReverseMap();

            CreateMap<Feedback, AddFeedbackDTO>().ReverseMap();
            CreateMap<Feedback, UpdateFeedbackDTO>().ReverseMap();

            CreateMap<ApplicationDocument, AddDocumentDTO>().ReverseMap();
            CreateMap<ApplicationDocument, UpdateDocumentDTO>().ReverseMap();

            CreateMap<ApplicantProfile, AddApplicantProfileDTO>().ReverseMap();
            CreateMap<ApplicantProfile, UpdateApplicantProfileDTO>().ReverseMap();

            CreateMap<University, AddUniversityDTO>().ReverseMap();
            CreateMap<University, UpdateUniversityDTO>().ReverseMap();
            CreateMap<University, UniversityResponse>().ReverseMap();

            CreateMap<Country, AddCountryDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();

            CreateMap<Achievement, AchievementAddDTO>().ReverseMap();
            CreateMap<Achievement, AchievementUpdateDTO>().ReverseMap();
            CreateMap<Achievement, AchievementDTO>().ReverseMap();

            CreateMap<Application, ApplicationAddDTO>().ReverseMap();
            CreateMap<Application, ApplicationUpdateDTO>().ReverseMap();
            CreateMap<Application, ApplicationDTO>().ReverseMap();
        }
    }
}
