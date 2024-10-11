using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Account;

public class AccountAddDTO {
    [Required]
    public string Username { get; set; }

    [Required]
    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? HashedPassword { get; set; }

    public string? Address { get; set; }

    public string? Avatar { get; set; }

    public string? Gender { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    [Required]
    public string? Status { get; set; }
}
