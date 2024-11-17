namespace Domain.DTOs.Major;

public class MajorDto 
{
    public int? Id { get; set; }
    
    public string? Name { get; set; }

    public string? Description { get; set; }
    
    public List<MajorDto> SubMajors { get; set; }
    
    public List<SkillDto> Skills { get; set; }
}