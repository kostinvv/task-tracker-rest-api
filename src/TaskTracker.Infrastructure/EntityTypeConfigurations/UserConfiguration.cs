namespace TaskTracker.Infrastructure.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.HasIndex(user => user.Email)
            .IsUnique();
        
        builder.Property(user => user.Email)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.HasMany(user => user.Tasks)
            .WithOne(task => task.User)
            .HasForeignKey(task => task.UserId)
            .IsRequired();
    }
}