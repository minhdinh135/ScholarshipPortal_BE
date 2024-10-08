using AutoMapper;
using Domain.Automapper;

namespace SSAP.API.Extensions;

public static class MapperExtension
{
    public static IServiceCollection AddMapperServices(this IServiceCollection services)
    {
        services.AddSingleton<IMapper>(sp =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BaseProfile>();
                cfg.AddProfile<CategoryProfile>();
                cfg.AddProfile<CriteriaProfile>();
                cfg.AddProfile<MajorProfile>();
                cfg.AddProfile<ScholarshipProgramProfile>();
                cfg.AddProfile<MappingProfile>();
            });

            return config.CreateMapper();
        });
        
        return services;
    }
}