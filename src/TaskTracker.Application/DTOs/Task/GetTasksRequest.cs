namespace TaskTracker.Application.DTOs.Task;

public record GetTasksRequest(Guid UserId, string? Search, string? SortItem, string? Order);