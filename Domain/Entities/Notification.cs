namespace Domain.Entities;

public class Notification : BaseEntity
{
    public string? Message { get; set; }
    
    public bool? IsRead { get; set; }
    
    public DateTime? SentDate { get; set; }
    
    public int? ReceiverId { get; set; }
    
    public Account? Receiver { get; set; }
}
