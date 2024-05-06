namespace TaskTracker.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    
    public string Email { get; set; } = null!;
    
    public string PasswordHash { get; set; } = null!;

    public ICollection<TaskEntity> Tasks { get; } = [];
}