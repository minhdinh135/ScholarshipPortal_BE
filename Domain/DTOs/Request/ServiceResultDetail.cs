namespace Domain.DTOs.Request;

public class ServiceResultDetail
{
    public string? Comment { get; set; }

    public int? ServiceId { get; set; }

    public List<string> RequestFileUrls { get; set; }
}