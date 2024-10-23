using Domain.DTOs.Common;

namespace Domain.DTOs.Notification;

public class NotificationAddDTO : BaseDto
{
    public required string Title { get; set; }

    public required string Body { get; set; }

    public string? Icon { get; set; }
    
    public DateTime? Time { get; set; }
    
    public string? Link { get; set; }
    
    public string? Status { get; set; }

    public int AccountId { get; set; }
}
