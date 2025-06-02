using System.Net;
using System.Text.Json;
using GlobalInsightsApi_Assessment.Exceptions;

namespace GlobalInsightsApi_Assessment.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            TraceId = context.TraceIdentifier,
            Message = exception.Message,
            Details = exception is ApiException apiEx ? apiEx.Details : null
        };

        switch (exception)
        {
            case ApiException apiException:
                response.StatusCode = apiException.StatusCode;
                errorResponse.ErrorCode = apiException.ErrorCode;
                _logger.LogWarning(exception, "API Error: {Message}", exception.Message);
                break;

            case HttpRequestException httpEx:
                response.StatusCode = (int)HttpStatusCode.BadGateway;
                errorResponse.ErrorCode = ErrorCodes.ExternalApiError;
                errorResponse.Message = "Error communicating with external service";
                _logger.LogError(httpEx, "External API Error: {Message}", httpEx.Message);
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.ErrorCode = ErrorCodes.InternalError;
                if (_environment.IsDevelopment())
                {
                    errorResponse.StackTrace = exception.StackTrace;
                }
                _logger.LogError(exception, "Unhandled Error: {Message}", exception.Message);
                break;
        }

        var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await response.WriteAsync(json);
    }
}

public class ErrorResponse
{
    public string TraceId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string ErrorCode { get; set; } = ErrorCodes.InternalError;
    public object? Details { get; set; }
    public string? StackTrace { get; set; }
} 