using Domain.DTOs.Common;

namespace Domain.DTOs.Notification;

public class NotificationAddDTO : BaseDto
{
    public string? Message { get; set; }
    
    public bool? IsRead { get; set; }
    
    public DateTime? SentDate { get; set; }
    
    public int? ReceiverId { get; set; }
}
