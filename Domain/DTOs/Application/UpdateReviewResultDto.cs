namespace Domain.DTOs.Application;

public class UpdateReviewResultDto
{
    public int ApplicationReviewId { get; set; }
    public string Comment { get; set; }
    
    public bool IsPassed { get; set; }

    public float Score { get; set; }
    
    public bool IsFirstReview { get; set; }
}
