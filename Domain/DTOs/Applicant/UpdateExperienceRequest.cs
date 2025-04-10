﻿namespace Domain.DTOs.Applicant;

public class UpdateExperienceRequest
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public int FromYear { get; set; }

    public int ToYear { get; set; }
}