﻿namespace Domain.DTOs.Authentication;

public record LoginDTO(string? Email, string? Password);

// public class LoginDTO
// {
//     public string? Email { get; set; }
//     public string? Password { get; set; }
// }