using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Role;

public class RoleUpdateDTO{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public string? Status { get; set; }
}
