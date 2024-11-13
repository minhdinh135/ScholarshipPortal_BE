namespace Domain.DTOs.ScholarshipProgram;

public class ScholarshipSearchOptions
{
    public string Name { get; set; } = "scholarship";

    public decimal ScholarshipMinAmount { get; set; } = 500;

    public decimal ScholarshipMaxAmount { get; set; } = 100000;

    public string CategoryName { get; set; } = "merit-based";

    public string Status { get; set; } = "open";
    
    public DateTime Deadline { get; set; } = DateTime.Now;
}