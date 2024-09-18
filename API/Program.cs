using Infrastructure.Data;
using Infrastructure.ExternalService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Scholarship API", Version = "v1" });
});

//Add dbContext
builder.Services.AddScholarshipDbContext(builder.Configuration);

//Console.WriteLine(builder.Configuration["OpenAI:ApiKey"].ToString());
builder.Services.AddHttpClient<GeminiService>();
builder.Services.AddSingleton(sp => new GeminiService(
    sp.GetRequiredService<HttpClient>(),
    builder.Configuration["OpenAI:ApiKey"]
));

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

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
