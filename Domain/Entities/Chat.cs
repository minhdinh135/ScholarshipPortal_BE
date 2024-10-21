namespace Domain.Entities;

public class Chat : BaseEntity
{
    public string? Message { get; set; }
    
    public DateTime? SentDate { get; set; }
    
    public int? SenderId { get; set; }
    
    public Account? Sender { get; set; }
    
    public int? ReceiverId { get; set; }
    
    public Account? Receiver { get; set; }
}