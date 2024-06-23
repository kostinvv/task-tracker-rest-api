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

        builder.Services.AddCors(options 
            => options.AddPolicy(name: "jsClient", configurePolicy: policyBuilder 
            => policyBuilder
                .WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
            )
        );
        
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
        
        app.UseCors(policyName: "jsClient");
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        
        return app;
    }
}