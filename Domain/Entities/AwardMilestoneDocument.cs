using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class AwardMilestoneDocument : BaseEntity
{
    [MaxLength(100)]
    public string Type { get; set; }
    
    public int AwardMilestoneId { get; set; }    
    
    public AwardMilestone AwardMilestone { get; set; }
}