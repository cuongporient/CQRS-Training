namespace Assignment04_TaskManager.Domain.Exceptions;

public class ForbiddenException : AppException
{
    public ForbiddenException() : base("You do not have permission to access this resource.")
    {
    }

    public ForbiddenException(string message) : base(message)
    {
    }
}
