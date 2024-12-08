namespace Domain.DTOs.Application;

public class AssignApplicationsToExpertRequest
{
    public int ExpertId { get; set; }
    
    public DateTime ReviewDate { get; set; }
    
    public string Description { get; set; }
    
    public List<int> ApplicationIds { get; set; }
}