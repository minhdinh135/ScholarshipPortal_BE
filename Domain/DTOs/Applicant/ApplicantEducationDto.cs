﻿namespace Domain.DTOs.Applicant;

public class ApplicantEducationDto
{
    public int Id { get; set; }
    
    public string EducationLevel { get; set; }

    public string? Description { get; set; }

    public int FromYear { get; set; }

    public int ToYear { get; set; }

    public string Major { get; set; }

    public double Gpa { get; set; }

    public string School { get; set; }
}