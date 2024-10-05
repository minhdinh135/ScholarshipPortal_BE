namespace Domain.DTOs.Achievement;

public record AchievementUpdateDTO(
    int Id,
    string? Name,
    string? Description,
    DateTime? AchievedDate,
    int? ApplicantProfileId,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    string? Status
);
