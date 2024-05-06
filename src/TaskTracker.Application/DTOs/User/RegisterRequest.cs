namespace TaskTracker.Application.DTOs.User;

public record RegisterRequest
{
    [Required] [EmailAddress] public string Email { get; init; } = null!;
    
    [Required] [MinLength(6)] public string Password { get; init; } = null!;
    
    [Required] [Compare("Password")] public string PasswordConfirm { get; init; } = null!;
}