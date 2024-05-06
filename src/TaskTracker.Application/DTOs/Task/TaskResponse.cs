namespace TaskTracker.Application.DTOs.Task;

public record TaskResponse(
    Guid Id, 
    string Title, 
    string Description, 
    bool IsCompleted, 
    bool IsModified, 
    bool IsFavorite, 
    DateTime CreatedAt, 
    DateTime UpdatedAt);