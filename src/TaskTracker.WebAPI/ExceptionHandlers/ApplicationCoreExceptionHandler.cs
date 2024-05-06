using Microsoft.AspNetCore.Diagnostics;
using TaskTracker.Application.DTOs;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Exceptions.User;

namespace TaskTracker.WebAPI.ExceptionHandlers;

public class ApplicationCoreExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ApplicationCoreException applicationException)
        {
            return false;
        }
        
        context.Response.StatusCode = GetExceptionStatusCode(applicationException);
        
        var errorResponse = new ErrorResponse(applicationException.ErrorCode, applicationException.ErrorMessage);
        await context.Response
            .WriteAsJsonAsync(errorResponse, cancellationToken);
        return true;
    }
    
    private static int GetExceptionStatusCode(Exception ex) => ex switch
    {
        NotFoundException => StatusCodes.Status404NotFound,
        UserAlreadyExistException => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status400BadRequest
    };
}