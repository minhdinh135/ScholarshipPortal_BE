namespace Domain.DTOs.Application;

public class AssignApplicationsToExpertRequest
{
    public int ExpertId { get; set; }
    
    public DateTime ReviewDate { get; set; }
    
    public bool IsFirstReview { get; set; }
    
    public List<int> ApplicationIds { get; set; }
}