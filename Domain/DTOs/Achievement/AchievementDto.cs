namespace Domain.DTOs.Achievement;

public class AchievementDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? AchievedDate { get; set; }

    public int? ApplicantProfileId { get; set; }
}