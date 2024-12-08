using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class FunderProfile : BaseEntity
{
    [MaxLength(100)]
    public string OrganizationName { get; set; }
    
    [MaxLength(100)]
    public string? ContactPersonName { get; set; }
    
    public int FunderId { get; set; }
    
    public Account Funder { get; set; }
    
    public ICollection<FunderDocument>? FunderDocuments { get; set; }
}