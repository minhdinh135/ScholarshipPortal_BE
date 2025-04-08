namespace Domain.DTOs.Request;

public class RequestFile
{
    public string? FileUrl { get; set; }
    
    public string? UploadedBy { get; set; }
    
    public DateTime? UploadDate { get; set; }
}