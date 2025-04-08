using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ApplicationDocument : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(100)]
    public string Type { get; set; }
    
    [MaxLength(1024)]
    public string FileUrl { get; set; }
    
    public int ApplicationId { get; set; }
    
    public Application Application { get; set; }
}