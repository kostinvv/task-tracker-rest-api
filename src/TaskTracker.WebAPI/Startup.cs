namespace TaskTracker.WebAPI;

public static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<ApplicationCoreExceptionHandler>();
        
        builder.Services.AddProblemDetails();
        
        builder.Services.AddApplicationServices();

        builder.Services.AddAuthConfiguration(builder.Configuration);
        
        builder.Services.AddControllers();
        
        builder.Services.AddInfrastructure(builder.Configuration);
        
        builder.Services.AddSwaggerConfiguration();
        
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseExceptionHandler();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        
        return app;
    }
}