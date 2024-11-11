namespace Domain.Entities;

public class MajorSkill : BaseEntity
{
    public int? MajorId { get; set; }
    
    public Major? Major { get; set; }
    
    public int? SkillId { get; set; }
    
    public Skill? Skill { get; set; }
    
    public int? ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram? ScholarshipProgram { get; set; }
}