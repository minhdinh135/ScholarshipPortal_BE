﻿using AutoMapper;
using Domain.DTOs.Common;
using Domain.DTOs.Major;
using Domain.Entities;

namespace Domain.Automapper;

public class MajorProfile : Profile
{
    public MajorProfile()
    {
        CreateMap<Major, MajorDto>()
            .IncludeBase<BaseEntity, BaseDto>()
            .ReverseMap();

        CreateMap<CreateMajorRequest, Major>();

        CreateMap<UpdateMajorRequest, Major>();
    }
}