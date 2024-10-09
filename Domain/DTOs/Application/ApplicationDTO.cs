namespace Domain.DTOs.Application;

public class ApplicationDTO{
    public int Id { get; set; }

    public DateTime? AppliedDate { get; set; }

    public int ApplicantId { get; set; }

    public int ScholarshipProgramId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public string? Status { get; set; }
}
