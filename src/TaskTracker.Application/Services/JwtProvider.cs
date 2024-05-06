namespace TaskTracker.Application.Services;

public class JwtProvider(IOptions<JwtSettings> settings) : IJwtProvider
{
    private readonly JwtSettings _settings = settings.Value;
    
    public string CreateAccessToken(Payload payload)
    {
        Claim[] claims = [
            new Claim(ClaimTypes.NameIdentifier, payload.UserId),
            new Claim(ClaimTypes.Email, payload.Email),
        ];
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey)), 
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_settings.ExpiresHours)
        );
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }
}