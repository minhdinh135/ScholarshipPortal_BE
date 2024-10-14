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
            var countriesData = File.ReadAllText(basePath + "/countries.json");
            var countries = JsonSerializer.Deserialize<List<Country>>(countriesData);
            context.Countries.AddRange(countries);
        }

        if (!context.Universities.Any())
        {
            var countriesData = File.ReadAllText(basePath + "/universities.json");
            var countries = JsonSerializer.Deserialize<List<University>>(countriesData);
            context.Universities.AddRange(countries);
        }

        if (!context.Majors.Any())
        {
            var countriesData = File.ReadAllText(basePath + "/majors.json");
            var countries = JsonSerializer.Deserialize<List<Major>>(countriesData);
            context.Majors.AddRange(countries);
        }

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}
