using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Country : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    public int Code { get; set; }
    
    public ICollection<University>? Universities { get; set; }
}