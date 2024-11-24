namespace Domain.DTOs.Expert;

public class CreateExpertDetailsDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Major { get; set; }
    
    public int? ExpertId { get; set; }


    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public string? AvatarUrl { get; set; }

    public bool? LoginWithGoogle { get; set; }

    public string? Status { get; set; }

    public int? FunderId { get; set; }

    public int? RoleId { get; set; }
}