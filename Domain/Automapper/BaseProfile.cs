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
            .ReverseMap();

        CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>));
    }
}