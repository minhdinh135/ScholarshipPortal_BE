namespace Domain.Entities;

public class AwardMilestoneDocument : BaseEntity
{
    public string? Type { get; set; }
    
    public int? AwardMilestoneId { get; set; }    
    
    public AwardMilestone? AwardMilestone { get; set; }
}