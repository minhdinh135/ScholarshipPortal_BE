namespace Domain.DTOs.Application;

public record ApplicationAddDTO(
    DateTime? AppliedDate,
    int? ApplicantId,
    int? ScholarshipProgramId,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    string? Status
);
