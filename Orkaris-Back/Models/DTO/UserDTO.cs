using System;
using System.ComponentModel.DataAnnotations;

namespace Orkaris_Back.Models.DTO;

public class LoginRequestDTO
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
public class RegisterUserDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
public class MinimalUserDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
public class PutUserDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Gender { get; set; } = string.Empty;

    public int Height { get; set; }

    public int Weight { get; set; }
    public DateOnly BirthDate { get; set; }

    public int ProfileType { get; set; }

}
public class InfoUserDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public int Height { get; set; }

    public int Weight { get; set; }
    public DateOnly BirthDate { get; set; }

    public int ProfileType { get; set; }

    public bool IsVerified { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}