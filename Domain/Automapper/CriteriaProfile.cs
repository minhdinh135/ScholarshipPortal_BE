using AutoMapper;
using Domain.DTOs.Common;
using Domain.DTOs.Criteria;
using Domain.Entities;

namespace Domain.Automapper;

public class CriteriaProfile : Profile
{
    public CriteriaProfile()
    {
        CreateMap<Criteria, CriteriaDto>()
            .IncludeBase<BaseEntity, BaseDto>()
            .ReverseMap();

        CreateMap<CreateCriteriaRequest, Criteria>();

        CreateMap<UpdateCriteriaRequest, Criteria>();
    }
}