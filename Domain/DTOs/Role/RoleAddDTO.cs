namespace Domain.DTOs.Role;

public record RoleAddDTO(
    string? Name,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    string? Status
);

// public class RoleAddDTO
// {
//     public string? Name { get; set; }
// }