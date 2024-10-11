using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Common;
using Domain.Entities;

namespace Domain.Automapper;

public class BaseProfile : Profile
{
    public BaseProfile()
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

        CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>));
    }
}