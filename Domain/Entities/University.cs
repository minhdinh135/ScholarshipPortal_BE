using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class University : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    [MaxLength(100)]
    public string City { get; set; }
    
    public int CountryId { get; set; }
   
    public Country Country { get; set; }
    
    public ICollection<ScholarshipProgram>? ScholarshipPrograms { get; set; }
}