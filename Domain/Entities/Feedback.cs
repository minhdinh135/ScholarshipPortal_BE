﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Feedback : BaseEntity
{
    [MaxLength(200)]
    public string Content { get; set; }
    
    public double Rating { get; set; } 
    
    public DateTime FeedbackDate { get; set; }
    
    public int ApplicantId { get; set; }
    
    public Account Applicant { get; set; }
    
    public int ServiceId { get; set; }
    
    public Service Service { get; set; }
}