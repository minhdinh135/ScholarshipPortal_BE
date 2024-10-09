using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Achievement;

public record AchievementAddDTO{
    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? AchievedDate { get; set; }

    public int? ApplicantProfileId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt  { get; set; } = DateTime.Now;

    public string? Status { get; set; }
};
