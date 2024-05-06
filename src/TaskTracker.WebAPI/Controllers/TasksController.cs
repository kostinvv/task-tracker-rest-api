namespace TaskTracker.WebAPI.Controllers;

[Route("api/tasks")]
[ApiController]
public class TasksController(ITaskService taskService) : BaseController
{
    /// <summary> Получение списка задач. </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     GET api/tasks
    /// 
    /// </remarks>
    /// <returns> Tasks. </returns>
    /// <response code="200"> Tasks. </response>
    /// <response code="401"> Unauthorized. </response>
    /// <response code="500"> Internal server error. </response>
    [Authorize]
    [HttpGet(template: "")]
    [ProducesResponseType<List<TaskResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken) 
        => Ok(await taskService.GetAllAsync(UserId, cancellationToken));

    /// <summary> Получение задачи по идентификатору. </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     GET api/tasks/{id}
    /// 
    /// </remarks>
    /// <returns> Task. </returns>
    /// <response code="200"> Task. </response>
    /// <response code="401"> Unauthorized. </response>
    /// <response code="404"> Not found. </response>
    /// <response code="500"> Internal server error. </response>
    [Authorize]
    [HttpGet(template: "{id:guid}")]
    [ProducesResponseType<TaskResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetTaskRequest(id, UserId);
        return Ok(await taskService.GetByIdAsync(request, cancellationToken));
    }

    /// <summary> Создание новой задачи. </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST api/tasks
    ///     {
    ///         "title": "string"
    ///     }
    /// 
    /// </remarks>
    /// <response code="201"> Created. </response>
    /// <response code="401"> Unauthorized. </response>
    /// <response code="500"> Internal server error. </response>
    [Authorize]
    [HttpPost(template: "")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        request = request with { UserId = UserId };
        await taskService.CreateAsync(request, cancellationToken);
        return Created();
    }

    /// <summary> Изменение задачи. </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     PUT api/tasks
    ///     {
    ///         "title": "string",
    ///         "description": "string",
    ///         "isCompleted": true,
    ///         "isFavorite": true,
    ///     }
    /// 
    /// </remarks>
    /// <response code="200"> Ok. </response>
    /// <response code="401"> Unauthorized. </response>
    /// <response code="404"> Not found. </response>
    /// <response code="500"> Internal server error. </response>
    [Authorize]
    [HttpPut(template: "{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateTaskRequest request,
        CancellationToken cancellationToken)
    {
        request = request with { Id = id, UserId = UserId };
        await taskService.UpdateAsync(request, cancellationToken);
        return Ok();
    }

    /// <summary> Удаление задачи. </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     DELETE api/tasks/{id}
    /// 
    /// </remarks>
    /// <response code="200"> Ok. </response>
    /// <response code="401"> Unauthorized. </response>
    /// <response code="404"> Not found. </response>
    /// <response code="500"> Internal server error. </response>
    [Authorize]
    [HttpDelete(template: "{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteTaskRequest(id, UserId);
        await taskService.DeleteAsync(request, cancellationToken);
        return Ok();
    }
}