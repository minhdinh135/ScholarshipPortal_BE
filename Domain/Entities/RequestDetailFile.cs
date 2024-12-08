using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class RequestDetailFile : BaseEntity
{
    [MaxLength(1024)]
    public string FileUrl { get; set; }
    
    [MaxLength(100)]
    public string UploadedBy { get; set; }
    
    public DateTime UploadDate { get; set; }
    
    public int RequestDetailId { get; set; }
    
    public RequestDetail RequestDetail { get; set; }
}