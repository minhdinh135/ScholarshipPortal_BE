namespace Domain.Entities;

public class Achievement : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime? AchievedDate { get; set; }
    
    public int? ApplicantProfileId { get; set; }
    
    public ApplicantProfile? ApplicantProfile { get; set; }
}