namespace TaskTracker.WebAPI.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(IUserService userService) : BaseController
{
    /// <summary> Регистрация нового пользователя в системе. </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST api/users
    ///     {
    ///         "email": "user@mail.ru",
    ///         "password": "string",
    ///         "passwordConfirm": "string"
    ///     }
    /// 
    /// </remarks>
    /// <returns> Token. </returns>
    /// <response code="200"> Token. </response>
    /// <response code="409"> Conflict. </response>
    /// <response code="500"> Internal server error. </response>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterRequest request, CancellationToken cancellationToken) 
        => Ok(await userService.RegisterAsync(request, cancellationToken));
}