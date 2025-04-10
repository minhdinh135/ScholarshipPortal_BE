﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class RequestDetail : BaseEntity
{
    [MaxLength(200)]
    public string? Comment { get; set; }
    
    public int RequestId { get; set; }
    
    public Request Request { get; set; }
    
    public int ServiceId { get; set; }
    
    public Service Service { get; set; }
    
    public ICollection<RequestDetailFile>? RequestDetailFiles { get; set; }
}