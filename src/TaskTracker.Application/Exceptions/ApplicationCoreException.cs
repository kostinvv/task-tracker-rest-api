namespace TaskTracker.Application.Exceptions;

public class ApplicationCoreException(string errorCode, string errorMessage) : Exception(errorMessage)
{
    public string ErrorCode => errorCode;

    public string ErrorMessage => Message;
}