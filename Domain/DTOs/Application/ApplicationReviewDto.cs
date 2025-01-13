namespace Domain.DTOs.Application;

public class ApplicationReviewDto
{
    public int Id { get; set; }
    
    public string ApplicantName { get; set; }
    
    public string Description { get; set; }
    
    public float Score { get; set; }

    public string Comment { get; set; }

    public DateTime ReviewDate { get; set; }

    public string Status { get; set; }

    public int ExpertId { get; set; }
    
    public int ApplicationId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}
