namespace TaskTracker.Application.Services;

public class UserService(IApplicationDbContext dbContext, IJwtProvider jwtProvider) : IUserService
{
    public async Task<AuthenticationResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var email = request.Email;
        var user = await dbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
        
        if (user is not null)
        {
            throw new UserAlreadyExistException();
        }
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var userId = Guid.NewGuid();
        
        await dbContext.Users.AddAsync(new User {
            Id = userId,
            Email = email,
            PasswordHash = passwordHash,
        }, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var payload = new Payload(userId.ToString(), email);
        var accessToken = jwtProvider.CreateAccessToken(payload);
        var response = new AuthenticationResponse(email, accessToken);

        return response;
    }

    public async Task<AuthenticationResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var email = request.Email;
        var user = await dbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken: cancellationToken);
        
        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UserLoginException();
        }
        
        var payload = new Payload(user.Id.ToString(), email);
        var accessToken = jwtProvider.CreateAccessToken(payload);
        var response = new AuthenticationResponse(email, accessToken);
        
        return response;
    }
}