namespace Domain.Entities;

public class Notification : BaseEntity
{
    public required string Title { get; set; }

    public required string Body { get; set; }

    public string? Icon { get; set; }
    
    public DateTime? Time { get; set; }
    
    public string? Link { get; set; }

    public int AccountId { get; set; }
    
    public Account? Account { get; set; }
}
