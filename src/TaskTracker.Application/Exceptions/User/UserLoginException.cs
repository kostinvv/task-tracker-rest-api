namespace TaskTracker.Application.Exceptions.User;

public class UserLoginException() : ApplicationCoreException(
    nameof(Resources.ErrorMessage.LOGIN_ERROR), Resources.ErrorMessage.LOGIN_ERROR);