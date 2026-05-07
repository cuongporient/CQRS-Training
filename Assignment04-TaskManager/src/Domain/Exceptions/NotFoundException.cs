namespace Assignment04_TaskManager.Domain.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, object key) 
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}
