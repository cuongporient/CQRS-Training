namespace Assignment04_TaskManager.Domain.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException(string message) : base(message)
    {
    }
}
