namespace Domain.DTOs.Application;

public class ExtendApplicationDto
{
    public int ApplicationId { get; set; }

    public List<AddApplicationDocumentDto> Documents { get; set; }
}
