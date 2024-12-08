namespace Domain.Entities;

public class MajorSkill 
{
    public int MajorId { get; set; }
    
    public Major Major { get; set; }
    
    public int SkillId { get; set; }
    
    public Skill Skill { get; set; }
}