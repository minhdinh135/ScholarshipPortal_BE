namespace Domain.DTOs.ScholarshipProgram;

public class ScholarshipSearchOptions
{
    public string? Name { get; set; }  

    public decimal ScholarshipMinAmount { get; set; } = 500;

    public decimal ScholarshipMaxAmount { get; set; } = 100000;

    public string? CategoryName { get; set; } 

    public string? Status { get; set; } 
    
    public DateTime? Deadline { get; set; } 
}