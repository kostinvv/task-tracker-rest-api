namespace TaskTracker.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        
        services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<ITaskService, TaskService>();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}