using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Criteria : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public int ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram ScholarshipProgram { get; set; }
}