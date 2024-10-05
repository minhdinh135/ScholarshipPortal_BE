using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Account;
using Domain.DTOs.Category;
using Domain.DTOs.Common;
using Domain.DTOs.Role;
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

            CreateMap<Category, CategoryDto>()
                .IncludeBase<BaseEntity, BaseDto>()
                .ReverseMap();
            CreateMap<CreateCategoryRequest, Category>()
                .IncludeBase<BaseCreateRequest, BaseEntity>();
            CreateMap<UpdateCategoryRequest, Category>()
                .IncludeBase<BaseUpdateRequest, BaseEntity>();
        }
    }
}