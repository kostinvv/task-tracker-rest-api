namespace TaskTracker.Application.Interfaces.Services;

public interface IUserService
{
    public Task<AuthenticationResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);

    public Task<AuthenticationResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
}