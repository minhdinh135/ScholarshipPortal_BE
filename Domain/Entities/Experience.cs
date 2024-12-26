using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Experience : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public int FromYear { get; set; }
    
    public int ToYear { get; set; }
    
    public int ApplicantProfileId { get; set; }

    public ApplicantProfile ApplicantProfile { get; set; } 
}