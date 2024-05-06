namespace TaskTracker.Application.DTOs.User;

public record AuthenticationResponse(string Email, string AccessToken);