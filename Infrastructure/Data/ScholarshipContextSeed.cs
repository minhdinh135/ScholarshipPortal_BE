﻿using Application.Helper;
using Domain.Entities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infrastructure.Data;

public class ScholarshipContextSeed
{
    public static async Task SeedAsync(ScholarshipContext context)
    {
        var basePath = "../Infrastructure/SeedData";

        if (!context.Roles.Any())
        {
            var rolesData = File.ReadAllText(basePath + "/roles.json");
            var roles = JsonSerializer.Deserialize<List<Role>>(rolesData);
            context.Roles.AddRange(roles);
        }
        
        if (!context.Categories.Any())
        {
            var categoriesData = File.ReadAllText(basePath + "/categories.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
            context.Categories.AddRange(categories);
        }

        if (!context.Countries.Any())
        {
            // var countriesData = File.ReadAllText(basePath + "/countries.json");
            // var countries = JsonSerializer.Deserialize<List<Country>>(countriesData);

            var countries = CsvUtils.ReadFile<Country>(basePath + "/countries.csv");
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
            var majorsData = File.ReadAllText(basePath + "/majors.json");
            var majors = JsonSerializer.Deserialize<List<Major>>(majorsData);
            context.Majors.AddRange(majors);
        }

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}
