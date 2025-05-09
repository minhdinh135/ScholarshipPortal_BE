﻿namespace Domain.DTOs.Applicant;

public class ApplicantCertificateDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public int AchievedYear { get; set; }
}