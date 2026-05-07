using Assignment04_TaskManager.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Assignment04_TaskManager.Presentation.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var (statusCode, title, detail, errors) = exception switch
        {
            ValidationException validationException => (
                StatusCodes.Status400BadRequest,
                "Validation Error",
                validationException.Message,
                validationException.Errors),
            NotFoundException notFoundException => (
                StatusCodes.Status404NotFound,
                "Not Found",
                notFoundException.Message,
                null),
            BadRequestException badRequestException => (
                StatusCodes.Status400BadRequest,
                "Bad Request",
                badRequestException.Message,
                null),
            ForbiddenException forbiddenException => (
                StatusCodes.Status403Forbidden,
                "Forbidden",
                forbiddenException.Message,
                null),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred.",
                null)
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        };

        if (errors != null)
        {
            problemDetails.Extensions.Add("errors", errors);
        }

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
