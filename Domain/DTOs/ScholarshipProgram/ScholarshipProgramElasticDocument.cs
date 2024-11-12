namespace Domain.DTOs.ScholarshipProgram;

public class ScholarshipProgramElasticDocument
{
    public int Id { get; set; }

    public string? Name { get; set; }
    
    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public decimal? ScholarshipAmount { get; set; }

    public int? NumberOfScholarships { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Status { get; set; }
}