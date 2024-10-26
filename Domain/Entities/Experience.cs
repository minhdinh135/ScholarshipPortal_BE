namespace Domain.Entities;

public class Experience : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public int? ApplicantProfileId { get; set; }
    
    public ApplicantProfile? ApplicantProfile { get; set; }
}