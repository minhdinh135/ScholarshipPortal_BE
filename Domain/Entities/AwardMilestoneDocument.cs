﻿namespace Domain.Entities;

public class AwardMilestoneDocument : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Type { get; set; }
    
    public string? FileUrl { get; set; }
    
    public int? AwardMilestoneId { get; set; }    
    
    public AwardMilestone? AwardMilestone { get; set; }
}