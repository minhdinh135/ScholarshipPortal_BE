using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Services;
using Infrastructure.ExternalServices.Cloudinary;
using Infrastructure.ExternalServices.Elastic;
using Infrastructure.ExternalServices.Email;
using Infrastructure.ExternalServices.Gemini;
using Infrastructure.ExternalServices.Google;
using Infrastructure.ExternalServices.Password;
using Infrastructure.ExternalServices.Token;
using Infrastructure.Repositories;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Infrastructure.ExternalServices.Notification;
using Infrastructure.ExternalServices.Chat;
using Infrastructure.ExternalServices.PDF;
using Infrastructure.ExternalServices.Stripe;

namespace SSAP.API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ITokenService, JwtService>();

        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddScoped<IProviderService, ProviderService>();
        services.AddScoped<IProviderRepository, ProviderRepository>();

        services.AddScoped<IFunderService, FunderService>();
        services.AddScoped<IFunderRepository, FunderRepository>();

        services.AddScoped<IApplicantService, ApplicantService>();
        services.AddScoped<IApplicantRepository, ApplicantRepository>();

        services.AddScoped<IScholarshipProgramService, ScholarshipProgramService>();
        services.AddScoped<IScholarshipProgramRepository, ScholarshipProgramRepository>();

        services.AddScoped<ICriteriaService, CriteriaService>();
        services.AddScoped<ICriteriaRepository, CriteriaRepository>();

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IMajorService, MajorService>();
        services.AddScoped<IMajorRepository, MajorRepository>();

        services.AddScoped<IUniversityService, UniversityService>();
        services.AddScoped<IUniversityRepository, UniversityRepository>();
        
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICountryRepository, CountryRepository>();

        services.AddScoped<ICertificateService, CertificateService>();
        services.AddScoped<ICertificateRepository, CertificateRepository>();

        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<ISkillRepository, SkillRepository>();

        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();

        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        services.AddScoped<IRequestService, RequestService>();
        services.AddScoped<IRequestRepository, RequestRepository>();

        services.AddScoped<IFeedbackService, FeedbackService>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
		
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

        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddScoped<ICloudinaryService, CloudinaryService>();

        services.AddScoped<IEmailService, EmailService>();
        
        services.AddScoped<IPdfService, PdfService>();

        services.Configure<ElasticSettings>(config.GetSection("ElasticSettings"));
        services.AddSingleton(typeof(IElasticService<>), typeof(ElasticService<>));

        services.Configure<StripeSettings>(config.GetSection("StripeSettings"));
        services.AddScoped<IStripeService, StripeService>();

        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("./firebase-adminsdk.json"),
        });

        services.AddScoped<INotificationService, NotificationsService>();
        services.AddScoped<IReviewMilestoneService, ReviewMilestoneService>();
        services.AddScoped<IReviewMilestoneRepository, ReviewMilestoneRepository>();
        services.AddScoped<IApplicationDocumentService, ApplicationDocumentService>();
        return services;
    }
}
