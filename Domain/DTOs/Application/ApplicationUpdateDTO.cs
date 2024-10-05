namespace Domain.DTOs.Application;

public record ApplicationUpdateDTO(
    int Id,
    DateTime? AppliedDate,
    int? ApplicantId,
    int? ScholarshipProgramId,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    string? Status
);
