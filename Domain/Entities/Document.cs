using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Document : BaseEntity
{
    [MaxLength(200)]
    public string Type { get; set; }
    
    public bool IsRequired { get; set; }
    
    public int ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram ScholarshipProgram { get; set; }
}