using System.Text;
using System.Text.Json.Serialization;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Services;
using AutoMapper;
using Domain.Automapper;
using Infrastructure.Data;
using Infrastructure.ExternalServices.Gemini;
using Infrastructure.ExternalServices.Google;
using Infrastructure.ExternalServices.Password;
using Infrastructure.ExternalServices.Token;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(options => options.SuppressInputFormatterBuffering = true)
  .AddJsonOptions(options => {
      options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      options.JsonSerializerOptions.MaxDepth = 64;
  });

builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Not throw errors imidiately
builder.Services.Configure<ApiBehaviorOptions>(options
    => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Scholarship API", Version = "v1" });
});

builder.Services.AddDbContext<ScholarshipContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Db") ?? string.Empty));

//Add autoMapper
builder.Services.AddSingleton<IMapper>(sp =>
    {
    var config = new MapperConfiguration(cfg =>
        {
        // Configure your mapping profiles
        cfg.AddProfile<MappingProfile>();
        });

    return config.CreateMapper();
    });

//Repository injection
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//Service injection
builder.Services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<AuthService>();

// Add external services
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, JwtService>();

builder.Services.AddHttpClient<GeminiService>();
builder.Services.AddSingleton(sp => new GeminiService(
    sp.GetRequiredService<HttpClient>(),
    builder.Configuration["OpenAI:ApiKey"]
));

builder.Services.AddScoped<GoogleService>(s => new GoogleService(
      builder.Configuration["Google:ClientId"],
      builder.Configuration["Google:ClientSecret"],
      builder.Configuration["Google:RedirectUri"]
  ));

//Add Jwt
builder.Services.AddSwaggerGen(
    c => {
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
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]
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
builder.Services.AddCors(options => {
    options.AddPolicy(name: "MyAllowPolicy", policy =>{
            //policy.WithOrigins("https://locovn.azurewebsites.net", "https://test-payment.momo.vn")
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
            });  
});


var app = builder.Build();

// Automatically apply pending migrations and update the database.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ScholarshipContext>();

    // Check for pending migrations and apply them.
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyAllowPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.Run();
