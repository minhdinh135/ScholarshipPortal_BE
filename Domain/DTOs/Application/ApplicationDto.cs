namespace Domain.DTOs.Application;

public class ApplicationDto
{
    public int Id { get; set; }

    public DateTime? AppliedDate { get; set; }
    
    public string? Status { get; set; }
    
    public int? ApplicantId { get; set; }

    public int? ScholarshipProgramId { get; set; }
}