﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ApplicantCertificate : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    [MaxLength(100)]
    public string Type { get; set; }
    
    [MaxLength(1024)]
    public string? ImageUrl { get; set; }
    
    public int ApplicantProfileId { get; set; }
    
    public ApplicantProfile ApplicantProfile { get; set; }
}