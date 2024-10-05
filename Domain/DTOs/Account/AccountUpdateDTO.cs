namespace Domain.DTOs.Account;

public record AccountUpdateDTO(
    int Id,
    string? Username,
    string? FullName,
    string? PhoneNumber,
    string? Email,
    string? HashedPassword,
    string? Address,
    string? Avatar,
    string? Gender,
    int? RoleId
);

// public class UserUpdateDTO
// {
//     public string? UserName { get; set; }
//
//     public string? FullName { get; set; }
//
//     public string? PhoneNumber { get; set; }
//
//     public string? Email { get; set; }
//
//     public string? HashedPassword { get; set; }
//
//     public string? Address { get; set; }
//
//     public string? Avatar { get; set; }
//
//     public string? Gender { get; set; }
//
//     public int? RoleId { get; set; }
// }
