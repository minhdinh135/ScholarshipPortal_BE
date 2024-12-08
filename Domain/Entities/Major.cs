using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Major : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public int? ParentMajorId { get; set; }
    
    public Major? ParentMajor { get; set; }
    
    public ICollection<Major>? SubMajors { get; set; }
    
    public ICollection<MajorSkill>? MajorSkills { get; set; }
    
    public ICollection<ScholarshipProgram>? ScholarshipPrograms { get; set; }
}