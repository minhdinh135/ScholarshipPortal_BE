using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Request : BaseEntity
{
    [MaxLength(200)]
    public string Description { get; set; }
    
    public DateTime RequestDate { get; set; }
    
    [MaxLength(100)]
    public string Status { get; set; }
    
    public int ApplicantId { get; set; }
    
    public Account Applicant { get; set; }
    
    public ICollection<RequestDetail> RequestDetails { get; set; }
}