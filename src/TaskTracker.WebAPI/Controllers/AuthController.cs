namespace TaskTracker.WebAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IUserService userService) : BaseController
{
    /// <summary> Аутентификация пользователя в системе. </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST api/auth/login
    ///     {
    ///         "email": "user@mail.ru",
    ///         "password": "123456"
    ///     }
    /// 
    /// </remarks>
    /// <returns> Token. </returns>
    /// <response code="200"> Token. </response>
    /// <response code="400"> Bad request. </response>
    /// <response code="500"> Internal server error. </response>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginAsync(
        [FromBody] LoginRequest request, CancellationToken cancellationToken)
        => Ok(await userService.LoginAsync(request, cancellationToken));
}