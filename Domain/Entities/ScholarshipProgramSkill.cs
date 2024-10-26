namespace Domain.Entities;

public class ScholarshipProgramSkill
{
    public int? ScholarshipProgramId { get; set; }

    public ScholarshipProgram? ScholarshipProgram { get; set; }

    public int? SkillId { get; set; }

    public Skill? Skill { get; set; }
}