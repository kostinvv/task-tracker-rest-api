using System.Linq.Expressions;

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

    public async Task<IEnumerable<TaskResponse>> GetAllAsync(GetTasksRequest request, CancellationToken cancellationToken)
    {
        var query = dbContext.Tasks
            .Where(task => string.IsNullOrWhiteSpace(request.Search) || 
                           task.Title.ToLower().Contains(request.Search.ToLower()));

        Expression<Func<TaskEntity, object>> selectorKey = request.SortItem?.ToLower() switch
        {
            "title" => task => task.Title,
            "date" => task => task.CreatedAt,
            "favorite" => task => task.IsFavorite,
            _ => task => task.Id
        };

        query = request.Order == "desc" 
            ? query.OrderByDescending(selectorKey) 
            : query.OrderBy(selectorKey);

        var tasks = await query
            .Where(task => task.UserId == request.UserId)
            .ToListAsync(cancellationToken);
            
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