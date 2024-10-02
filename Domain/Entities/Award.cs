namespace Domain.Entities;

public class Award : BaseEntity
{
    public string? Description { get; set; }    
    
    public decimal? Amount { get; set; }
    
    public string? Image { get; set; }
    
    public DateTime? AwardedDate { get; set; }
    
    public int? ApplicationId { get; set; }
    
    public Application? Application { get; set; }
}