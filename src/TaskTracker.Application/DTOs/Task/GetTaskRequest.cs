namespace TaskTracker.Application.DTOs.Task;

public record GetTaskRequest(Guid Id, Guid UserId);