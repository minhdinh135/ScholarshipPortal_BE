using Application.Helper;
using Domain.Entities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infrastructure.Data;

public class ScholarshipContextSeed
{
    public static async Task SeedAsync(ScholarshipContext context)
    {
        var basePath = "../Infrastructure/SeedData";
        
        if (!context.Categories.Any())
        {
            var categoriesData = File.ReadAllText(basePath + "/categories.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
            context.Categories.AddRange(categories);
        }

        if (!context.Countries.Any())
        {
            var categories = CsvUtils.ReadFile<Country>(basePath + "/countries.csv");
            context.Countries.AddRange(categories);
        }
        
        if (!context.Universities.Any())
        {
            var universitiesData = File.ReadAllText(basePath + "/universities.json");
            var universities = JsonSerializer.Deserialize<List<University>>(universitiesData);
            context.Universities.AddRange(universities);
        }

        if (!context.Majors.Any())
        {
            var majorsData = File.ReadAllText(basePath + "/majors.json");
            var majors = JsonSerializer.Deserialize<List<Major>>(majorsData);
            context.Majors.AddRange(majors);
        }

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}