namespace Domain.DTOs.Application;

public class AssignExpertsToApplicationDto
{
    public int ApplicationId { get; set; }
    
    public bool IsFirstReview { get; set; }
    
    public List<ExpertListDto> ExpertIds { get; set; }
}

public class ExpertListDto
{
    public int Id { get; set; }    
    public DateTime? DeadlineDate { get; set; }
}
