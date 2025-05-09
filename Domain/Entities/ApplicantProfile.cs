﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ApplicantProfile : BaseEntity
{
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [MaxLength(100)]
    public string LastName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    [MaxLength(100)]
    public string? Gender { get; set; }
    
    [MaxLength(100)]
    public string? Nationality { get; set; }
    
    [MaxLength(100)]
    public string? Ethnicity { get; set; }
    
    [MaxLength(200)]
    public string? Bio { get; set; }
    
    public int ApplicantId { get; set; }
    
    public Account Applicant { get; set; }
    
    public ICollection<Education>? Educations { get; set; }
    
    public ICollection<ApplicantSkill>? ApplicantSkills { get; set; }
    
    public ICollection<ApplicantCertificate>? ApplicantCertificates { get; set; }
    
    public ICollection<Experience>? Experiences { get; set; }
}