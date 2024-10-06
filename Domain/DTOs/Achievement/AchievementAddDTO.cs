namespace Domain.DTOs.Achievement;

public record AchievementAddDTO(
    string? Name,
    string? Description,
    DateTime? AchievedDate,
    int? ApplicantProfileId,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    string? Status
);
