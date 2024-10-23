namespace Domain.DTOs.Application;

public class AddApplicationDto
{
    public DateTime? AppliedDate { get; set; }

    public int ApplicantId { get; set; }

    public int ScholarshipProgramId { get; set; }

    public string? Status { get; set; }
}