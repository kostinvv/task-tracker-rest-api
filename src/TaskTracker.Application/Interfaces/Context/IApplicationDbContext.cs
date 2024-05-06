namespace TaskTracker.Application.Interfaces.Context;

public interface IApplicationDbContext
{
    DbSet<TaskEntity> Tasks { get; set; }
    
    DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}