using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskTracker.Domain.Settings;

namespace TaskTracker.WebAPI.Configurations;

public static class AuthConfiguration
{
    public static void AddAuthConfiguration(this IServiceCollection service, IConfiguration config)
    {
        service.Configure<JwtSettings>(config.GetSection(nameof(JwtSettings)));

        service.AddAuthentication(configureOptions: options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                var jwtSettings = config.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
                
                options.Audience = jwtSettings!.Audience;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });
        
        service.AddAuthorization();
    }
}