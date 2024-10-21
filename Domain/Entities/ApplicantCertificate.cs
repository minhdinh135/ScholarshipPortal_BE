namespace Domain.Entities;

public class ApplicantCertificate : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Type { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public int? ApplicantProfileId { get; set; }
    
    public ApplicantProfile? ApplicantProfile { get; set; }
}