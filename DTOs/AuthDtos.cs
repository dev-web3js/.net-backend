using System;
using System.ComponentModel.DataAnnotations;

namespace Houseiana.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string? Phone { get; set; }

    public string? Role { get; set; } = "Guest";
}

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}

public class UpdateProfileDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Bio { get; set; }
    public string? ProfileImage { get; set; }
    public string? Language { get; set; }
    public string? Currency { get; set; }
    public bool? EmailNotifications { get; set; }
    public bool? SmsNotifications { get; set; }
    public bool? PushNotifications { get; set; }
}

public class UserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string Role { get; set; }
    public bool IsHost { get; set; }
    public bool IsAdmin { get; set; }
    public string HostVerified { get; set; }
    public DateTime? HostSince { get; set; }
    public string? ProfileImage { get; set; }
    public string? Bio { get; set; }
    public string Language { get; set; }
    public string Currency { get; set; }
    public bool EmailNotifications { get; set; }
    public bool SmsNotifications { get; set; }
    public bool PushNotifications { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class AuthResponseDto
{
    public UserDto User { get; set; }
    public string? Token { get; set; }
    public string Message { get; set; }
}

public class MessageResponseDto
{
    public string Message { get; set; }
}

public class TokenValidationResponseDto
{
    public bool Valid { get; set; }
    public UserDto User { get; set; }
    public string Message { get; set; }
}