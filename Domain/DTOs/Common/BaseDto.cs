namespace Domain.DTOs.Common;

public class BaseDto
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Status { get; set; }
}