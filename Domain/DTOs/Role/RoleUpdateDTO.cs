namespace Domain.DTOs.Role;

public record RoleUpdateDTO(
    int Id,
    string? Name,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    string? Status
);

// public class RoleUpdateDTO
// {
//     public string? Name { get; set; }
// }
