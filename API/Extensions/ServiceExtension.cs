using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Services;
using Infrastructure.ExternalServices.Gemini;
using Infrastructure.ExternalServices.Google;
using Infrastructure.ExternalServices.Password;
using Infrastructure.ExternalServices.Token;
using Infrastructure.Repositories;

namespace SSAP.API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ITokenService, JwtService>();

        services.AddScoped<IScholarshipProgramService, ScholarshipProgramService>();
        services.AddScoped<IScholarshipProgramRepository, ScholarshipProgramRepository>();
        
        services.AddScoped<ICriteriaService, CriteriaService>();
        services.AddScoped<ICriteriaRepository, CriteriaRepository>();

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IMajorService, MajorService>();
        services.AddScoped<IMajorRepository, MajorRepository>();

        services.AddHttpClient<GeminiService>();
        services.AddSingleton(sp => new GeminiService(
            sp.GetRequiredService<HttpClient>(),
            config.GetSection("OpenAI").GetSection("ApiKey").Value ?? string.Empty
        ));

        services.AddScoped<GoogleService>(s => new GoogleService(
            config.GetSection("Google").GetSection("ClientId").Value ?? string.Empty,
            config.GetSection("Google").GetSection("ClientSecret").Value ?? string.Empty,
            config.GetSection("Google").GetSection("RedirectUri").Value ?? string.Empty
        ));
        
        return services;
    }
}