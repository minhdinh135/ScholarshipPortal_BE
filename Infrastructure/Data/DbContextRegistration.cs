using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data;
public static class DbContextRegistration
{
    public static IServiceCollection AddScholarshipDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ScholarshipContext>(options =>
            options.UseMySQL(configuration.GetConnectionString("Db") ?? string.Empty));
        return services;
    }
}
