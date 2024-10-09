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

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}