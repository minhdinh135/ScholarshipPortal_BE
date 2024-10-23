using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Account;

public class AccountUpdateDTO{
    [Key]
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? HashedPassword { get; set; }

    public string? Address { get; set; }

    public string? AvatarUrl { get; set; }

    public bool? LoginWithGoogle { get; set; }

    public string? Status { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}
