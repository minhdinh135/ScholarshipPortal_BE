﻿using AutoMapper;
using Domain.DTOs.Common;
using Domain.DTOs.ScholarshipProgram;
using Domain.Entities;

namespace Domain.Automapper;

public class ScholarshipProgramProfile : Profile
{
    public ScholarshipProgramProfile()
    {
        CreateMap<ScholarshipProgram, ScholarshipProgramDto>()
            .ForMember(dest => dest.Universities, opt =>
                    opt.MapFrom(src => src.ScholarshipProgramUniversities.Select(spu => spu.University)))
            .ForMember(dest => dest.Majors, opt =>
                opt.MapFrom(src => src.ScholarshipProgramMajors.Select(spm => spm.Major)))
            .ReverseMap();

        CreateMap<CreateScholarshipProgramRequest, ScholarshipProgram>()
            .ForMember(dest => dest.ScholarshipProgramUniversities,
                opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                if (src.UniversityIds != null)
                {
                    dest.ScholarshipProgramUniversities = src.UniversityIds.Select(universityId =>
                        new ScholarshipProgramUniversity()
                        {
                            UniversityId = universityId
                        }).ToList();
                }
            })
            .ForMember(dest => dest.ScholarshipProgramMajors,
                opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                if (src.MajorIds != null)
                {
                    dest.ScholarshipProgramMajors = src.MajorIds.Select(majorId =>
                        new ScholarshipProgramMajor()
                        {
                            MajorId = majorId
                        }).ToList();
                }
            });

        CreateMap<UpdateScholarshipProgramRequest, ScholarshipProgram>()
            .ForMember(dest => dest.ScholarshipProgramUniversities,
                opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                if (src.UniversityIds != null)
                {
                    foreach (var universityId in src.UniversityIds)
                    {
                        dest.ScholarshipProgramUniversities.Add(new ScholarshipProgramUniversity()
                        {
                            ScholarshipProgramId = dest.Id,
                            UniversityId = universityId
                        });
                    }
                }
            })
            .ForMember(dest => dest.ScholarshipProgramMajors,
                opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                if (src.MajorIds != null)
                {
                    foreach (var majorId in src.MajorIds)
                    {
                        dest.ScholarshipProgramMajors.Add(new ScholarshipProgramMajor()
                        {
                            ScholarshipProgramId = dest.Id,
                            MajorId = majorId
                        });
                    }
                }
            });
    }
}