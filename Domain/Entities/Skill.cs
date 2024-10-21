﻿namespace Domain.Entities;

public class Skill : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Type { get; set; }
    
    public int? ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram? ScholarshipProgram { get; set; }
}