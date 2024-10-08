namespace Domain.DTOs.University;

public class UniversityResponse
{
    public int? Id { get; set; }
    
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? City { get; set; }

    public int? CountryId { get; set; }
}