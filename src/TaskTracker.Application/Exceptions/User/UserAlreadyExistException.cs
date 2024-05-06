namespace TaskTracker.Application.Exceptions.User;

public class UserAlreadyExistException() : ApplicationCoreException(
    nameof(Resources.ErrorMessage.USER_ALREADY_EXIST), Resources.ErrorMessage.USER_ALREADY_EXIST);