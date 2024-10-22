namespace Domain.Entities;

public class ScholarshipProgramCertificate
{
    public int? ScholarshipProgramId { get; set; }
    
    public ScholarshipProgram? ScholarshipProgram { get; set; }
    
    public int? CertificateId { get; set; }
    
    public Certificate? Certificate { get; set; }
}