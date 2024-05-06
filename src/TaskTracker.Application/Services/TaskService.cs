namespace TaskTracker.Application.Services;

public class TaskService(IApplicationDbContext dbContext, IMapper mapper) : ITaskService
{
    public async Task<TaskResponse> GetByIdAsync(GetTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await dbContext.Tasks.AsNoTracking()
            .FirstOrDefaultAsync(task => task.Id == request.Id, cancellationToken: cancellationToken);

        if (task is null || task.UserId != request.UserId)
        {
            throw new NotFoundException();
        }

        return mapper.Map<TaskResponse>(task);
    }

    public async Task<IEnumerable<TaskResponse>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        var tasks = await dbContext.Tasks.AsNoTracking()
            .Where(task => task.UserId == userId)
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<List<TaskResponse>>(tasks);
    }

    public async Task CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = new TaskEntity
        {
            Id = new Guid(),
            Title = request.Title,
            UserId = request.UserId,
        };
        await dbContext.Tasks.AddAsync(task, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeleteTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await dbContext.Tasks.AsNoTracking()
            .FirstOrDefaultAsync(task => task.Id == request.Id, cancellationToken);

        if (task is null || task.UserId != request.UserId)
        {
            throw new NotFoundException();
        }
        dbContext.Tasks.Remove(task);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await dbContext.Tasks.FirstOrDefaultAsync(entity =>
            entity.Id == request.Id, cancellationToken);
        
        if (task is null || task.UserId != request.UserId)
        {
            throw new NotFoundException();
        }

        task.IsModified = true;
        task.Title = request.Title;
        task.Description = request.Description;
        task.IsFavorite = request.IsFavorite;
        task.IsCompleted = request.IsCompleted;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}