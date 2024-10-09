using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Application;

public class ApplicationUpdateDTO{
    [Key]
    public int Id { get; set; }

    public DateTime? AppliedDate { get; set; }

    [Required]
    public int ApplicantId { get; set; }

    [Required]
    public int ScholarshipProgramId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public string? Status { get; set; }
}
