namespace Domain.Entities;

public class Certificate : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Type { get; set; }
    
    public int? ScholarshipProgramId { get; set; }
    
    public ICollection<ScholarshipProgramCertificate> ScholarshipProgramCertificates { get; set; }
}