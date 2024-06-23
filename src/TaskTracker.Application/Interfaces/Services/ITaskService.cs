namespace TaskTracker.Application.Interfaces.Services;

public interface ITaskService
{
    public Task<TaskResponse> GetByIdAsync(GetTaskRequest request, CancellationToken cancellationToken);
    
    public Task<IEnumerable<TaskResponse>> GetAllAsync(GetTasksRequest request, CancellationToken cancellationToken);
    
    public Task CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken);

    public Task DeleteAsync(DeleteTaskRequest request, CancellationToken cancellationToken);

    public Task UpdateAsync(UpdateTaskRequest request, CancellationToken cancellationToken);
}