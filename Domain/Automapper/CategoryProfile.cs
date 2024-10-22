using AutoMapper;
using Domain.DTOs.Category;
using Domain.DTOs.Common;
using Domain.Entities;

namespace Domain.Automapper;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>()
            .IncludeBase<BaseEntity, BaseDto>()
            .ReverseMap();
        
        CreateMap<CreateCategoryRequest, Category>();
        
        CreateMap<UpdateCategoryRequest, Category>();
    }
}