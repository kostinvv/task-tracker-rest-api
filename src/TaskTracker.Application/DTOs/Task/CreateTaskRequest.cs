namespace TaskTracker.Application.DTOs.Task;

public record CreateTaskRequest
{
    [JsonIgnore] public Guid UserId { get; init; }

    [Required] [MaxLength(80)] public string Title { get; init; } = null!;
}