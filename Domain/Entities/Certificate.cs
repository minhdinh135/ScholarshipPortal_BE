using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Certificate : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    [MaxLength(100)]
    public string Type { get; set; }
    
    public ICollection<ScholarshipProgramCertificate> ScholarshipProgramCertificates { get; set; }
}