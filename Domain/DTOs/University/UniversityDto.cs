using Domain.DTOs.Country;

namespace Domain.DTOs.University;

public class UniversityDto
{
    public int? Id { get; set; }
    
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? City { get; set; }

    public CountryDto Country { get; set; }
}