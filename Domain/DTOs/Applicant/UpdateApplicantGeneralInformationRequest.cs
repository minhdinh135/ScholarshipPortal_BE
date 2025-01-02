﻿namespace Domain.DTOs.Applicant;

public class UpdateApplicantGeneralInformationRequest
{
    public DateTime BirthDate { get; set; }

    public string Gender { get; set; }

    public string Nationality { get; set; }
    
    public string Ethnicity { get; set; }
    
    public string Bio { get; set; }
}