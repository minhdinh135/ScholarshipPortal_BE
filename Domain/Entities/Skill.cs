using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Skill : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    [MaxLength(100)]
    public string Type { get; set; }
    
    public ICollection<MajorSkill>? MajorSkills { get; set; }
}