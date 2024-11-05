namespace Domain.Entities;

public class Major : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public int? ParentMajorId { get; set; }
    
    public Major ParentMajor { get; set; }
    
    public ICollection<Major>? SubMajors { get; set; }
    
    public ICollection<ScholarshipProgramMajor>? ScholarshipProgramMajors { get; set; }
    
    public ICollection<MajorSkill>? MajorSkills { get; set; }
}