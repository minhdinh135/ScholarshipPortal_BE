namespace Domain.Entities;

public class Major : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public ICollection<ScholarshipProgramMajor>? ScholarshipProgramMajors { get; set; }
}