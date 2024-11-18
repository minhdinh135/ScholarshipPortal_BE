namespace Domain.DTOs.Application;

public class AddApplicationDto
{
    public int ApplicantId { get; set; }

    public int ScholarshipProgramId { get; set; }

    public List<AddApplicationDocumentDto> Documents { get; set; }
}
