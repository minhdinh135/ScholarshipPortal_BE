﻿namespace Domain.DTOs.Service;

public class UpdateServiceDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Type { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }
}