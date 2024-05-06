namespace TaskTracker.Application.DTOs.Task;

public record UpdateTaskRequest
{
    [JsonIgnore] public Guid Id { get; init; }
    
    [JsonIgnore] public Guid UserId { get; init; }

    [Required] [MaxLength(80)] public string Title { get; init; } = null!;
    
    [Required] [MaxLength(256)] public string Description { get; init; } = null!;
    
    [Required] public bool IsCompleted { get; init; }
    
    [Required] public bool IsFavorite { get; init; }
}