﻿using Domain.DTOs.Feedback;
using Domain.DTOs.Request;

namespace Domain.DTOs.Service;

public class ServiceDto
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Type { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public int? ProviderId { get; set; }
    public DateTime? CreatedAt { get; set; }
    
    public List<RequestDetailsDto> RequestDetails { get; set; }
    
    public List<FeedbackDto> Feedbacks { get; set; }
}