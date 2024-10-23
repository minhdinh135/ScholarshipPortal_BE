namespace Domain.DTOs.Achievement;

public class UpdateAchievementDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? AchievedDate { get; set; }

    public int? ApplicantProfileId { get; set; }
}