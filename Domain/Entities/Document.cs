namespace Domain.Entities;

public class Document : BaseEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Content { get; set; }
    
    public string? Type { get; set; }
    
    public string? FilePath { get; set; }
    
    public int? ApplicationId { get; set; }
    
    public Application? Application { get; set; }
}