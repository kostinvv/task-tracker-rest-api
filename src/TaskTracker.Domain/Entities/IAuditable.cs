namespace TaskTracker.Domain.Entities;

public interface IAuditable
{
    public DateTime CreatedAt { get; init; }
    
    public DateTime? UpdatedAt { get; set; }
}