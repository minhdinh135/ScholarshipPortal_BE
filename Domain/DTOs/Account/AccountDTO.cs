namespace Domain.DTOs.Account;

public class AccountDTO{
    public int Id { get; set; }

    public string Username { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? HashedPassword { get; set; }

    public string? Address { get; set; }

    public string? Avatar { get; set; }

    public string? Gender { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public string? Status { get; set; }
}
