using Domain.DTOs.Common;

namespace Domain.DTOs.Notification;

public class NotificationDTO : BaseDto
{
    public int Id { get; set; }

    public string? Message { get; set; }
    
    public bool? IsRead { get; set; }
    
    public DateTime? SentDate { get; set; }
    
    public int? ReceiverId { get; set; }
}
