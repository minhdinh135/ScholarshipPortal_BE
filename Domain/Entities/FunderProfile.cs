namespace Domain.Entities;

public class FunderProfile : BaseEntity
{
    public string? OrganizationName { get; set; }
    
    public string? ContactPersonName { get; set; }
    
    public int? FunderId { get; set; }
    
    public Account? Funder { get; set; }
    
    public ICollection<FunderDocument>? FunderDocuments { get; set; }
}