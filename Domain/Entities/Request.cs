namespace Domain.Entities;

public class Request : BaseEntity
{
    public string? Description { get; set; }
    
    public DateTime? RequestDate { get; set; }
    
    public string? Status { get; set; }
    
    public int? ApplicantId { get; set; }
    
    public Account? Applicant { get; set; }
    
    public int? ServiceId { get; set; }
    
    public Service? Service { get; set; }
}