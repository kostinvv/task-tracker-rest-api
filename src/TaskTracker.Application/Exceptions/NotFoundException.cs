namespace TaskTracker.Application.Exceptions;

public class NotFoundException() : ApplicationCoreException(
    nameof(Resources.ErrorMessage.NOT_FOUND), Resources.ErrorMessage.NOT_FOUND);