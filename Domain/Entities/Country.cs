namespace Domain.Entities;

public class Country : BaseEntity
{
    public string? Name { get; set; }
    
    public int? Code { get; set; }
    
    public ICollection<University> Universities { get; set; }
    
}