namespace TaskTracker.Application.Interfaces.Services;

public interface IJwtProvider
{
    public string CreateAccessToken(Payload payload);
}