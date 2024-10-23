namespace Domain.Entities;

public class ApplicationDocument : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Type { get; set; }
    
    public string? FileUrl { get; set; }
    
    public int? ApplicationId { get; set; }
    
    public Application? Application { get; set; }
}