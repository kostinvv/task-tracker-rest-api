namespace TaskTracker.Domain.Entities;

/// <summary> Сущность, описывающая пользовательскую задачу. </summary>
public class TaskEntity : IAuditable
{
    /// <summary> Идентификатор задачи. </summary>
    public Guid Id { get; init; }
    
    /// <summary> Наименование задачи. </summary>
    public string Title { get; set; } = null!;
    
    /// <summary> Заметка к задаче. </summary>
    public string? Description { get; set; }
    
    /// <summary> Идентификатор пользователя. </summary>
    public Guid UserId { get; init; }

    /// <summary> Навигационное свойство, ссылка на сущность пользователя. </summary>
    public User User { get; init; } = null!;
    
    /// <summary> Метка выполнения задачи. </summary>
    public bool IsCompleted { get; set; }
    
    /// <summary> Метка изменения задачи. </summary>
    public bool IsModified { get; set; }
    
    /// <summary> Метка добавления задачи в избранное. </summary>
    public bool IsFavorite { get; set; }
    
    /// <summary> Дата создания задачи. </summary>
    public DateTime CreatedAt { get; init; }
    
    /// <summary> Дата изменения задачи. </summary>
    public DateTime? UpdatedAt { get; set; }
}