using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Account;
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
		}
    }
}
