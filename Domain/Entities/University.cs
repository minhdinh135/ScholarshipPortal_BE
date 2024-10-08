namespace Domain.Entities;

public class University : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? City { get; set; }
    
    public int? CountryId { get; set; }
   
    public Country? Country { get; set; }
    
    public ICollection<ScholarshipProgramUniversity>? ScholarshipProgramUniversities { get; set; }
}