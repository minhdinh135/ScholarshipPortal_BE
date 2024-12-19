using System.Text;
using System.Text.Json.Serialization;
using Application.Common;
using Application.Interfaces.IServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using HealthChecks.UI.Client;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SSAP.API.Extensions;
using SSAP.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(options => options.SuppressInputFormatterBuffering = true)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<ScholarshipContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Db") ?? string.Empty));

builder.Services.AddMapperServices();

// Add FluentValidation validators
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

// Register services and inject dependencies
builder.Services.AddApplicationServices(builder.Configuration);

// Register exception handler service
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Add health check
builder.Services.AddHealthChecks()
    .AddMySql(builder.Configuration.GetConnectionString("Db")!, tags: ["database"]);
builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage();
    
//Add Jwt
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Scholarship API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    }
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""
                )),
            // RoleClaimType = "role",
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAuthorization();

//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://scholarship-portal-nu.vercel.app");
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


var app = builder.Build();

// Automatically apply pending migrations and update the database.
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<ScholarshipContext>();
//
//     // Check for pending migrations and apply them.
//     if (dbContext.Database.GetPendingMigrations().Any())
//     {
//         dbContext.Database.Migrate();
//     }
// }

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     var context = services.GetRequiredService<ScholarshipContext>();
//     var logger = services.GetService<ILogger<Program>>();
//
//     try
//     {
//         await context.Database.MigrateAsync();
//         await ScholarshipContextSeed.SeedAsync(context);
//     }
//     catch (Exception ex)
//     {
//         logger.LogError(ex, "An error occurred during migration or seeding.");
//     }
// }

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// app.MapHealthChecks("/health", new HealthCheckOptions
// {
//     ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
// });
// app.MapHealthChecksUI(config => config.UIPath="/healthcheck-ui");

app.UseCors("MyAllowPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.UseHangfireDashboard();
// app.MapHangfireDashboard("/hangfire");

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var backgroundService = serviceProvider.GetRequiredService<IBackgroundService>();

    RecurringJob.AddOrUpdate("ScheduleScholarshipsAfterDeadline", () => backgroundService.ScheduleScholarshipsAfterDeadline(), Cron.Daily);
    RecurringJob.AddOrUpdate("ScheduleApplicationsNeedExtend", () => backgroundService.ScheduleApplicationsNeedExtend(), Cron.Daily);
}

app.Run();