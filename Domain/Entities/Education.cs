using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Education : BaseEntity
{
    [MaxLength(100)]
    public string EducationLevel { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public int FromYear { get; set; }
    
    public int ToYear { get; set; }
    
    [MaxLength(200)]
    public string Major { get; set; }
    
    public double Gpa { get; set; }
    
    [MaxLength(200)]
    public string School { get; set; }
    
    public int ApplicantProfileId { get; set; }
    
    public ApplicantProfile ApplicantProfile { get; set; }
}