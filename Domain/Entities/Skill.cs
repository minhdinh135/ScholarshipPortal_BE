namespace Domain.Entities;

public class Skill : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Type { get; set; }
    
    public ICollection<MajorSkill>? MajorSkills { get; set; }
}