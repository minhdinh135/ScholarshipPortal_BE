using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Review : BaseEntity
{
    [MaxLength(200)]
    public string? Description { get; set; }
    
    public int? Score { get; set; }
    
    [MaxLength(200)]
    public string? Comment { get; set; }
    
    public DateTime ReviewDate { get; set; }
    
    [MaxLength(100)]
    public string Status { get; set; }
    
    public int ExpertId { get; set; }
    
    public Account Expert { get; set; }
    
    public int ApplicationId { get; set; }
    
    public Application Application { get; set; }
}