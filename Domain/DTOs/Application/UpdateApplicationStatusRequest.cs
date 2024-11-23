namespace Domain.DTOs.Application;

public class UpdateApplicationStatusRequest
{
    public string Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

}
