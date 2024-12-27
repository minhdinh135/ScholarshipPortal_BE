using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Services;
using Domain.DTOs.Account;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Hangfire;
using Hangfire.MySql;
using Infrastructure.ExternalServices.Chat;
using Infrastructure.ExternalServices.Cloudinary;
using Infrastructure.ExternalServices.Email;
using Infrastructure.ExternalServices.Google;
using Infrastructure.ExternalServices.Notification;
using Infrastructure.ExternalServices.Password;
using Infrastructure.ExternalServices.PDF;
using Infrastructure.ExternalServices.Stripe;
using Infrastructure.ExternalServices.Token;
using Infrastructure.Repositories;
using BackgroundService = Infrastructure.ExternalServices.Background.BackgroundService;

namespace SSAP.API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.Configure<AdminAccount>(config.GetSection("AdminAccount"));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ITokenService, JwtService>();

        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddScoped<IWalletRepository, WalletRepository>();

        services.AddScoped<IProviderService, ProviderService>();
        services.AddScoped<IProviderRepository, ProviderRepository>();

        services.AddScoped<IFunderService, FunderService>();
        services.AddScoped<IFunderRepository, FunderRepository>();

        services.AddScoped<IExpertService, ExpertService>();
        services.AddScoped<IExpertRepository, ExpertRepository>();

        services.AddScoped<IApplicantService, ApplicantService>();
        services.AddScoped<IApplicantRepository, ApplicantRepository>();
        services.AddScoped<IExperienceRepository, ExperienceRepository>();

        services.AddScoped<IScholarshipProgramService, ScholarshipProgramService>();
        services.AddScoped<IScholarshipProgramRepository, ScholarshipProgramRepository>();
        services.AddScoped<IProgramExpertRepository, ProgramExpertRepository>();

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
        services.AddScoped<IApplicationReviewRepository, ApplicationReviewRepository>();
        
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        services.AddScoped<IRequestService, RequestService>();
        services.AddScoped<IRequestRepository, RequestRepository>();

        services.AddScoped<IFeedbackService, FeedbackService>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();

        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IChatMessageRepository, ChatMessageRepository>();

        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.Configure<GoogleSettings>(config.GetSection("GoogleSettings"));
        services.AddScoped<IGoogleService, GoogleService>();
        
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddScoped<ICloudinaryService, CloudinaryService>();

        services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<IPdfService, PdfService>();

        services.Configure<StripeSettings>(config.GetSection("StripeSettings"));
        services.AddScoped<IStripeService, StripeService>();

        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseStorage(new MySqlStorage(
                config.GetConnectionString("Db"),
                new MySqlStorageOptions()
            )));
        services.AddHangfireServer();
        services.AddScoped<IBackgroundService, BackgroundService>();

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile("./firebase-adminsdk.json"),
        });
        services.AddScoped<IFirebaseNotificationService, FirebaseNotificationService>();

        services.AddScoped<INotificationService, NotificationsService>();
        services.AddScoped<IReviewMilestoneService, ReviewMilestoneService>();
        services.AddScoped<IReviewMilestoneRepository, ReviewMilestoneRepository>();
        services.AddScoped<IAwardMilestoneService, AwardMilestoneService>();
        return services;
    }
}
