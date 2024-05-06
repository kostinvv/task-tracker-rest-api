namespace TaskTracker.Application.DTOs.Task;

public record DeleteTaskRequest(Guid Id, Guid UserId);