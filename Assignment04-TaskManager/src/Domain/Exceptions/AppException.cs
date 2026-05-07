namespace Assignment04_TaskManager.Domain.Exceptions;

public abstract class AppException : Exception
{
    protected AppException(string message) : base(message)
    {
    }
}
