namespace TaskTracker.Infrastructure.EntityTypeConfigurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("Tasks");
    
        builder.HasKey(task => task.Id);

        builder.Property(task => task.Title)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(task => task.Description)
            .HasMaxLength(255);

        builder.Property(task => task.UserId)
            .IsRequired();
    }
}