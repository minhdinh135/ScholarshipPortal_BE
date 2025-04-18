﻿namespace Domain.DTOs.Authentication;

public class RegisterDto
{
    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public string? AvatarUrl { get; set; }

    public bool? LoginWithGoogle { get; set; }

    public string? Status { get; set; }

    public int? FunderId { get; set; }

    public int? RoleId { get; set; }
}
