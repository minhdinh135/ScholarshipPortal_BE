namespace Domain.DTOs.University;

public class AddUniversityDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? City { get; set; }
    public int? CountryId { get; set; }
}