namespace Domain.DTOs.Application;

public class AssignExpertsToApplicationDto
{
    public int ApplicationId { get; set; }
    
    public DateTime ReviewDate { get; set; }
    
    public bool IsFirstReview { get; set; }
    
    public List<int> ExpertIds { get; set; }
}
