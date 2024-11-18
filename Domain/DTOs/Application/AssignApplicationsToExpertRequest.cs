namespace Domain.DTOs.Application;

public class AssignApplicationsToExpertRequest
{
    public int ExpertId { get; set; }
    
    public List<int> ApplicationIds { get; set; }
}