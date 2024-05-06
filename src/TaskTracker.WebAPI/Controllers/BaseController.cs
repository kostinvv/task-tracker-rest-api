namespace TaskTracker.WebAPI.Controllers;

public class BaseController : ControllerBase
{
    /// <summary> Идентификатор авторизованного пользователя.</summary>
    protected Guid UserId => Guid.Parse(
        HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    
    /// <summary> E-mail авторизованного пользователя. </summary>
    protected string Email => HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
}