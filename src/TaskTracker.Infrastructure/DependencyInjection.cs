namespace TaskTracker.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var defaultConnection = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(builder 
            => builder.UseNpgsql(defaultConnection));

        services.AddScoped<IApplicationDbContext>(provider 
            => provider.GetService<ApplicationDbContext>()!);

        services.AddStackExchangeRedisCache(setupAction: options =>
        {
            options.Configuration = config.GetConnectionString("Redis");
            options.InstanceName = "TaskTracker_";
        });
    }
}